using UnityEngine;

public abstract class APlayer2D : APlayer
{
    protected SpriteRenderer spriteRenderer;
    protected Color defaultColor;

    protected override void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;

        base.Awake();
    }

    protected override void SetHealth(float xpAmount)
    {
        base.SetHealth(xpAmount);
        spriteRenderer.color = Color.Lerp(Color.black, defaultColor, health / MaxHP);
    }
}
