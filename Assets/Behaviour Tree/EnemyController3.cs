using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController3 : StateController2
{
    public float AttackDistance;
    public float HP;
    private float nextHurt = 0;
    public Navigation nav;

    private void Start()
    {
        nav = GetComponent<Navigation>();
    }
    void Update()
    {
        StateTransition();
        if(currentState.action!=null)   currentState.action.OnUpdate();
        if (Input.GetKey("space") && Time.time >= nextHurt)
        {
            OnHurt(1);
            nextHurt = Time.time + 0.3f;
        }
    }

    public void OnHurt(float damage)
    {
        HP -= damage;
    }

    private void OnTriggerEnter(Collider collision)
    {
        target = collision.gameObject;
    }
    private void OnTriggerExit(Collider collision)
    {
        target = null;
        nav.UpdateDestination();
    }
}
