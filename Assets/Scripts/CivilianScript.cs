using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CivilianScript : Human
{
    public GameObject vision;

    protected virtual void Update() {
        vision.transform.right = Vector2.Lerp(transform.right, lookDir, .5f);

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
            case HumanStates.Fleeing:
                lookDir = (transform.position - Player.transform.position).normalized;
                Rb.AddForce(vision.transform.right * (speed * 3 * Time.deltaTime));
                break;
            case HumanStates.Attacking:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override void PlayerEnter()
    {
        base.PlayerEnter();

        CurrentState = HumanStates.Fleeing;
    }

    public override void PlayerExit()
    {
        base.PlayerExit();

        Invoke(nameof(stopFleeing), 3);
    }

    private void stopFleeing(){
        CurrentState = HumanStates.Idle;
    }
}
