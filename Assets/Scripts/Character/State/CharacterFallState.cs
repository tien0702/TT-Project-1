using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fall State", menuName = "Character State/Fall State")]
public class CharacterFallState : StateMachineBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    float inputX;
    Rigidbody2D rb2d;
    Animator animator;
    CharacterData charData;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        groundCheck = animator.transform.Find("GroundCheck");
        groundMask = LayerMask.GetMask("Ground");
        charData = animator.GetComponent<Character>().characterData;
        rb2d = animator.GetComponent<Rigidbody2D>();
        this.animator = animator;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CheckDash()) animator.SetBool("Dashing", true);
        else if (CheckDoubleJump()) animator.SetTrigger("DoubleJump");
        else if (CheckIlde()) animator.SetBool("Falling", false);

        inputX = Input.GetAxis("Horizontal");
        float newSpeed = CalculateNewSpeed();
        rb2d.velocity = new Vector2(newSpeed, rb2d.velocity.y);
        Flip();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Falling", false);
    }

    bool CheckIlde()
    {
        return IsGrounded();
    }
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundMask);
    }

    float CalculateNewSpeed()
    {
        float currentSpeed = rb2d.velocity.x;
        float targetSpeed = inputX * charData.FallSpeed * Time.fixedDeltaTime;
        float newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, charData.FallRate);

        return newSpeed;
    }
    private void Flip()
    {
        if (inputX == 0) return;
        float scaleVal = (inputX > 0) ? (1) : (-1);
        Vector3 newScale = new Vector3(scaleVal, animator.transform.localScale.y, animator.transform.localScale.y);
        animator.transform.localScale = newScale;
    }
    bool CheckDoubleJump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    bool CheckDash()
    {
        return Input.GetKeyDown(KeyCode.F) && !charData.DidDash;
    }
}
