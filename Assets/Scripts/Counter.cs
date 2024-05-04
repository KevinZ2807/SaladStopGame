using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private Spawner aSpawner;
    
    void Start()
    {
        aSpawner = GetComponent<Spawner>();
    }

    void Update()
    {
        
    }

    
}
