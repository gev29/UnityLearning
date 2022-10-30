using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    private void Update()
    {
        if (destroyTime > 0)
        {
            destroyTime = Mathf.Max(destroyTime - Time.deltaTime, 0);
            if (destroyTime == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
