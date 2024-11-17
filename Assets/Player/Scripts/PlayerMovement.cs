using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Rigidbody2D _rigidbody;
    private StandardControls _controls;
    private Animator _animator;
    private int _lastDirection = 0;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _controls = new StandardControls();
        _controls.Player.Move.Enable();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        // TODO add acceleration
        Vector2 movementVector = _controls.Player.Move.ReadValue<Vector2>();
        _rigidbody.velocity = movementVector * speed;

        int direction = 0;
        if (movementVector.x < 0)
        {
            direction = -1;
            _lastDirection = direction;
        }
        else if (movementVector.x > 0)
        {
            direction = 1;
            _lastDirection = direction;
        }
        else if (movementVector.y != 0)
            direction = _lastDirection;

        _animator.SetInteger("Direction", direction);
    }
}
