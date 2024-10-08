using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startanim : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("12"); 
    }
}