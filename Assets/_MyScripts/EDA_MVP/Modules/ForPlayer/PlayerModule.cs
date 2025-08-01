using System.Collections;
using System.Collections.Generic;
using ModularEventArchitecture;
using UnityEngine;
using UnityEngine.AI;

public class PlayerModule : ModuleBase
{
    //-----------------------------------------------------------
    [Information("Этот модуль отвечает за передвижение игрока по карте. Если его удалить, то игрок не сможет перемещаться по карте.", InformationAttribute.InformationType.Info, false)]
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;
    private Coroutine _moveCoroutine;
    private GameEntity _targetEntity;
    //!-----------------------------------------------------------


    public override void Initialize()
    {
        Entity.SubscribeGlobalEvent<MoveToPointEvent>(MoveActions.Move_To_point, OnMoveTo);

        Entity.SubscribeGlobalEvent<SetTargetEvent>(MoveActions.Set_target, OnSetTarget);
    }

    private void OnSetTarget(SetTargetEvent setTargetEvent)
    {
        _targetEntity = setTargetEvent.Target;
    }

    public void OnMoveTo(MoveToPointEvent moveToPointEvent)
    {
        _navMeshAgent.SetDestination(moveToPointEvent.Point);

        _animator.SetBool("IsRunning", true);

        if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);

        _moveCoroutine = StartCoroutine(CheckArrival());
    }

    private IEnumerator CheckArrival()
    {
        while (_navMeshAgent.pathPending || _navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
        {
            yield return null;
        }

        _animator.SetBool("IsRunning", false);

        // вызов события использования объекта
        if (_targetEntity)
        { 
            _targetEntity.PublishLocalEvent<UseEvent>(MoveActions.Use_target, new UseEvent());
            
            _targetEntity = null; 
        }

    }
}