using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float health = 100f;
    public State DeathState;
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
