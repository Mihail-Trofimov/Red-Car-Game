using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnim : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnMouseEnter()
    {
        anim.SetBool("Selected", true);
    }

    private void OnMouseExit()
    {
        anim.SetBool("Selected", false);
    }
}
