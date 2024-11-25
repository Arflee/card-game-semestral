using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Rigidbody2D _rigidbody;
    private static StandardControls _controls;
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

        Controls ??= new StandardControls();
        Controls.Player.Move.Enable();
    }

    void Update()
    {
        // TODO add acceleration
        Vector2 movementVector = Controls.Player.Move.ReadValue<Vector2>();
        _rigidbody.velocity = movementVector * speed;
    }
}
