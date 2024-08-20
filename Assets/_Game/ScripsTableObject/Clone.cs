using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Clone : MonoBehaviour
{
    private NavMeshAgent agent;
    public float moveSpeed = 3f;
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        StartCoroutine(MoveAround());
    }

    private IEnumerator MoveAround()
    {
        while (true)
        {
            float moveTime = Random.Range(1f,3f);
            Vector3 randomPosition = GetRandomPositionOnNavMesh();
            agent.SetDestination(randomPosition);

            yield return new WaitForSeconds(moveTime);
        }
    }

    private Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomPosition = Random.insideUnitSphere * 10f;
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + randomPosition, out hit, 10f, NavMesh.AllAreas);

        return hit.position;
    }
}