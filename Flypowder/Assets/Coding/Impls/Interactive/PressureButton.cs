using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : Interactive
{
    [SerializeField]
    private Interactive target;
    [SerializeField]
    private float minimalForce = 1;
    [SerializeField]
    private bool onlyOneTime = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    public override void TurnOn()
    {
        base.TurnOn();
        target.TurnOn();
    }

    public override void TurnOff()
    {
        base.TurnOff();
        target.TurnOff();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "pushable")
        {
            TurnOn();
        }
    }

    void OnCollisionExit2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "pushable")
        {
            if (!onlyOneTime) 
            {
                TurnOff();
            }
        }
    }
}
