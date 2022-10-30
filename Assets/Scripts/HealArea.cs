using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float minBrightness;
    [Range(0, 1)]
    [SerializeField] private float maxBrightness;

    [SerializeField] private SpriteRenderer background;
    [SerializeField] private float animationSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Activate(collision.GetComponent<PlayerPhysics>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Deactivate();
        }
    }

    private void Activate(PlayerPhysics player)
    {
        StartCoroutine(Heal(player));
    }

    private IEnumerator Heal(PlayerPhysics player)
    {
        while (true)
        {
            yield return null;


        }
    }

    private void Deactivate()
    {
        StopAllCoroutines();
    }
}
