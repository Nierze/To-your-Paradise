using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimationHandler : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void isHovering(bool isHovering)
    {
        animator.SetBool("onHover", isHovering);
    }
}