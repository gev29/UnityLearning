using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPhysics : APlayer2D
{
    private Rigidbody2D rb;
    private int shootCount;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonUp(0))
        {
            shootCount++;
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.GameFinished)
        {
            MovePlayer();
            RotatePlayer();

            if (shootCount > 0)
            {
                Shoot();
                shootCount--;
            }
        }
    }

    protected override void MovePlayer()
    {
        //rb.MovePosition(transform.position + (Vector3)direction * Time.deltaTime * movementSpeed);
        //rb.velocity = (Vector3)direction * movementSpeed;
        rb.AddForce((Vector3)direction * movementSpeed, ForceMode2D.Force);
    }

    protected override void RotatePlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        Vector2 direction = mousePosition - transform.position;
        rb.MoveRotation (Quaternion.LookRotation(Vector3.forward, direction));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GOTag.Enemy.ToString()))
        {
            Destroy(collision.gameObject);
            SetHealth(health - 100f / hitCount);
        }
    }
}
