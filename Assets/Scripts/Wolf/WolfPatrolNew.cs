using UnityEngine;
using UnityEngine.AI;

public class WolfPatrolNew : MonoBehaviour
{
    [Header("CONFIG PATROL")]
    public GameObject target;
    public Transform[] savedPoint;
    private NavMeshAgent _Agent;
    private bool _onSight;
    private int Index;
    public float TargetRadius;
    private Vector3 _targetDistance;  
    public DogOrders dogOrder;
    public GameObject alertIcon;  
    public WolfPatrolNew _patrol; // AGREGADOR POR NEHUEN
    [HideInInspector]
    public bool _rabbitDetected = false;
    private AudioSource _myAudio;
    [Header("DETECTION PLAYER")]
    public GameObject player;
    public AudioClip soundDetected;
    [Header("DETECTION DOG")]
    public AudioSource audioDog;
    public AudioClip dogScarySound;
    [Header("DETECTION RABBIT")]
    public RabbitEscape _rabbit; // AGREGADOR POR NEHUEN
    public WolfChaseRabbit _chaseRabbit; // AGREGADOR POR NEHUEN
    public AudioSource audioRabbit;
    public AudioClip soundRabbit;
    public AudioClip soundGrunt;
    

    void Start()
    {
        _chaseRabbit.enabled = false;
        alertIcon.gameObject.SetActive(false);
        _myAudio = GetComponent<AudioSource>();
        _Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_onSight) chase();
        if (!_onSight) patrol();
        if (_rabbit.escape || _rabbitDetected) // AGREGADO POR NEHUEN
        {
            _chaseRabbit.enabled = true;
            _patrol.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            alertIcon.gameObject.SetActive(true);
            _onSight = true;
            _myAudio.PlayOneShot(soundDetected);
            audioDog.PlayOneShot(dogScarySound);
            print("El lobo detectó al jugador");
            dogOrder.dogFlee();
        }

        if (other.gameObject.layer == 9)
        {
            var Test = other.GetComponent<DogTest>();
            if (Test != null)
            {
                Test.Scared = true;
                Debug.Log("The wolf Scared The dog");
            }
            audioDog.PlayOneShot(dogScarySound);
        }

        if (other.gameObject.name == "Rabbit")
        {
            _myAudio.PlayOneShot(soundGrunt);
            audioRabbit.PlayOneShot(soundRabbit);
            _rabbitDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            alertIcon.gameObject.SetActive(false);
            _onSight = false;
            print("El lobo detectó al jugador");
        }

        if (other.gameObject.layer == 9)
        {
            var Test = other.GetComponent<DogTest>();
            if (Test != null)
            {
                Test.Scared = false;
                Debug.Log("The wolf is not longer scaring The dog");
            }
        }
    }

    public void patrol()
    {
        target.transform.position = savedPoint[Index].position;
        _Agent.destination = target.transform.position;
        _targetDistance = target.transform.position - this.transform.position;
        if (_targetDistance.magnitude <= TargetRadius)
        {
            if (Index >= savedPoint.Length - 1) Index = 0;
            else Index++;
        }
    }

    public void chase()
    {
        target.transform.position = player.transform.position;
        _Agent.destination = target.transform.position;
    }
}
