using UnityEngine;
using UnityEngine.AI;

public class WolfLvl2 : MonoBehaviour
{
    public GameObject target;
    public Transform[] savedPoint;
    public NavMeshAgent _Agent;
    public bool onSight;
    public int Index;
    public float TargetRadius;
    public Vector3 TargetDistance;
    public GameObject player;
    public GameObject alertIcon;
    private float _speed = 1f;
    private AudioSource _myAudio;
    public AudioClip soundDetected;
    public ConeVision coneVision;

    void Start()
    {
        _myAudio = GetComponent<AudioSource>();
        alertIcon.gameObject.SetActive(false);
    }

    void Update()
    {
        if (coneVision.detected) chase();
        if (!coneVision.detected) patrol();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") _myAudio.PlayOneShot(soundDetected);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            alertIcon.gameObject.SetActive(true);
            onSight = true;
            print("DETECTADOOOOOOOOO");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            alertIcon.gameObject.SetActive(false);
            onSight = false;
            print("El lobo detectó al jugador");
        }
    }

    public void patrol()
    {
        target.transform.position = savedPoint[Index].position;
        _Agent.destination = target.transform.position;
        TargetDistance = target.transform.position - this.transform.position;
        if (TargetDistance.magnitude <= TargetRadius)
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
