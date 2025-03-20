using UnityEngine;

public class player_mov : MonoBehaviour
{
     Rigidbody2D rb;
    public int direction;
    public int speed = 10;
    public bool isGrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // U
    // pdate is called once per frame
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        if (Mathf.Abs(rb.linearVelocityY) < 0.1f)
            isGrounded = true;
        else
            isGrounded = false;
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * 500f, ForceMode2D.Force);
        }
    }
    void Update()
    {
        direction = (int)Input.GetAxisRaw("Horizontal");
      
    }
}
