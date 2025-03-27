using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEditor.Purchasing;


[RequireComponent(typeof(NavMeshAgent))]
public class AgentLinkMover : MonoBehaviour
{
    public AnimationCurve m_Curve = new AnimationCurve();
    ZombieManager zombieManager;

    public float jumpHeight = 2.0f; // 기본 높이
    public float jumpDuration = 1.0f;
    private float jumpScaleFactor = 0.6f; // 점프 높이 보정 계수

    Animator animator;

    IEnumerator Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
        animator = GetComponent<Animator>();
        zombieManager = GetComponent<ZombieManager>();

        while (true)
        {
            if (agent.isOnOffMeshLink)
            {
                yield return StartCoroutine(Curve(agent, jumpDuration));
                agent.CompleteOffMeshLink();
            }
            yield return null;
        }
    }

    IEnumerator Curve(NavMeshAgent agent, float duration)
    {
        zombieManager.isJumping = true;

        // 점프 애니메이션 실행
        animator.Play("ZombieJump");

        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up;
        float normalizedTime = 0.0f;

        while (normalizedTime < 1.0f)
        {
            float yOffset = m_Curve.Evaluate(normalizedTime) * jumpHeight * jumpScaleFactor;
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        animator.Play("ZombieIdle");

        zombieManager.isJumping = false;

    }
}