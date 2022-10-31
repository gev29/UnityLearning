using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyCoroutine());
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    //private void Update()
    //{
    //    if (destroyTime > 0)
    //    {
    //        destroyTime = Mathf.Max(destroyTime - Time.deltaTime, 0);
    //        if (destroyTime == 0)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }
}
