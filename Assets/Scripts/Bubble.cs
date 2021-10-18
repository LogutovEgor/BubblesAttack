using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Bubble : MonoBehaviour
{
    [SerializeField]
    private BubbleCharacteristics bubbleCharacteristics = default;
    [SerializeField]
    private float currentHealth = default;

    private SpriteRenderer spriteRenderer = default;

    private void Awake()
    {
        currentHealth = bubbleCharacteristics.Health;
        spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;
        UpdateSpriteColor();
        if (currentHealth <= 0)
            Destroy(gameObject);
    }

    private void UpdateSpriteColor()
    {
        Color color = Color.Lerp(bubbleCharacteristics.DeadColor, bubbleCharacteristics.HealthyColor, currentHealth / bubbleCharacteristics.Health);
        spriteRenderer.color = color;
    }
}
