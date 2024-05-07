using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_script : MonoBehaviour
{
    public Transform entryPoint;
    public Transform exitPoint;
    public Transform orderPoint;
    public Transform seatPoint;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GoToOrderPoint();
    }

    void GoToOrderPoint()
    {
        animator.SetBool("isWalking", true);
        agent.SetDestination(orderPoint.position);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            animator.SetBool("isWalking", false);
            
            if (agent.destination == orderPoint.position)
            {
                // Simulate ordering food
                StartCoroutine(WaitAndEat(2)); // Wait for 2 seconds before sitting
            }
            else if (agent.destination == seatPoint.position)
            {
                // Simulate eating
                StartCoroutine(WaitAndPay(5)); // Wait for 5 seconds to simulate eating
            }
            else if (agent.destination == exitPoint.position)
            {
                Destroy(gameObject); // NPC leaves the restaurant
            }
        }
    }

    IEnumerator WaitAndEat(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GoToSeat();
    }

    void GoToSeat()
    {
        agent.SetDestination(seatPoint.position);
    }

    IEnumerator WaitAndPay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PayAndLeave();
    }

    void PayAndLeave()
    {
        // Simulate payment here
        GoToExit();
    }

    void GoToExit()
    {
        animator.SetBool("isWalking", true);
        agent.SetDestination(exitPoint.position);
    }
}
