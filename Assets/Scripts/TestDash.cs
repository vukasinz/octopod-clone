using UnityEngine;

public class TestDash : MonoBehaviour
{
    public float dashForce = 10f;
    Rigidbody2D rb;
    Transform player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Vector2 dir = (player.position - transform.position).normalized;
            rb.linearVelocity = dir * dashForce;
            Debug.Log("Dash velocity: " + rb.linearVelocity);
        }
    }
}