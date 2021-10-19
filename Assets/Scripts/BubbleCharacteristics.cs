using UnityEngine;

[CreateAssetMenu(menuName = nameof(BubbleCharacteristics))]
public class BubbleCharacteristics : ScriptableObject
{
    [SerializeField]
    private GameObject bubblePrefab = default;
    public GameObject BubblePrefab => bubblePrefab;

    [SerializeField]
    private float health = default;
    public float Health => health;

    [SerializeField]
    private Color healthyColor = default;
    public Color HealthyColor => healthyColor;

    [SerializeField]
    private Color deadColor = default;
    public Color DeadColor => deadColor;

    [SerializeField]
    private GameObject destroyParticles = default;
    public GameObject DestroyParticles => destroyParticles;
}
