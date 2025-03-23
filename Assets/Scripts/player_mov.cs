using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class player_mov : MonoBehaviour
{
    public Rigidbody2D rb;
    public int direction;
    public int directionPar;
    public int speed = 10;
    public float jumpForce = 500f;
    public float fallMultiplier = 3f;
    public float lowJumpMultiplier = 2f;

    float dashProgress = 0.7f;
    public bool isGrounded;
    public bool canDash = true;
    public bool isDashing = false;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = this.GetComponentInChildren<Animator>();
    }

    // pdate is called once per frame
    private void FixedUpdate()
    {
        if(!isDashing)
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        if (Mathf.Abs(rb.linearVelocityY) < 0.1f)
            isGrounded = true;
        else
            isGrounded = false;
      
    }
    public void Dash()
    {
        rb.AddForce(new Vector2(direction * 200f, 0), ForceMode2D.Force);
        
        isDashing = true;
        canDash = false;
        anim.SetTrigger("isDashing");
        StartCoroutine("dashColor");
    }

    private void DashOnOff()
    {
        if (isDashing)
        {
            if (dashProgress > 0)
                dashProgress -= Time.deltaTime;
            if (dashProgress <= 0)
            {
                dashProgress = 0.7f;
                isDashing = false;
                rb.linearVelocityX = 0;
            }
        }
    }
    IEnumerator dashColor()
    {
        Color original = this.GetComponentInChildren<SpriteRenderer>().color;
        Color target = original;
        target.a = 0.5f;
        this.GetComponentInChildren<SpriteRenderer>().color = target;
        yield return new WaitForSeconds(0.5f);
        this.GetComponentInChildren<SpriteRenderer>().color = original;
    }
    void Move()
    {
        directionPar = direction;
        if (direction == 1)
            this.GetComponentInChildren<SpriteRenderer>().flipX = false;
        else if (direction == -1)
            this.GetComponentInChildren<SpriteRenderer>().flipX = true;
        if (directionPar == 0)
            anim.SetFloat("directionPar", 0);
        else if(directionPar != 0 && isGrounded)
            anim.SetFloat("directionPar", 1);
    }

   void BetterJump()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * (fallMultiplier - 1) * Time.deltaTime * Physics2D.gravity.y;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump")) 
        {
            rb.linearVelocity += Vector2.up * (lowJumpMultiplier - 1) * Time.deltaTime * Physics2D.gravity.y;
        }
    }
    void Update()
    {
       BetterJump();
        DashOnOff();
        if (!canDash && isGrounded)
            canDash = true;
        direction = (int)Input.GetAxisRaw("Horizontal");
        if(!isDashing)
            Move();
   
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        { 
            anim.SetTrigger("isJumping");
            rb.linearVelocity = Vector2.up * jumpForce;
        }
        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            Dash();

        }
    }
}
