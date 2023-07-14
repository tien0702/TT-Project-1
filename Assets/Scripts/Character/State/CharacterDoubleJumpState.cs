using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDoubleJumpState : StateMachineBehaviour
{
    float inputX;
    float elapsedTime;
    Rigidbody2D rb2d;
    Animator animator;
    CharacterData charData;
    GameObject fxPrefabs;
    GameObject fx;
    bool isLoaded = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isLoaded) LoadComponent(animator, stateInfo, layerIndex);
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.AddForce(new Vector2(rb2d.velocity.x, charData.DoubleJumpPower), ForceMode2D.Impulse);
        elapsedTime = 0;

        AudioManager.Instance.PlaySFX("Jump");
        fx = Instantiate(fxPrefabs, animator.transform.position, Quaternion.identity, null);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        elapsedTime += Time.deltaTime;
        if (CheckDash()) animator.SetBool("Dashing", true);
        else if (CheckFall()) animator.SetBool("FallDoubleJump", true);
        inputX = Input.GetAxis("Horizontal");
        float newSpeed = CalculateNewSpeed();
        rb2d.velocity = new Vector2(newSpeed, rb2d.velocity.y);
        Flip();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
    void LoadComponent(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isLoaded = true;
        rb2d = animator.GetComponent<Rigidbody2D>();
        this.animator = animator;
        charData = animator.GetComponent<Character>().characterData;
        fxPrefabs = Resources.Load<GameObject>("Prefabs/Fx/FXJump");
    }
    float CalculateNewSpeed()
    {
        float currentSpeed = rb2d.velocity.x;
        float targetSpeed = inputX * charData.DoubleAirJumpSpeed * Time.fixedDeltaTime;
        float newSpeed = Mathf.Lerp(currentSpeed, targetSpeed, charData.DoubleAirJumpRate);

        return newSpeed;
    }
    bool CheckDash()
    {
        return Input.GetKeyDown(KeyCode.F) && !charData.DidDash;
    }
    bool CheckFall()
    {
        return (elapsedTime >= charData.TimeFly);
    }
    private void Flip()
    {
        if (inputX == 0) return;
        float scaleVal = (inputX > 0) ? (1) : (-1);
        Vector3 newScale = new Vector3(scaleVal, animator.transform.localScale.y, animator.transform.localScale.y);
        animator.transform.localScale = newScale;
    }
}
