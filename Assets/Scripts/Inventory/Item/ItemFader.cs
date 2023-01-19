using DG.Tweening;
using Mono.Cecil;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemFader : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// became opaque
    /// </summary>
    public void FadeIn()
    {
        Color targetColor = new Color(1f, 1f, 1f, 1f);
        spriteRenderer.DOColor(targetColor, Settings.ItemFadeDuration);

    }

    /// <summary>
    /// became translucent
    /// </summary>
    public void FadeOut()
    {
        Color targetColor = new Color(1f, 1f, 1f, Settings.TargetAlpha);
        spriteRenderer.DOColor(targetColor, Settings.ItemFadeDuration);
    }
}
