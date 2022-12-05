using UnityEngine;

public abstract class APlayer : MonoBehaviour
{
    public const float MaxHP = 100f;
    public const float MaxMana = 100f;

    private const string InputHorizontalAxix = "Horizontal";
    private const string InputVerticalAxix = "Vertical";

    private const string BulletsParentName = "BulletsParent";

    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float hitCount;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletPrefab;

    protected Vector2 direction;

    protected Transform bulletsParent;
    protected float health;
    protected float mana;

    public float Health => health;
    public float Mana => mana;

    protected virtual void Awake()
    {
        SetHealth(MaxHP);
        SetMana(MaxMana);
        bulletsParent = new GameObject(BulletsParentName).transform;
    }

    protected virtual void Update()
    {
        float horizontal = Input.GetAxisRaw(InputHorizontalAxix);
        float vertical = Input.GetAxisRaw(InputVerticalAxix);
        direction = new Vector2(horizontal, vertical).normalized;
    }

    protected virtual void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.transform.SetParent(bulletsParent);
        bullet.SetVelocity(transform.up * bulletSpeed);
    }

    protected virtual void SetHealth(float xpAmount)
    {
        health = Mathf.Clamp(xpAmount, 0, MaxHP);

        if (health <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    protected virtual void SetMana(float manaAmount)
    {
        mana = Mathf.Clamp(manaAmount, 0, MaxMana);
    }

    public void Heal(float xpAmount)
    {
        SetHealth(health + xpAmount);
    }

    protected abstract void MovePlayer();
    protected abstract void RotatePlayer();

    protected virtual void OnDestroy()
    {
        if (bulletsParent != null)
        {
            Destroy(bulletsParent.gameObject);
        }
    }
}
