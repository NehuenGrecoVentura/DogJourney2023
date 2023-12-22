using UnityEngine;
using UnityEngine.AI;

public class Dog2022 : MonoBehaviour
{
    NavMeshAgent _myAgent;
    [SerializeField] Animator _myAnim;
    Vector3 _targetDistance;
    bool _goTo;
    [HideInInspector] public bool scared = false;
    public Transform[] scaredPoint;
    public float normalSpeed;
    public float runSpeed;
    public GameObject target;
    public float targetRadius;
    [SerializeField] AudioSource _audioTrolley;
    [SerializeField] float _distanceWithPlayer = 10f;

    private DogOrder2022 _player;
    bool _isFar = false;

    private void Awake()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<DogOrder2022>();
    }

    void Start()
    {
        _myAgent.speed = normalSpeed;
        _myAnim.SetBool("Idle", true);
    }

    void Update()
    {
        if (scared)
        {
            _myAgent.speed = runSpeed;
            int aux = Random.Range(0, 1);
            target.transform.position = scaredPoint[aux].transform.position;
            _myAgent.destination = target.transform.position;
            _targetDistance = target.transform.position - this.transform.position;
            if (_targetDistance.magnitude <= targetRadius)
            {
                scared = false;
                _myAgent.speed = normalSpeed;
            }

            if (_targetDistance.x <= 3) scared = false;
            if (_targetDistance.z <= 3) scared = false;
            _myAgent.speed = normalSpeed;

        }

        else
        {
            if (_goTo)
            {
                target.GetComponent<MeshRenderer>().enabled = true;
                _myAgent.destination = target.transform.position;
                _targetDistance = target.transform.position - transform.position;
                if (_targetDistance.magnitude <= targetRadius)
                {
                    _goTo = false;
                    target.GetComponent<MeshRenderer>().enabled = false;
                }
                AnimWalk(); // Puesto por Nehuen
            }
            else AnimIdle(); // Puesto por Nehuen
            if (_targetDistance.x == 0) _goTo = false; // Puesto por Nehuen - Este es un fix que arregla el pasaje del GoTo a false. Ya que antes por más que llegara a destino no cambiaba el booleano. Ahora si.
            if (_targetDistance.z == 0) _goTo = false;
        }

        NearToPlayer();
    }

    public void OrderGO()
    {
        target.transform.position = _player.transform.position;
        _goTo = true;
    }

    public void OrderStay()
    {
        target.transform.position = transform.position;
        _goTo = true;
    }

    private void AnimWalk()
    {
        _myAnim.SetBool("Idle", false); // Puesto por Nehuen - Los perros salen de su estado idle y van a caminar cuando hagamos la acción de llamar.
        _myAnim.SetBool("Walk", true); // Puesto por Nehuen
    }

    private void AnimIdle()
    {
        _audioTrolley.Stop();
        _myAnim.SetBool("Idle", true); // Puesto por Nehuen - Una vez que los perros llegan a su destino, dejan de caminar y pasan a su estado idle.
        _myAnim.SetBool("Walk", false); // Puesto por Nehuen
    }

    private void NearToPlayer() // Puesto por Nehuen. Esta función hace que el perro vaya directamente hacia el jugador si la distancia que los separa es mucha.
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        if (distanceToPlayer > _distanceWithPlayer && !_isFar)
        {
            OrderGO();
            _isFar = true;
            target.GetComponent<MeshRenderer>().enabled = false;
        }

        else if (distanceToPlayer !> _distanceWithPlayer && _isFar)
        {
            target.GetComponent<MeshRenderer>().enabled = false;
            _isFar = false;
        }
    }
}
