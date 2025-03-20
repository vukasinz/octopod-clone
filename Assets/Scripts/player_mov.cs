using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class player_mov : MonoBehaviour
{
     Rigidbody2D rb;
    public int direction;
    public int directionPar;
    public int speed = 10;
    public float jumpForce = 500f;
    public float dashTimer = 3f;
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
    public void dashRegulator()
    {
        if (!canDash)
        {
            if (dashTimer > 0)
                dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                canDash = true;
                dashTimer = 3f;
                
            }
        }

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
   IEnumerator dashColor()
    {
       Color original = this.GetComponentInChildren<SpriteRenderer>().color;
        Color target = original;
        target.a = 0.5f;
        this.GetComponentInChildren<SpriteRenderer>().color = target;
        yield return new WaitForSeconds(0.7f);
        this.GetComponentInChildren<SpriteRenderer>().color = original;
    }
   
    void Update()
    {
        DashOnOff();
        dashRegulator();
        direction = (int)Input.GetAxisRaw("Horizontal");
        if(!isDashing)
            Move();
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("isHitting");
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetTrigger("isJumping");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }
        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            canDash = false;
            rb.AddForce(new Vector2(direction * 650f,0), ForceMode2D.Force);
            isDashing = true;
            StartCoroutine("dashColor");

            anim.SetTrigger("isDashing");

        }
    }
}
