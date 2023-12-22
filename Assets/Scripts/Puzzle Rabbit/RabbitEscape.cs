using UnityEngine;
using UnityEngine.AI;

public class RabbitEscape : MonoBehaviour
{
    public Transform escapePos;
    public float time = 2f;
    [HideInInspector] public bool escape = false;
    public WolfPatrolNew wolf;
    private int _index;
    public Transform[] waypoints;
    private Grabbable _grabbable;
    private Animator _myAnim;
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioSource _audioWolf;
    public AudioClip soundEscape;
    public AudioClip soundWolf;

    private void Start()
    {
        _grabbable = GetComponent<Grabbable>();
        _myAnim = GetComponent<Animator>();
    }


    private void Update()
    {
        Escape();    
    }

    void Escape()
    {
        if (escape)
        {
            _myAnim.SetBool("Run", true);
            _myAnim.SetBool("Idle", false);
            transform.position = new Vector3(transform.position.x, 0.137f, transform.position.z);
            _grabbable.BeReleased();
            if (transform.position != waypoints[_index].position) transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].position, time * Time.deltaTime);
            else _index = (_index + 1) % waypoints.Length;
            transform.LookAt(waypoints[_index].position);
            if (transform.position.x <= -43.55f) Destroy(gameObject);
        }

        if (wolf._rabbitDetected) escape = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name=="Escape Rabbit" && Input.GetKeyDown(KeyCode.F))
        {
            escape = true;
            _myAudio.PlayOneShot(soundEscape);
            _audioWolf.PlayOneShot(soundWolf);
        }        
    }
}