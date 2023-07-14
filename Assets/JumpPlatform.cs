using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField] private float m_JumpForce;

    Rigidbody2D m_Rigidbody;
    Animator m_Animator;

    Transform m_OldParent;
    bool isJumping;
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        if (!isJumping)
        {
            m_Animator.SetBool("IsJump", true);
            isJumping = true;
        }
        Debug.Log("enter");
        m_Rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        m_OldParent = collision.transform.parent;
        collision.transform.parent = transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        Debug.Log("exit");
        //m_Rigidbody = null;
        collision.transform.parent = m_OldParent;
    }

    public void TriggerJump()
    {
        //if (m_Rigidbody == null) return;
        Debug.Log("Add force");
        m_Rigidbody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
    }

    public void EndJumpAni()
    {
        isJumping = false;
        m_Animator.SetBool("IsJump", false);
    }
}
