using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Transform player;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Transform player)
    {
        this.player = player;
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
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
