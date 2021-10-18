using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField]
    private float gravityRadius = default;
    [SerializeField]
    private float gravityPower = default;

    void FixedUpdate()
    {
        RaycastHit2D[] raycastHit2Ds = Physics2D.CircleCastAll(transform.position, gravityRadius, Vector2.zero);
        foreach(RaycastHit2D raycastHit2D in raycastHit2Ds)
        {
            Vector2 direction = transform.position - raycastHit2D.transform.position;
            raycastHit2D.rigidbody.AddForce(direction * gravityPower, ForceMode2D.Force);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, gravityRadius);
    }
}
