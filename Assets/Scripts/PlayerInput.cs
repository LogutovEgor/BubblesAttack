using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private float touchRadius = default;
    [SerializeField]
    private float explosionForce = default;
    [SerializeField]
    private float damage = default;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll(mouseWorldPosition, touchRadius, Vector2.zero);
            foreach (RaycastHit2D raycastHit2D in raycastHit2Ds)
                if (raycastHit2D.transform.TryGetComponent<Bubble>(out Bubble bubble))
                {
                    AddExplosionForce(raycastHit2D.rigidbody, explosionForce, mouseWorldPosition, touchRadius);
                    bubble.TakeDamage(damage);
                }
        }
    }

    private void AddExplosionForce(Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Impulse)
    {
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        // Normalize without computing magnitude again
        if (upwardsModifier == 0)
            explosionDir /= explosionDistance;
        else
        {
            // From Rigidbody.AddExplosionForce doc:
            // If you pass a non-zero value for the upwardsModifier parameter, the direction
            // will be modified by subtracting that value from the Y component of the centre point.
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.25f);
        Gizmos.DrawSphere(transform.position, touchRadius);
    }
}
