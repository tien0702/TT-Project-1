using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] List<Transform> m_Points;
    [SerializeField] private float m_Speed;

    Transform m_TargetMove;
    int m_CurrentPointsIndex;

    Transform m_OldParent;
    void Start()
    {
        m_Points = new List<Transform>();
        foreach(Transform child in transform.parent)
        {
            if (child == this.transform) continue;

            m_Points.Add(child);
        }
        m_CurrentPointsIndex = 0;
        m_TargetMove = m_Points?[m_CurrentPointsIndex];
    }

    void Update()
    {
        UpdateTargetMove();
        Moving();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            m_OldParent = collision.transform.parent;
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = m_OldParent;
        }
    }

    void UpdateTargetMove()
    {
        if(Vector2.Distance(transform.position, m_TargetMove.position) < 0.1f)
        {
            NextPoint();
        }
    }

    void NextPoint()
    {
        m_CurrentPointsIndex++;
        if (m_CurrentPointsIndex >= m_Points.Count) m_CurrentPointsIndex = 0;
        m_TargetMove = m_Points[m_CurrentPointsIndex];
    }

    void Moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_TargetMove.position, m_Speed * Time.deltaTime);
    }
}
