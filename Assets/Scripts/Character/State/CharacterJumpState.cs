using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jump State", menuName = "Character State/Jump State")]
public class CharacterJumpState : StateMachineBehaviour
{
    float inputX;
    Rigidbody2D rb2d;
    Animator animator;
    GameObject fxPrefabs;
    GameObject fx;
    CharacterData charData;
    bool isLoaded = false;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!isLoaded) LoadComponent(animator, stateInfo, layerIndex);
        rb2d.AddForce(new Vector2(rb2d.velocity.x, charData.JumpPower), ForceMode2D.Impulse);
        AudioManager.Instance.PlaySFX("Jump");
        fx = Instantiate(fxPrefabs, animator.transform.position, Quaternion.identity, null);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CheckDoubleJump()) animator.SetTrigger("DoubleJump");
        else if (CheckDash()) animator.SetBool("Dashing", true);
        else if (CheckFall()) animator.SetBool("Falling", true);

        inputX = Input.GetAxis("Horizontal");
        float newSpeed = CalculateNewSpeed();
        rb2d.velocity = new Vector2(newSpeed, rb2d.velocity.y);
        Flip();
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Jumping", false);
    }
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        Debug.Log("Entered state machine");
    }
    float CalculateNewSpeed()
    {
        float currentSpeed = rb2d.velocity.x;
        float targetSpeed = inputX * charData.AirJumpSpeed * Time.fixedDeltaTime;
        float newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, charData.AirJumpRate);

        return newSpeed;
    }
    private void Flip()
    {
        if (inputX == 0) return;
        float scaleVal = (inputX > 0) ? (1) : (-1);
        Vector3 newScale = new Vector3(scaleVal, animator.transform.localScale.y, animator.transform.localScale.y);
        animator.transform.localScale = newScale;
    }
    bool CheckFall()
    {
        return rb2d.velocity.y <= 0;
    }
    bool CheckDoubleJump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    bool CheckDash()
    {
        return Input.GetKeyDown(KeyCode.F) && !charData.DidDash;
    }

    void LoadComponent(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isLoaded = true;
        rb2d = animator.GetComponent<Rigidbody2D>();
        this.animator = animator;
        charData = animator.GetComponent<Character>().characterData;
        fxPrefabs = Resources.Load<GameObject>("Prefabs/Fx/FXJump");
    }
}
