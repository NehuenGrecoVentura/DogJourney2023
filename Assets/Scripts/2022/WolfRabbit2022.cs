using UnityEngine;
using UnityEngine.AI;

public class WolfRabbit2022 : MonoBehaviour
{
    public BoxCollider colliderRockAxe;
    public BoxCollider colliderDogRock;
    public GameObject target;
    public Transform rabbitPos;
    public float time;
    public Transform[] waypoints;
    NavMeshAgent _myAgent;
    int _index;

    void Start()
    {
        colliderDogRock.enabled = false;
        _myAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Destroy(colliderRockAxe);
        colliderDogRock.enabled = true;
        ChaseRabbit();
    }

    void ChaseRabbit()
    {
        Destroy(_myAgent);
        if (transform.position != waypoints[_index].position) transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].position, time * Time.deltaTime);
        else _index = (_index + 1) % waypoints.Length;
        transform.LookAt(waypoints[_index].position);
        if (transform.position.x < -39f) Destroy(gameObject);

    }
}
