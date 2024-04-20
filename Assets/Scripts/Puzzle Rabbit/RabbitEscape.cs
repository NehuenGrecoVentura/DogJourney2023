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

   
}