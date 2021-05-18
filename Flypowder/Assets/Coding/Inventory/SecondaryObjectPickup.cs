using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryObjectPickup : MonoBehaviour
{
    public SecondaryObjectBase secondary;
    private SpriteRenderer sprite;

    void Start()
    {
        if (secondary == null)
        {
            Destroy(gameObject);
        }

        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.sprite = secondary.objectSprite;
    }

    public SecondaryObjectBase getSecondary()
    {
        return secondary;
    }
}
