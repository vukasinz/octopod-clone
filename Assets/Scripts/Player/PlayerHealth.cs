using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
     Slider healthBar;
    public float hp = 100f;
     GameObject traps;
    GameObject cerberus;
    private void Start()
    {
        traps = GameObject.FindGameObjectWithTag("traps");
      
      
         
        
        cerberus = GameObject.FindGameObjectWithTag("Cerberus");
    }
    public void TakeDamage(float amount)
    {
        hp -= amount;
        healthBar = GameObject.FindGameObjectWithTag("health_bar").GetComponent<Slider>();
        StartCoroutine(FlashColor(Color.gray));
    }
    IEnumerator FlashColor(Color flashColor, float duration = 0.1f)
    {
        SpriteRenderer sr = this.GetComponentInChildren<SpriteRenderer>();
        Color original = sr.color;
        sr.color = flashColor;
        yield return new WaitForSeconds(duration);
        sr.color = original;
    }
    // Update is called once per frame
    void Update()
    {

        if (healthBar != null)
        {
            healthBar.value = hp;
        }
        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        traps.GetComponent<traps>().StartCoroutine("SpawnAnotherPlayer");
        hp = 100f;
        cerberus.GetComponent<BossHealth>().health = 100f;
        traps = GameObject.FindGameObjectWithTag("traps");
        healthBar = GameObject.FindGameObjectWithTag("health_bar").GetComponent<Slider>();
    }
}
