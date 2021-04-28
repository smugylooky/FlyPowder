using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private float health = 10;
    [SerializeField]
    private float damage = 1;
    [SerializeField]
    private float defense = 1;

    public float Health { get; set; }
    public float Damage { get; set; }
    public float Defense { get; set; }
   
    public NPCState state = NPCState.Alive;
    public enum NPCState 
    { 
        Dead,
        Alive
    }


}
