using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Secundarias/Dinamita")]
public class SecondaryDynamite : SecondaryObjectBase
{
    public float weight;
    public bool activable;
    public float detonationTime;
    public bool destroyOnImpact;
    public float explosionRadius;
    public float explosionForce;

}
