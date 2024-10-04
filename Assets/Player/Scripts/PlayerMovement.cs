using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Rigidbody2D _rigidbody;
    private StandardControls _controls;

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _controls = new StandardControls();
        _controls.Player.Move.Enable();
    }

    void Update()
    {
        // TODO add acceleration
        Vector2 movementVector = _controls.Player.Move.ReadValue<Vector2>();
        _rigidbody.velocity = movementVector * speed;
    }
}
