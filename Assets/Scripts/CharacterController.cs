using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Properties
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float rate;

    [Header("Dash")]
    [SerializeField] private float dashPower;
    [SerializeField] private float dashCountdown;
    [SerializeField] private float dashTime;
    [SerializeField] private KeyCode dashKey;
    bool isDashing;
    bool canDash;

    [Header("Jump")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float fallMuiltiplier;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpMuiltiplier;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private KeyCode jumpKey;

    bool isJumping;
    float jumpCounter;
    Vector2 vecGravity;
    // component
    Rigidbody2D rb2d;
    Animator animator;
    float inputMove;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Dash
        isDashing = false;
        canDash = true;
        dashKey = KeyCode.F;

        // Jump
        GroundCheck = transform.Find("GroundCheck");
        GroundLayer = LayerMask.GetMask("Ground");
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        jumpKey = KeyCode.Space;
    }

    void Update()
    {
        if (isDashing) return;
        HandleJump();
        HandleHoldJump();
        HandleFall();
        HandleUp();

        inputMove = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(dashKey) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) return;
        float newSpeed = CalculateNewSpeed();
        rb2d.velocity = new Vector2(newSpeed, rb2d.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        Flip();
    }

    private void Flip()
    {
        if (inputMove == 0) return;
        float scaleVal = (inputMove > 0) ? (1) : (-1);
        transform.localScale = new Vector3(scaleVal, transform.localScale.y, transform.localScale.y);
    }

    float CalculateNewSpeed()
    {
        float currentSpeed = rb2d.velocity.x;
        float targetSpeed = inputMove * speed * Time.fixedDeltaTime;
        float newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, rate);

        return newSpeed;
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0;
        rb2d.velocity = new Vector2(transform.localScale.x * dashPower, 0);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        rb2d.gravityScale = originalGravity;
        rb2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashCountdown);
        canDash = true;
    }

    // Jump
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.3f, GroundLayer);
    }

    void HandleJump()
    {
        if (!(Input.GetKeyDown(jumpKey) && IsGrounded())) return;

        rb2d.AddForce(new Vector2(rb2d.velocity.x, jumpPower), ForceMode2D.Impulse);
        isJumping = true;
        animator.SetBool("Jumping", true);
        jumpCounter = 0;
    }
    void HandleHoldJump()
    {
        if (!(rb2d.velocity.y > 0 && isJumping)) return;

        jumpCounter += Time.deltaTime;
        if (jumpCounter > jumpTime) isJumping = false;

        float t = jumpCounter / jumpTime;
        float jumpM = jumpMuiltiplier;
        if (t > 0.5f)
        {
            jumpM = jumpMuiltiplier * (1f - t);
        }
        rb2d.velocity += vecGravity * jumpM * Time.deltaTime;
    }
    void HandleFall()
    {
        if (rb2d.velocity.y >= 0) return;
        rb2d.velocity -= vecGravity * fallMuiltiplier * Time.deltaTime;
        if (IsGrounded())
            animator.SetBool("Falling", true);
        else
        {
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", false);
        }
    }
    void HandleUp()
    {
        if (!Input.GetKeyUp(jumpKey)) return;
        isJumping = false;
        jumpCounter = 0;
        if (rb2d.velocity.y > 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.3f);
        }
    }
}
