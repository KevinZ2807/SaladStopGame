using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> NPCs = new List<GameObject>();
    void OnEnable() {
        StartCoroutine(Spawning());
    }
    void OnDisable() {
        StopCoroutine(Spawning());
    }

    IEnumerator Spawning() {
        while (true) {
            while (transform.GetChild(0).childCount > 9) {
                yield return new WaitForSeconds(1f);
            }
            int index = Random.Range(0, NPCs.Count);
            GameObject selectedNPC = NPCs[index];

            GameObject newNPC = Instantiate(selectedNPC, transform.position, transform.rotation, transform.GetChild(0));
            yield return new WaitForSeconds(3f);
             
        }
    }
}
