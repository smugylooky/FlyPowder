using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : Interactive
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Interactive target;
    private State state = State.Off;
    [SerializeField]
    private float timer = -1;
    private float previusTime;
    SFXManager sfxManager;

    private enum State
    {
        On = 1,
        Off = 0
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sfxManager = SFXManager.Instance;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && PlayerIsInRange())
        {
            ChangeState();
            if (state == State.On)
            {
                TurnOn();
            }
            else
            {
                TurnOff();
            }
            
        }
        if (HasATimer() && TimerIsOut() && state == State.On) 
        {
            ChangeState();
            TurnOff();
            
        }
    }

    private bool TimerIsOut()
    {
        return Time.time - previusTime > timer;
    }

    private void ChangeState()
    {
        sfxManager.playSonidoCorrediza();
        sfxManager.playSonidoPalanca();
        
        if (state == State.On)
            state = State.Off;
        else state = State.On;
        previusTime = Time.time;
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 9) 
        {
            if (state == State.Off) 
            {
                ChangeState();
                TurnOn();
            }
        }
    }

    private bool PlayerIsInRange()
    {
        return Vector2.Distance(player.transform.position, this.transform.position) < 2f;
    }

    private bool HasATimer() 
    {
        return timer > 0;
    }
}
