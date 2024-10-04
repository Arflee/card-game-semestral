using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Rigidbody2D rb;
    private StandardControls controls;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new StandardControls();
        controls.Player.Move.Enable();
    }

    void Update()
    {
        Vector2 movementVector = controls.Player.Move.ReadValue<Vector2>();
        rb.velocity = movementVector * speed;
    }
}
