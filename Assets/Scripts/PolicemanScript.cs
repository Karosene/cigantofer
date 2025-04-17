using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicemanScript : CivilianScript
{
    public override void PlayerEnter()
    {
        base.PlayerEnter();

        CurrentState = HumanStates.Attacking;
    }

    public override void PlayerExit()
    {
        base.PlayerExit();

        Invoke(nameof(stopAttacking), .5f);
    }

    private void stopAttacking(){
        CurrentState = HumanStates.Idle;
    }

    protected override void Update(){

        if(CurrentState != HumanStates.Attacking)
            CancelInvoke(nameof(Shoot));
        
        switch(CurrentState){
            case HumanStates.Idle:
                TargetPosition = GetRandomPosInRange(transform.position, 6);
                CurrentState = HumanStates.Walking;
                break;
            case HumanStates.Walking:
                lookDir = (TargetPosition - (Vector2)transform.position).normalized;
                Rb.AddForce(vision.transform.right * (speed * Time.deltaTime));

                if(Vector2.Distance(transform.position, TargetPosition) < 1.5f)
                    CurrentState = HumanStates.Idle;
                break;
            case HumanStates.Attacking:
                lookDir = (Player.transform.position - transform.position).normalized;
                
                if(Vector2.Distance(Player.transform.position, transform.position) > 6f) {
                    Rb.AddForce(vision.transform.right * (speed * Time.deltaTime));
                }
                if(Vector2.Distance(Player.transform.position, transform.position) < 5f) {
                    Rb.AddForce(vision.transform.right * (-speed * Time.deltaTime));
                }
                Shoot();
                break;
            default:
                break;
        }

        vision.transform.right = lookDir;
    }

    private void Shoot(){
        PistolScript p = vision.transform.GetChild(0).GetComponent<PistolScript>();
        p.AIShoot();

    }
}
