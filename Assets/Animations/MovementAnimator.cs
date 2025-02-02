using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimator : MonoBehaviour
{
    private Animator _animator;
    private Vector2 _lastPosition;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 direction = (Vector2)transform.position - _lastPosition;
        _lastPosition = transform.position;

        _animator.SetInteger("Horizontal", (int)Mathf.Sign(direction.x));
        _animator.SetInteger("Vertical", (int)Mathf.Sign(direction.y));

        if (direction.x == 0)
            _animator.SetInteger("Horizontal", 0);
        if (direction.y == 0)
            _animator.SetInteger("Vertical", 0);
    }
}
