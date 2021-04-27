using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    [SerializeField]
    protected string type;
    protected Animator animator;
    [SerializeField]
    protected SFXManager sfxManager;


    void Start() 
    {
        animator = GetComponent<Animator>();
    }

    public virtual void TurnOn() 
    {
        animator.SetTrigger("On");
        //sfxManager.PlaySfx(type);
    }

    public virtual void TurnOff() 
    {
        animator.SetTrigger("Off");
        //sfxManager.PlaySfx(type);
    }
}
