using UnityEngine;

public class player_mov : MonoBehaviour
{
     Rigidbody2D rb;
    public int direction;
    public int speed = 10;
    public float jumpForce = 500f;
    public bool isGrounded;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = this.GetComponentInChildren<Animator>();
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
       
    }
    void Update()
    {
        direction = (int)Input.GetAxisRaw("Horizontal");
        if(direction == 0)
            anim.SetFloat("directionPar", 0);
        else
            anim.SetFloat("directionPar", 1);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
    }
}
