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
    [SerializeField] private float healingSpeed;
    [SerializeField] private AnimationCurve animationCurve;

    //private IEnumerator enumerator;
    private Coroutine coroutine;

    private void Awake()
    {
        SetBrightness(minBrightness);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GOTag.Player.ToString()))
        {
            Activate(collision.GetComponent<APlayer>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(GOTag.Player.ToString()))
        {
            Deactivate();
        }
    }

    private void Activate(APlayer player)
    {
        //StartCoroutine("Heal", player);

        //enumerator = Heal(player);
        //StartCoroutine(enumerator);

        coroutine = StartCoroutine(Heal(player));
    }

    private void Deactivate()
    {
        //StopCoroutine("Heal");
        //StopCoroutine(enumerator);
        StopCoroutine(coroutine);

        //StopAllCoroutines();
        SetBrightness(minBrightness);
    }

    private IEnumerator Heal(APlayer player)
    {
        //float curBrightness = minBrightness;
        //float targetBrightness = maxBrightness;
        float curTime = 0;
        while (true)
        {
            yield return null;

            curTime = (curTime + Time.deltaTime * animationSpeed) % 1;
            SetBrightness(animationCurve.Evaluate(curTime));

            //curBrightness = Mathf.MoveTowards(curBrightness, targetBrightness, Time.deltaTime * animationSpeed);
            //if (curBrightness == targetBrightness)
            //{
            //    targetBrightness = minBrightness + maxBrightness - targetBrightness;
            //}
            //SetBrightness(curBrightness);

            player.Heal(Time.deltaTime * healingSpeed);
        }
    }

    private void SetBrightness(float brightness)
    {
        brightness = Mathf.Clamp01(brightness);
        Color.RGBToHSV(background.color, out float h, out float s, out float _);
        background.color = Color.HSVToRGB(h, s, brightness);
    }
}
