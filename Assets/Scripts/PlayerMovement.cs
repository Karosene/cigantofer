using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public GameObject sprite;
    public GameObject objectHolder;
    public Interactable heldObject;
    private List<Interactable> interactablesInRange = new();

    private Vector2 _dir;
    private Rigidbody2D _rb;

    private bool _isSprinting;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _dir.x = Input.GetAxis("Horizontal");
        _dir.y = Input.GetAxis("Vertical");

        Vector2 _mousePos = Input.mousePosition;
        Vector2 _worldMousePos = Camera.main.ScreenToWorldPoint(_mousePos);

        if(heldObject != null){
            bool shouldFlip = _worldMousePos.x < objectHolder.transform.position.x;

            heldObject.gameObject.transform.localScale = new Vector3(1, shouldFlip ? -1 : 1, 1);
        }

        if(Input.GetKeyDown(KeyCode.E) && interactablesInRange.Count > 0 && heldObject == null){
            GameObject closestInteractable = interactablesInRange[0].gameObject;

            foreach(Interactable interactable in interactablesInRange){
                if(Vector2.Distance(transform.position, interactable.gameObject.transform.position) < Vector2.Distance(transform.position, closestInteractable.transform.position)
                ){
                    closestInteractable = interactable.gameObject;
                }
            }
            
            heldObject = closestInteractable.GetComponent<Interactable>();
            heldObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            heldObject.pickedUp = true;
            closestInteractable.transform.position = objectHolder.transform.position;
            closestInteractable.transform.rotation = objectHolder.transform.rotation;
            closestInteractable.transform.SetParent(objectHolder.transform);
            closestInteractable.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Default";
        }

        if(Input.GetKeyDown(KeyCode.G) && heldObject != null){
            heldObject.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "GroundClutter";
            heldObject.pickedUp = false;
            heldObject.gameObject.transform.parent = null;
            heldObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            heldObject.GetComponent<Rigidbody2D>().AddForce((_worldMousePos - (Vector2)transform.position) * 30f);
            heldObject.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-200f, 200f));
            heldObject = null;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && !_isSprinting){            
            _isSprinting = true;
            speed = speed * 2;
        } if(Input.GetKeyUp(KeyCode.LeftShift)){
            _isSprinting = false;
            speed = speed / 2;
        }
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_dir * (speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Interactable")){
            interactablesInRange.Add(other.gameObject.GetComponent<Interactable>());
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("Interactable")){
            interactablesInRange.Remove(other.gameObject.GetComponent<Interactable>());
        }
    }
}
