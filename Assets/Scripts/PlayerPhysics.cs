using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform bulletsParent;
    [SerializeField] private float hitCount;

    [SerializeField] private GameObject bulletPrefab;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 direction;
    private int shootCount;
    private Color defaultColor;

    private float health;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = 100f;
        defaultColor = spriteRenderer.color;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector2(horizontal, vertical).normalized;

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

    private void MovePlayer()
    {
        //rb.MovePosition(transform.position + (Vector3)direction * Time.deltaTime * movementSpeed);
        //rb.velocity = (Vector3)direction * movementSpeed;
        rb.AddForce((Vector3)direction * movementSpeed, ForceMode2D.Force);
    }

    private void RotatePlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        Vector2 direction = mousePosition - transform.position;
        rb.MoveRotation (Quaternion.LookRotation(Vector3.forward, direction));
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.transform.SetParent(bulletsParent);
        bullet.SetVelocity(transform.up * bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GOTag.Enemy.ToString()))
        {
            Destroy(collision.gameObject);
            SetHealth(health - 100f / hitCount);
        }
    }

    private void SetHealth(float xpAmount)
    {
        health = Mathf.Clamp(xpAmount, 0, 100);
        spriteRenderer.color = Color.Lerp(Color.black, defaultColor, health / 100f);

        if (health <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void Heal(float xpAmount)
    {
        SetHealth(health + xpAmount);
    }
}
