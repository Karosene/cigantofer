using System.Diagnostics.Tracing;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BazookaScript : Interactable
{
    public GameObject bulletBazooka;
    public GameObject magazineBazooka;
    public Transform magPosBazooka;
    public Transform shootpointBazooka;


    public int maxAmmoBazooka;
    public int bulletCountBazooka = 1;

    public AudioCue shotSoundBazooka;
    public AudioCue emptySoundBazooka;
    public AudioCue reloadSoundBazooka;
    public AudioCue equipSoundBazooka;
    public AudioCue dropSoundBazooka;


    private int _currAmmo;
    private bool _canShoot = true;
    private bool _isReloading;

    protected override void CustomStart()
    {
        base.CustomStart();

        _currAmmo = maxAmmoBazooka;
    }

    protected override void CustomUpdate() {
        base.CustomUpdate();

        if(_currAmmo <= 0 && magPosBazooka.childCount > 0){
            magPosBazooka.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    protected override void MainInteractionPressed()
    {
        base.MainInteractionPressed();

        if(_canShoot && _currAmmo <= 0 && !_isReloading){
            AudioManagerScript.SpawnAudio(emptySoundBazooka.GetSound(), emptySoundBazooka.volume, transform.position);
        }
    }

    protected override void MainInteractionHold()  // strielanie
    {
        base.MainInteractionHold();

            if (_canShoot && !_isReloading && _currAmmo > 0)
            {
            _canShoot = false;

            _currAmmo--;

            AudioManagerScript.SpawnAudio(shotSoundBazooka.GetSound(), shotSoundBazooka.volume, transform.position);

            for(int i = 0; i < bulletCountBazooka; i++){
                GameObject b = Instantiate(bulletBazooka, shootpointBazooka.position, shootpointBazooka.rotation);
                b.GetComponent<BulletScript>().Setup();
            }
        }
    }

    protected override void MainInteractionReleased()
    {
        base.MainInteractionReleased();

        if (!_canShoot){
            ResetShoot();
        }
    }

    protected override void ReloadPressed() //reload stlacenim klavesy "R"
    {
        base.ReloadPressed();

        if(_isReloading || _currAmmo == maxAmmoBazooka) return;

        if(magazineBazooka != null && magPosBazooka.GetChild(0) != null){
            _isReloading = true;
            Destroy(magPosBazooka.GetChild(0).gameObject);

            AudioClip reloadClip = reloadSoundBazooka.GetSound();
            AudioManagerScript.SpawnAudio(reloadClip, reloadSoundBazooka.volume, transform.position);

            Invoke(nameof(Reload), reloadClip.length + 0.5f);    
        }
    }

    private void ResetShoot()
    {
        _canShoot = true;
    }
    private void Reload()
    {
        _isReloading = false;
        _currAmmo = maxAmmoBazooka;

        if(magazineBazooka != null) {
            Instantiate(magazineBazooka, magPosBazooka);
        }
    }
}