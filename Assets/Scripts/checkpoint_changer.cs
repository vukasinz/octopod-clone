using UnityEngine;

public class checkpoint_changer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            traps.last_checkpoint = this.transform;
        }
    }
       
}
