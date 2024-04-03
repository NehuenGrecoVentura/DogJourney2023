using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;

public class ModelDog
{
    private NavMeshAgent _agent;
    private Vector3 _targetDist;
    private bool _scared;
    private Transform[] _scaredPoints;
    private float _speedNormal, _speedRun,_offSpeed, _distToPlayer, _targetRadius;
    private GameObject _target;
    private Character _player;
    private Transform _myTransform;
    public event Action EventIdle, EventWalk;
    private OrderDog _order;
    private Transform _posTeletransport;

    public ModelDog(NavMeshAgent agent, Vector3 targetDist, bool scared, Transform[] scaredPoints, float speedNormal, float speedRun, float offSpeed, float distToPlayer, float targetRadius, GameObject target, 
        Transform transform, Character player, OrderDog order, Transform posTeletransport)
    {
        _agent = agent;
        _targetDist = targetDist;
        _scared = scared;
        _scaredPoints = scaredPoints;
        _speedNormal = speedNormal;
        _speedRun = speedRun;
        _distToPlayer = distToPlayer;
        _targetRadius = targetRadius;
        _target = target;
        _myTransform = transform;
        _player = player;
        _agent.speed = speedNormal;
        _order = order;
        _posTeletransport = posTeletransport;
        _offSpeed = offSpeed;
    }

    public void Scared()
    {
        if (_scared && _order.activeOrders)
        {
            _agent.speed = _speedRun;
            int aux = UnityEngine.Random.Range(0, 1);
            _target.transform.position = _scaredPoints[aux].transform.position;
            _agent.destination = _target.transform.position;
            _targetDist = _target.transform.position - _myTransform.position;

            if (_targetDist.magnitude <= _targetRadius)
            {
                _scared = false;
                _agent.speed = _speedNormal;
            }

            if (_targetDist.x <= 3) _scared = false;
            if (_targetDist.z <= 3) _scared = false;
            _agent.speed = _speedNormal;
        }
    }

    public IEnumerator OrderGO()
    {
        if (_order.activeOrders)
        {
            yield return new WaitForEndOfFrame();
            _target.GetComponent<MeshRenderer>().enabled = true;
            _target.transform.position = new Vector3(_player.transform.position.x, 0, _player.transform.position.z);

            while (true)
            {
                if (_target != null)
                {
                     //_agent.speed = _speedNormal;
                    _agent.destination = _target.transform.position;
                    _targetDist = _target.transform.position - _myTransform.position;
                    EventWalk?.Invoke();

                    if (_targetDist.magnitude <= _targetRadius)
                    {
                        EventIdle?.Invoke();
                        _target.GetComponent<MeshRenderer>().enabled = false;
                        yield return null;
                    }
                }

                yield return null;
            }
        }
    }

    public void OrderStay()
    {
        if (_order.activeOrders)
            _target.transform.position = _myTransform.position;
    }

    public void OffScreenSpeed()
    {
        
        
            float distanceToPlayer = Vector3.Distance(_myTransform.position, _player.gameObject.transform.position);
            if (distanceToPlayer >= _distToPlayer)
            {
                _agent.speed = _offSpeed;
            }
            if (distanceToPlayer <= _distToPlayer)
            {
                _agent.speed = _speedNormal;
            }
        
       
        
    }

    public void TeletransportToPlayer()
    {
        if (_order.activeOrders && _player.gameObject.transform.position.y < 17f)
        {
            float distanceToPlayer = Vector3.Distance(_myTransform.position, _player.gameObject.transform.position);
            if (distanceToPlayer > _distToPlayer)
            {
                _myTransform.position = _posTeletransport.position;
                _target.transform.position = _myTransform.transform.position;
            }
        }

        else _myTransform.position = _myTransform.position;
    }
}