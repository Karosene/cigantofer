using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesScript : MonoBehaviour
{
    private Vector2 _mousePos;

    private void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localPosition = new Vector3(Mathf.Clamp((_mousePos.x - transform.position.x) / 200, -.04f, .04f), Mathf.Clamp((_mousePos.y - transform.position.y) / 200, -.02f, .02f), 0);

        transform.parent.GetComponent<SpriteRenderer>().flipX = _mousePos.x < transform.parent.position.x;
        GetComponent<SpriteRenderer>().flipX = _mousePos.x < transform.parent.position.x;
    }
}