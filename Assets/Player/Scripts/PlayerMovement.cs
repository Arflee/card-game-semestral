using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Rigidbody2D _rigidbody;
    private static StandardControls _controls;
    private int _lastDirection = 0;
    private Animator _animator;

    public static StandardControls Controls
    {
        get
        {
            _controls ??= new StandardControls();

            return _controls;
        }
        private set
        {
            _controls = value;
        }
    }

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _controls = new StandardControls();
        _controls.Player.Move.Enable();
        _animator = GetComponent<Animator>();


        Controls ??= new StandardControls();
        Controls.Player.Move.Enable();
    }

    void Update()
    {
        // TODO add acceleration
        Vector2 movementVector = Controls.Player.Move.ReadValue<Vector2>();
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
