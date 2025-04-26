using UnityEngine;

public class Spring : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionStay2D(Collision2D collision)
    {
        
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(Vector2.up * 500f);

            rb.AddTorque(100f);
        }
        this.GetComponent<Animator>().SetBool("spring", true);
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        this.GetComponent<Animator>().SetBool("spring", false);
    }
}
