using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private float speed = default;
    [SerializeField]
    private float touchRadius = default;
    [SerializeField]
    private float touchExplosionRadius = default;
    [SerializeField]
    private float explosionForce = default;
    [SerializeField]
    private float damage = default;

    private Bubble draggedBubble = default;
    private bool DraggingBubble => draggedBubble != default;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D raycastHit2D = Physics2D.CircleCast(GetMouseWorldPosition(), touchRadius, Vector2.zero);
            if (raycastHit2D.transform.TryGetComponent<Bubble>(out Bubble bubble))
            {
                draggedBubble = bubble;
            }
            //RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll(mouseWorldPosition, touchExplosionRadius, Vector2.zero);
            //foreach (RaycastHit2D raycastHit2D in raycastHit2Ds)
            //    if (raycastHit2D.transform.TryGetComponent<Bubble>(out Bubble bubble))
            //    {
            //        AddExplosionForce(raycastHit2D.rigidbody, explosionForce, mouseWorldPosition, touchExplosionRadius);
            //        bubble.TakeDamage(damage);
            //    }
        } 
        if (Input.GetMouseButton(0) && DraggingBubble)
        {
            Vector2 direction = GetMouseWorldPosition() - (Vector2)draggedBubble.transform.position;
            draggedBubble.Rigidbody2D.AddForce(direction * speed, ForceMode2D.Force);
        }
        if (Input.GetMouseButtonUp(0))
        {
            draggedBubble = default;
        }
    }

    private Vector2 GetMouseWorldPosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
        Gizmos.DrawSphere(transform.position, touchExplosionRadius);
    }
}
