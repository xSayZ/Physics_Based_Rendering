using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("Open", true);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("Open", false);
    }
}
