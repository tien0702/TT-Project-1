using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Run State", menuName = "Character State/Run State")]
public class CharacterRunState : StateMachineBehaviour
{
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;

    float inputX;
    Rigidbody2D rb2d;
    Animator animator;
    CharacterData charData;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb2d = animator.GetComponent<Rigidbody2D>();
        this.animator = animator;
        charData = animator.GetComponent<Character>().characterData;
        GroundCheck = animator.transform.Find("GroundCheck");
        GroundLayer = LayerMask.GetMask("Ground");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CheckDash()) animator.SetBool("Dashing", true);
        else if (CheckFall()) animator.SetBool("Falling", true);
        else if (CheckJump()) animator.SetBool("Jumping", true);
        inputX = Input.GetAxis("Horizontal");
        float newSpeed = CalculateNewSpeed();
        rb2d.velocity = new Vector2(newSpeed, rb2d.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        Flip();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("Speed", 0f);
    }
    bool CheckDash()
    {
        return Input.GetKeyDown(KeyCode.F) && !charData.DidDash;
    }
    float CalculateNewSpeed()
    {
        float currentSpeed = rb2d.velocity.x;
        float targetSpeed = inputX * charData.RunSpeed * Time.fixedDeltaTime;
        float newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, charData.RunRate);

        return newSpeed;
    }
    private void Flip()
    {
        if (inputX == 0) return;
        float scaleVal = (inputX > 0) ? (1) : (-1);
        Vector3 newScale = new Vector3(scaleVal, animator.transform.localScale.y, animator.transform.localScale.y);
        animator.transform.localScale = newScale;
    }

    bool CheckJump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    bool CheckFall()
    {
        return rb2d.velocity.y <= 0 && !IsGrounded();
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.3f, GroundLayer);
    }
}
