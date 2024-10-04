using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speed = 6f;

    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), speed * Time.deltaTime);
    }
}
