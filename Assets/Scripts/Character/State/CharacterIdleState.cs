using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle State", menuName = "Character State/Idle State")]
public class CharacterIdleState : StateMachineBehaviour
{
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;

    Character character;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GroundCheck = animator.transform.Find("GroundCheck");
        GroundLayer = LayerMask.GetMask("Ground");
        character = animator.GetComponent<Character>();
        UpdateDash();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CheckDash()) animator.SetBool("Dashing", true);
        else if (CheckJump()) animator.SetBool("Jumping", true);
        else if (CheckRun()) animator.SetFloat("Speed", 1f);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    bool CheckDash()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
    bool CheckRun()
    {
        return Input.GetAxis("Horizontal") != 0;
    }

    bool CheckJump()
    {
        return Input.GetKeyDown(KeyCode.Space) && IsGrounded();
    }
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.3f, GroundLayer);
    }

    void UpdateDash()
    {
        if (!character.characterData.DidDash) return;
        character.characterData.DidDash = !IsGrounded();
    }
}
