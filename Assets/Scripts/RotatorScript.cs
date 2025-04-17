using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotatorScript : MonoBehaviour
{
    public bool isRightHand = false;

    private Vector2 _mousePos;
    private Vector2 _dir;

    private void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _dir = _mousePos - (Vector2)transform.position;

        transform.right = _dir.normalized;

        if(!GetComponentInChildren<SpriteRenderer>())
            return;

        if (_mousePos.x < transform.position.x)
        {
            if(transform.parent.GetComponent<PlayerMovement>().heldObject != null){
                transform.parent.GetComponent<PlayerMovement>().heldObject.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -2;
            }
        }
        else
        {
            if(transform.parent.GetComponent<PlayerMovement>().heldObject != null){
                transform.parent.GetComponent<PlayerMovement>().heldObject.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
            }
        }
    }
}
