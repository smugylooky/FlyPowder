using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private Stats npcStats;
    private Animator animator;
    private GameObject player;

    private float minimalDistance = 3;

    private float lastAttack = 0f;
    private float timeOut = 3f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsInRange() && HasSpentTimeOut()) 
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        lastAttack = Time.time;
        //player.GetComponent<PlayerManager>().RecieveDamage(npcStats.Damage);
        animator.SetTrigger("Attack");
    }

    private bool HasSpentTimeOut()
    {
        return Time.time - lastAttack > timeOut;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            WeaponManager actualWeapon = player.GetComponentInChildren<WeaponManager>();
            ReciveDamage(5);
            if (NPCIsDead())
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    private bool PlayerIsInRange() 
    {
        return Vector2.Distance(transform.position, player.transform.position) <= minimalDistance;
    }
    private bool NPCIsDead()
    {
        return npcStats.Health <= 0;
    }

    void ReciveDamage(int damage) 
    {
        npcStats.Health = npcStats.Health - damage;

    }
}
