using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using DG.Tweening;

public class player_mov : MonoBehaviour
{
    [Header("Rigidbody")]
    private Rigidbody2D rb;
    public int direction;
    public int oldDirection;
    public int directionPar;

    [Header("Movement")]
    public int speed = 10;
    public LayerMask groundLayer;
    private Animator anim;

    [Header("Jump")]
    public float jumpForce = 500f;
    public float fallMultiplier = 3f;
    public float lowJumpMultiplier = 2f;
    public bool isGrounded;
    public bool isWallSliding;
    public bool jumped = false;

    [Header("Coyote Time")]
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [Header("Dash")]
    public float _dashingVelocity = 15f;
    public float _dashingTime = 0.5f;
    public Vector2 _dashingDir;
    public bool _isDashing;
    public bool _canDash = true;
    private TrailRenderer _trailRen;
    GameObject wall_slide;

    void Start()
    {
        wall_slide = GameObject.FindGameObjectWithTag("wall_slide");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        _trailRen = GetComponentInChildren<TrailRenderer>();
    }

    void Move()
    {
        directionPar = direction;

        if (direction == 1)
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        else if (direction == -1)
            GetComponentInChildren<SpriteRenderer>().flipX = true;

        if (directionPar == 0)
            anim.SetFloat("directionPar", 0);
        else if (directionPar != 0 && isGrounded)
            anim.SetFloat("directionPar", 1);
    }

    void BetterJump()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * (fallMultiplier - 1) * Time.deltaTime * Physics2D.gravity.y;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
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

    void WallSlide()
    {
        isWallSliding = wall_slide.GetComponent<wall_slide>().isWalled;

        if (isWallSliding)
        {
            if (Input.GetKey(KeyCode.X))
                rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Clamp(rb.linearVelocityY, 6f, float.MaxValue));
            else
                rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Clamp(rb.linearVelocityY, -1f, float.MaxValue));
        }
    }

    void Update()
    {
        // Raycast za detekciju tla
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1, groundLayer);

        if (hit.collider != null)
        {
            isGrounded = true;
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            isGrounded = false;
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (isGrounded)
        {
            _canDash = true;
            jumped = false;
        }

        WallSlide();
        BetterJump();

        direction = (int)Input.GetAxisRaw("Horizontal");
        if (direction != 0)
            oldDirection = direction;

        Move();

        // DASH
        if (Input.GetKeyDown(KeyCode.Z) && _canDash)
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
            rb.linearVelocity = new Vector2(_dashingDir.x * _dashingVelocity, _dashingDir.y * _dashingVelocity / 2);
            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(.4f, 0.55f, 20, 180f, false, true);
            return;
        }
        else
        {
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        }

        // SKOK
        if ((Input.GetKeyDown(KeyCode.UpArrow) && coyoteTimeCounter > 0f && !_isDashing && !jumped) ||
            (Input.GetKeyDown(KeyCode.UpArrow) && !jumped && isWallSliding))
        {
            anim.SetTrigger("isJumping");
            jumped = true;
            coyoteTimeCounter = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        }
    }
}
