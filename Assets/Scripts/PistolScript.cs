using System.Diagnostics.Tracing;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PistolScript : Interactable
{
    public GameObject bullet;
    public GameObject nabojnica;

    public GameObject magazine;
    public Transform magPos;

    public Transform shootpoint;

    public bool automatic;
    public float shootDelay;
    private bool _isReseting;

    public int maxAmmo;
    [Range(0f, 180f)] public float bulletSpread;
    public int bulletCount = 1;

    public AudioCue shotSound;
    public AudioCue emptySound;
    public AudioCue reloadSound;
    public AudioCue equipSound;
    public AudioCue dropSound;


    private int _currAmmo;
    private bool _canShoot = true;
    private bool _isReloading;

    protected override void CustomStart()
    {
        base.CustomStart();

        _currAmmo = maxAmmo;
    }

    protected override void CustomUpdate() {
        base.CustomUpdate();

    }

    public void AIShoot(){
        MainInteractionPressed();
        MainInteractionHold();
        if(!_isReseting)
            MainInteractionReleased();

        if(_currAmmo <= 0)
            ReloadPressed();
    }

    protected override void MainInteractionPressed()
    {
        base.MainInteractionPressed();

        if(_canShoot && _currAmmo <= 0 && !_isReloading){
            AudioManagerScript.SpawnAudio(emptySound.GetSound(), emptySound.volume, transform.position);
        }
    }

    protected override void MainInteractionHold()  // strielanie
    {
        base.MainInteractionHold();

            if (_canShoot && !_isReloading && _currAmmo > 0)
            {
            _canShoot = false;

            _currAmmo--;

            AudioManagerScript.SpawnAudio(shotSound.GetSound(), shotSound.volume, transform.position);

            for(int i = 0; i < bulletCount; i++){
                GameObject b = Instantiate(bullet, shootpoint.position, shootpoint.rotation);
                b.transform.rotation = Quaternion.Euler(0, 0,
                    b.transform.rotation.eulerAngles.z + Random.Range(-bulletSpread, bulletSpread));
                b.GetComponent<BulletScript>().Setup();
            }

            GameObject n = Instantiate(nabojnica, transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + Random.Range(-5f, 5f)));
            n.GetComponent<Rigidbody2D>().AddForce(n.transform.up * (300 * (transform.localScale.y < 0 ? -1 : 1)));
            n.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-500f, 500f));
            n.GetComponent<CasingScript>().startY = transform.position.y + Random.Range(-0.1f, -0.05f);

            Destroy(n, 5f);            

            if (automatic){
                Invoke(nameof(ResetShoot), shootDelay);
            }
        }
    }

    protected override void MainInteractionReleased()
    {
        base.MainInteractionReleased();

        if (!automatic && !_canShoot){
            _isReseting = true;
            Invoke(nameof(ResetShoot), shootDelay);
        }
    }

    protected override void ReloadPressed() //reload stlacenim klavesy "R"
    {
        base.ReloadPressed();

        if(_isReloading || _currAmmo == maxAmmo) return;

        if(magazine != null){
            if(magPos.GetChild(0) != null){
                GameObject spawnedMag = magPos.GetChild(0).gameObject;
                
                if(maxAmmo == 1) {

                }
                else{
                    spawnedMag.transform.parent = null;
                    spawnedMag.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    spawnedMag.GetComponent<CasingScript>().startY = transform.position.y + Random.Range(-0.1f, -0.05f);
                    spawnedMag.GetComponent<CasingScript>().enabled = true;
                    spawnedMag.GetComponent<Rigidbody2D>().AddForce(transform.up * (300 * (transform.localScale.y < 0 ? -1 : 1)));
                    spawnedMag.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100f, 100f));
                    
                    Destroy(spawnedMag, 10f);
                }

                _isReloading = true;

                AudioClip reloadClip = reloadSound.GetSound();
                AudioManagerScript.SpawnAudio(reloadClip, reloadSound.volume, transform.position);

                Invoke(nameof(Reload), reloadClip.length + 0.5f);    
            }
        }
    }

    private void ResetShoot()
    {
        _canShoot = true;
        _isReseting = false;
    }

    private void Reload()
    {
        _isReloading = false;
        _currAmmo = maxAmmo;

        if(magazine != null) {
            Instantiate(magazine, magPos);
        }
    }
}