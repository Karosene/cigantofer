using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CustomPlayerAnimation : MonoBehaviour
{
    public float framesPerSecond;

    private bool _isWalking;

    [Separator("Animations")]
    public Sprite[] idleAnim;
    public Sprite[] walkAnim;

    private SpriteRenderer _spriteRenderer;
    private Coroutine _animationCoroutine;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartAnimation();
    }

    private void StartAnimation()
    {
        // Stop any existing animation coroutine
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }

        // Start a new animation coroutine
        _animationCoroutine = StartCoroutine(AnimationLoop());
    }

    private IEnumerator AnimationLoop()
    {
        while (true)
        {
            Sprite[] currentAnim = _isWalking ? walkAnim : idleAnim;
            if (currentAnim != null && currentAnim.Length > 0)
            {
                for (int i = 0; i < currentAnim.Length; i++)
                {
                    _spriteRenderer.sprite = currentAnim[i];
                    yield return new WaitForSeconds(1f / framesPerSecond);
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    // Example method to toggle walking state
    public void SetWalking(bool walking)
    {
        if (_isWalking != walking)
        {
            _isWalking = walking;
            StartAnimation();
        }
    }
}