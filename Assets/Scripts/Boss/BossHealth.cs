using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float health = 100f;
    public float damageCooldown = 2f;
    public State DeathState;
    public Slider healthBar;

    private bool canTakeDamage = true;
    private Rigidbody2D rb;
    private Transform player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
       
    }
    private void FixedUpdate()
    {
        if (healthBar != null)
            healthBar.value = health;
    }

    public void TakeDamage(float amount)
    {
        if (!canTakeDamage) return;

        canTakeDamage = false;
        health -= amount;
        Debug.Log("Boss took damage: " + amount);
        StartCoroutine(FlashColor(Color.gray));
        StartCoroutine(DamageCooldown());

        if (health <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashColor(Color flashColor, float duration = 0.1f)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color original = sr.color;
        sr.color = flashColor;
        yield return new WaitForSeconds(duration);
        sr.color = original;
    }

    IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
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
