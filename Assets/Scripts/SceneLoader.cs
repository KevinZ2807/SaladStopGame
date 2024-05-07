using System.Collections;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    [SerializeField] private Animator animator;

    void Start() {
        instance = this;
        animator = GetComponent<Animator>();
    }

    public void StartTranstition() {
        animator.SetTrigger("ChangeScene");
    }
}
