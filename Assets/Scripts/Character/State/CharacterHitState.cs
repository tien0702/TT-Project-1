using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitState : StateMachineBehaviour
{
    Rigidbody2D rb2d;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb2d = animator.GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        AudioManager.Instance.PlaySFX("TakeHit");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public void OnEnd()
    {

    }
}
