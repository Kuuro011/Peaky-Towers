using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Animator anim;


    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask jumpableGround;

    // Direction of movement (X) / Horizontal movement
    private float dirX;
    [SerializeField] private float movementSpd = 7f;
    [SerializeField] private float jumpForce = 14f;
    private bool isFacingRight = true;

    //Wall Slide
    private bool isWallSlidable = true;
    private float wallSlidingSpeed = 2f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    //Wall Jump
    private bool isWallJumping;
    private float wallJumpDir;
    private float wallJumpTime = 0.2f;
    private float wallJumpCounter;
    private float wallJumpDuration = 0.4f;
    private Vector2 wallJumpPwr = new Vector2(8f, 16f);

    //Jump Buffer
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private bool isJumping;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    //Dashing
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private float dashSpd;
    [SerializeField] private float StartDashCount;
    private float dashCount;
    private int side;

    [SerializeField] TrailRenderer tr;

    private enum MovementState { Idle, Running, Jumping, Falling }
    MovementState state;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();

        dashCount = StartDashCount;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDashing)
        {
            return;
        }


        dirX = Input.GetAxisRaw("Horizontal");

        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }


        wallSlide();
        wallJump();
        Flip();

        UpdateAnimation();

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(dirX * movementSpd, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && dirX < 0f || !isFacingRight && dirX > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, jumpableGround);
    }


    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void wallSlide()
    {
        if (isWalled() && !isGrounded() && dirX != 0f)
        {
            isWallSlidable = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        else
        {
            isWallSlidable = false;
        }
    }

    private void wallJump()
    {
        if (isWallSlidable)
        {
            isWallJumping = false;
            wallJumpDir = -transform.localScale.x;
            wallJumpCounter = wallJumpTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpDir * wallJumpPwr.x, wallJumpPwr.y);
            wallJumpCounter = 0f;

            if (transform.localScale.x != wallJumpDir)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpDuration);
        }

    }
    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void UpdateAnimation()
    {
        // Idle = 0, Running = 1, Jumping = 2, Falling = 3

        if (dirX > 0f)
        {
            state = MovementState.Running;
        }

        else if (dirX < 0f)
        {
            state = MovementState.Running;
        }

        else
        {
            state = MovementState.Idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.Jumping;
        }

        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.Falling;
        }

        anim.SetInteger("state", (int)state);
    }
}
