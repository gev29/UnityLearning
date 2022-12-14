using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Transform player;
    private Rigidbody2D rb;
    private bool killed;
    private Action<bool> onDestroy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Transform player, Action<bool> onDestroy)
    {
        this.player = player;
        this.onDestroy = onDestroy;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.GameFinished)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            rb.velocity = direction * movementSpeed;
            rb.MoveRotation(Quaternion.LookRotation(Vector3.forward, direction));
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GOTag.Bullet.ToString()))
        {
            killed = true;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        this.onDestroy?.Invoke(killed);
    }
}
