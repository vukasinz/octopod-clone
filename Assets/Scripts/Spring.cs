using UnityEngine;

public class Spring : MonoBehaviour
{
    public float _spring_force;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(Vector2.up * _spring_force,ForceMode2D.Impulse);

            rb.AddTorque(100f);
        }
        this.GetComponent<Animator>().SetBool("spring", true);
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        this.GetComponent<Animator>().SetBool("spring", false);
    }
}
