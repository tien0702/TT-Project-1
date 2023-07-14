using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDashState : StateMachineBehaviour
{
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    CharacterData characterData;
    Rigidbody2D rb2d;
    Animator animator;
    SpriteRenderer spriteRenderer;
    float originGravity;
    float elapsedTime;

    bool isDashing;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
        rb2d = animator.GetComponent<Rigidbody2D>();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        characterData = animator.GetComponent<Character>().characterData;
        GroundCheck = animator.transform.Find("GroundCheck");
        GroundLayer = LayerMask.GetMask("Ground");
        PrepareDash();
        animator.SetBool("Dashing", false);
        characterData.DidDash = true;

        AudioManager.Instance.PlaySFX("Dash");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        elapsedTime += Time.deltaTime;
        if (!isDashing && elapsedTime >= characterData.DashBootTime) Dash();
        if (!IsOutTime()) return;
        if (!IsGrounded()) 
            animator.SetBool("FallDoubleJump", true);
        else animator.SetTrigger("Idle");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb2d.velocity = Vector2.zero;
        rb2d.gravityScale = originGravity;
        animator.SetBool("Dashing", false);
        spriteRenderer.enabled = true;
    }
    void PrepareDash()
    {
        originGravity = rb2d.gravityScale;
        elapsedTime = 0;

        spriteRenderer.enabled = false;
        rb2d.gravityScale = 0;
        isDashing = false;
    }
    void Dash()
    {
        rb2d.velocity = new Vector2(animator.transform.localScale.x * characterData.DashPower, 0);
        isDashing = true;
    }
    bool IsOutTime()
    {
        return elapsedTime >= (characterData.DashTime + characterData.DashBootTime);
    }
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.3f, GroundLayer);
    }
}
