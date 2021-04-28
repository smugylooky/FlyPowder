using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private Stats npcStats;
    private Animator animator;
    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private bool NPCIsDead()
    {
        return npcStats.Health <= 0;
    }

    void ReciveDamage(int damage) 
    {
        npcStats.Health = npcStats.Health - damage;

    }
}
