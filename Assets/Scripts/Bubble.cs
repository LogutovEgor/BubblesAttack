using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bubble : MonoBehaviour
{
    [SerializeField]
    private BubbleCharacteristics bubbleCharacteristics = default;
    [SerializeField]
    private float currentHealth = default;

    private SpriteRenderer spriteRenderer = default;
    private new Rigidbody2D rigidbody2D = default;
    public Rigidbody2D Rigidbody2D => rigidbody2D;

    private void Awake()
    {
        currentHealth = bubbleCharacteristics.Health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;
        UpdateSpriteColor();
        if (currentHealth <= 0)
            Destroy();
    }

    private void UpdateSpriteColor()
    {
        Color color = Color.Lerp(bubbleCharacteristics.DeadColor, bubbleCharacteristics.HealthyColor, currentHealth / bubbleCharacteristics.Health);
        spriteRenderer.color = color;
    }


    private void Destroy()
    {
        GameObject particles = Instantiate(bubbleCharacteristics.DestroyParticles);
        particles.transform.position = transform.position;
        Destroy(gameObject);
    }
}
