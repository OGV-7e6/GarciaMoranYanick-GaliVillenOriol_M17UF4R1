using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "ScriptableFollow", menuName = "ScriptableObjects2/ScriptableAction/ScriptableFollow")]

public class ScriptableFollow : ScriptableAction
{
    private ChaseBehaviour _chaseBehaviour;
    private EnemyController3 _enemyController;
    private NavMeshAgent agent;
    public override void OnFinishedState()
    {
        _chaseBehaviour.StopChasing();
    }

    public override void OnSetState(StateController2 sc)
    {
        base.OnSetState(sc);
       // GameManager.gm.UpdateText("Te persigo");
        _chaseBehaviour = sc.GetComponent<ChaseBehaviour>();
        _enemyController = (EnemyController3)sc;
    }

    public override void OnUpdate()
    {
        agent = sc.GetComponent<NavMeshAgent>();
        agent.destination = _enemyController.target.transform.position;
    }
}
