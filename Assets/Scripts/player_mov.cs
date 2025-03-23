using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class player_mov : MonoBehaviour
{
    [Header("Rigidbody")]
    public Rigidbody2D rb;
    public int direction;
    public int oldDirection;
    public int directionPar;
    [Header("Movement")]
    public int speed = 10;
    public LayerMask groundLayer;
    public Animator anim;
    [Header("Jump")]
    public float jumpForce = 500f;
    public float fallMultiplier = 3f;
    public float lowJumpMultiplier = 2f;
    public bool isGrounded;
    [Header("Dash")]
    public float _dashingVelocity = 15f;
    public float _dashingTime = 0.5f;
    public Vector2 _dashingDir;
    public bool _isDashing;
    public bool _canDash = true;
    public TrailRenderer _trailRen;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = this.GetComponentInChildren<Animator>();
        _trailRen = GetComponentInChildren<TrailRenderer>();
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
    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(_dashingTime);
        _isDashing = false;
        _trailRen.emitting = false;
    }
    void Update()
    {
        
         BetterJump();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1,groundLayer);
        if (hit.collider != null)
            isGrounded = true;
        else
            isGrounded = false;

        direction = (int)Input.GetAxisRaw("Horizontal");
        if(direction != 0)
            oldDirection = direction;
        Move();
        //DASH
        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            _isDashing = true;
            _canDash = false;
            _dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            _trailRen.emitting = true;
            if (_dashingDir == Vector2.zero)
                _dashingDir.x = oldDirection;
            anim.SetTrigger("isDashing");
            StartCoroutine(StopDashing());
        }
       
        if (_isDashing)
        {
            rb.linearVelocity = new Vector2(_dashingDir.x * _dashingVelocity, _dashingDir.y * _dashingVelocity/2);
            return;
        }
        else
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        //DASH
        if (isGrounded)
            _canDash = true;
        
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !_isDashing)
        { 
            anim.SetTrigger("isJumping");
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        }
      
    }
}
