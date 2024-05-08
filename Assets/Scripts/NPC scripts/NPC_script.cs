using UnityEngine;
using UnityEngine.AI;

public class NPC_script : MonoBehaviour
{
    private Transform currentTarget;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isReceivedFood = false;
    private bool isIdle = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        FindQueue();
    }
    
    public void FindQueue() {
        currentTarget = QueueManager.Instance.RequestPosition(this);
        
        if (currentTarget != null)
        {
            isIdle = false;
            MoveToPosition(currentTarget.position);
        }
        else
        {
            Debug.Log("No queue positions available");
        }
    }

    public void MoveToPosition(Vector3 targetPosition)
    {
        animator.SetBool("isRunning", true);
        agent.destination = targetPosition;
    }

    private void Update()
    {
        if (currentTarget != null && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("isRunning", false);
            if (isReceivedFood && QueueManager.Instance.IsFirstInQueue(this))
            {
                QueueManager.Instance.ReleasePosition(this);
                OnLeaveQueue();
            }
        }
    }

    public void OnLeaveQueue()
    {
        animator.SetBool("isRunning", true);
        agent.destination = GameManager.instance.leavingPosition.transform.position;
        Destroy(gameObject, 5f); // Or any other action when leaving the queue
    }

    public void ReceiveFood()
    {
        isReceivedFood = true;
    }
}
