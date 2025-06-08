using System.Collections;
using UnityEngine;

public class hit : MonoBehaviour
{
    public float impactThreshold = 10f; // Minimum relative velocity to count as a hit
    public float flashDuration = 0.4f;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float impactSpeed = collision.relativeVelocity.magnitude;

            if (impactSpeed >= impactThreshold)
            {
                GameObject cerberus = GameObject.FindGameObjectWithTag("Cerberus");
                cerberus.GetComponent<BossHealth>().TakeDamage(Random.Range(5, 15f));
                Debug.Log($"Hit registered! Impact speed: {impactSpeed}");
                
            }
           
        }
    }
    
}