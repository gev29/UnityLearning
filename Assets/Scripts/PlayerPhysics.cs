using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Rigidbody2D rb;
    private Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector2(horizontal, vertical).normalized;
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(transform.position + (Vector3)direction * Time.deltaTime * movementSpeed);
        rb.velocity = (Vector3)direction * movementSpeed;
    }
}
