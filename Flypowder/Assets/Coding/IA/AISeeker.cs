using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISeeker : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Transform target;
    [SerializeField]
    private float stopDistance;
    [SerializeField]
    private float rangeOfView;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(target.position, this.transform.position);
        if (TargetIsInRange(distance))
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        if (target.position.x <= transform.position.x) 
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else 
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private bool TargetIsInRange(float distance)
    {
        return distance > stopDistance && distance <= rangeOfView;
    }
}
