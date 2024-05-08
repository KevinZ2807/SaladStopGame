using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QueueManager : MonoBehaviour
{

    public static QueueManager Instance { get; private set; }
    [SerializeField] private List<Transform> queuePositions = new List<Transform>();
    private List<NPC_script> npcsInQueue = new List<NPC_script>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Transform RequestPosition(NPC_script npc)
    {
        if (npcsInQueue.Count < queuePositions.Count)
        {
            npcsInQueue.Add(npc);
            return queuePositions[npcsInQueue.IndexOf(npc)];
        }
        return null;
    }

    public void ReleasePosition(NPC_script npc)
    {
        int index = npcsInQueue.IndexOf(npc);
        if (index != -1)
        {
            npcsInQueue.RemoveAt(index);
            npc.OnLeaveQueue();
            // Update remaining NPCs' positions
            for (int i = index; i < npcsInQueue.Count; i++)
            {
                npcsInQueue[i].MoveToPosition(queuePositions[i].position);
            }
        }
    }

    public bool IsFirstInQueue(NPC_script npc)
    {
        return npcsInQueue.IndexOf(npc) == 0;
    }
}
