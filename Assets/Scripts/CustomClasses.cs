using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using random = UnityEngine.Random;

public enum HumanStates{
    Idle,Walking, Fleeing, Attacking
}
public class Interactable : MonoBehaviour
{
    public bool pickedUp;
    public List<SpriteRenderer> sprites = new();
    private void Start(){
        CustomStart();
    }

    private void Update() {
        CustomUpdate();

        if(!pickedUp) return;
        if(Input.GetMouseButtonDown(0)){
            MainInteractionPressed();
        }
        if(Input.GetMouseButton(0)){
            MainInteractionHold();
        }
        if(Input.GetMouseButtonUp(0)){
            MainInteractionReleased();
        }
        if(Input.GetKeyDown(KeyCode.R)){ // moj kod na reload
            ReloadPressed();
        }
        // if(Input.GetMouseButtonDown(1)){ // original reload z hodiny
        //     ReloadPressed();
        // }
        if(Input.GetMouseButtonDown(1)){
            ReloadHold();
        }
        if(Input.GetMouseButtonDown(1)){
            ReloadReleased();
        }
    }

    public void changePickedUp(bool pckdUp){
        pickedUp = pckdUp;
        
        foreach(SpriteRenderer sprite in sprites){
            sprite.sortingLayerName = pickedUp ? "Default" : "GroundClutter";
        }
    }
    protected virtual void CustomStart(){}
    protected virtual void CustomUpdate(){}
    protected virtual void MainInteractionPressed() {}
    protected virtual void MainInteractionHold() {}
    protected virtual void MainInteractionReleased() {}

    protected virtual void ReloadPressed(){}
    protected virtual void ReloadHold(){}
    protected virtual void ReloadReleased(){}
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Human : MonoBehaviour{
    public float speed;
    protected Vector2 lookDir;
    protected Vector2 TargetPosition;
    protected Rigidbody2D Rb;
    protected HumanStates CurrentState;
    public GameObject Player;

    private void Start(){
        Rb = GetComponent<Rigidbody2D>();
        CurrentState = HumanStates.Idle;
        // Player = GameObject.FindWithTag("Player");

        CustomStart();
    }
    protected virtual void CustomStart(){ }

    public virtual void PlayerEnter(){

    }
    
    public virtual void PlayerExit(){
        
    }

    protected Vector2 GetRandomLookDir() {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    protected Vector2 GetRandomPosInRange(Vector2 StartPos, float range){
        Vector2 randPoint = StartPos + new Vector2(Random.Range(-range, range), Random.Range(-range, range));
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(StartPos, randPoint);
        if(hits.Length > 2){
            return hits[2].point;
        }
        return randPoint;
    }
}