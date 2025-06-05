using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float health = 100f;
    public State DeathState;
    [SerializeField] private float dashForce = 1000f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float dashChance = 0.4f; // 40%
    [SerializeField] private float triggerDistance = 3f;

    private float lastDashTime = -999f;
    private Rigidbody2D rb;
     Transform player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void TryDashAtPlayer()
    {
        if (Time.time - lastDashTime < dashCooldown) return;
        if (Random.value > dashChance) return;

        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * dashForce; // ? CORRECT
        lastDashTime = Time.time;
        Debug.Log("Boss dashed at player!");
    }
    void Update()
    {
        float dist = Vector2.Distance(player.position, transform.position);
        if (dist < triggerDistance)
        {
            TryDashAtPlayer();
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Boss took damage: " + amount);
        if (health <= 0)
        {
            Die();
        }
    }



    void Die()
    {
        Debug.Log("Boss died");
        StateManager sm = GetComponent<StateManager>();
        if (sm != null)
        {
            sm.currentState = DeathState;
            sm.currentState.EnterState();
        }
        else
        {
            Debug.LogError("StateManager component not found on the boss.");
        }
    }
}
