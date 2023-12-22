using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogTest : MonoBehaviour
{
    public NavMeshAgent nvm;
    public DogOrders _player;
    public NavMeshAgent _agent;
    public CapsuleCollider body;
    public GameObject target;
    public bool GoTo;
    public float TargetRadius;
    public Vector3 TargetDistance;
    public GameObject _particle;
    private Animator _anim; // Puesto por Nehuen
    public bool Scared;
    public Transform[] scaredPoint;
    public float normalSpeed;
    public float runSpeed;
    private AudioSource _myAudio;
    public AudioClip soundContactPlayer;
    public AudioClip strokeDog;
    
    void Start()
    {
        nvm = GetComponent<NavMeshAgent>();
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>(); // Puesto por Nehuen
        _anim.SetBool("Idle", true); // Puesto por Nehuen
        _agent.speed = normalSpeed;
        _myAudio = GetComponent<AudioSource>();
        body = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (Scared)
        {
            _agent.speed = runSpeed;
            int aux = Random.Range(0, 1);
            target.transform.position = scaredPoint[aux].transform.position;
            _agent.destination = target.transform.position;
            TargetDistance = target.transform.position - this.transform.position;
            if (TargetDistance.magnitude <= TargetRadius)
            {
                Scared = false;
                _agent.speed = normalSpeed;
            }

            if (TargetDistance.x <= 3) Scared = false;
            if (TargetDistance.z<=3) Scared = false;
            _agent.speed = normalSpeed;
        }
        else
        {
            if (GoTo)
            {
                _agent.destination = target.transform.position;
                TargetDistance = target.transform.position - this.transform.position;
                if (TargetDistance.magnitude <= TargetRadius)
                {
                    GoTo = false;
                }

                AnimWalk(); // Puesto por Nehuen
            }
            else AnimIdle(); // Puesto por Nehuen
            if (TargetDistance.x==0) GoTo = false; // Puesto por Nehuen - Este es un fix que arregla el pasaje del GoTo a false. Ya que antes por más que llegara a destino no cambiaba el booleano. Ahora si.
            if (TargetDistance.z==0) GoTo = false;
        }
        
    }

    public void OrderGO()
    {
        target.transform.position = _player.transform.position;
        GoTo = true;
    }

    public void OrderStay()
    {
        target.transform.position = this.transform.position;
        GoTo = true;
    }



    public void Pet()
    {
        GameObject currentParticle = Instantiate(_particle, transform.position + Vector3.up, Quaternion.identity);
        Destroy(currentParticle, 2f);
        _myAudio.PlayOneShot(strokeDog);
    }

    private void AnimWalk()
    {
        _anim.SetBool("Idle", false); // Puesto por Nehuen - Los perros salen de su estado idle y van a caminar cuando hagamos la acción de llamar.
        _anim.SetBool("Walk", true); // Puesto por Nehuen
    }

    private void AnimIdle()
    {
        _anim.SetBool("Idle", true); // Puesto por Nehuen - Una vez que los perros llegan a su destino, dejan de caminar y pasan a su estado idle.
        _anim.SetBool("Walk", false); // Puesto por Nehuen
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") _myAudio.PlayOneShot(soundContactPlayer);
    }






    /* public void OrderGoTo()
     {
         target.transform.position = _player.transform.position + (_player.transform.forward * 4);
         GoTo = true;
     }*/

}
