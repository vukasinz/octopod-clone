using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float health = 100f;
    public State DeathState;
 

    private Rigidbody2D rb;
     Transform player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            sm.SwitchToTheNextState(DeathState);
            sm.currentState.EnterState();
        }
        else
        {
            Debug.LogError("StateManager component not found on the boss.");
        }
    }
}
