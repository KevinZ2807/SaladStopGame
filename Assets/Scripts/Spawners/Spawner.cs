using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform spawnPlace;
    [SerializeField] private GameObject spawnedObject;
    [SerializeField] private float minSpawnDelay;
    public int spawnedObjectCount = 0;
    // Start is called before the first frame update

    void Start() {
        animator = GetComponent<Animator>();
        spawnPlace = transform.GetChild(0).GetChild(0);
        StartCoroutine(Spawning(minSpawnDelay));
    }

    IEnumerator Spawning(float cooldown) {
        while (true) {
            animator.SetTrigger("OpenClose");
            GameObject spawning = Instantiate(spawnedObject, new Vector3(transform.position.x, -3f, transform.position.z), Quaternion.identity, transform.GetChild(1));
            spawning.transform.DOJump(new Vector3(spawnPlace.position.x, spawnPlace.position.y,
            spawnPlace.position.z), 2f, 1, 0.5f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(1f);
        }

    }
}
