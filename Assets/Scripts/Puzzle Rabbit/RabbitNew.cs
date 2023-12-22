using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitNew : MonoBehaviour
{
    public Carrot carrot;
    public Transform carrotPos;
    public Transform escapePos;
    public Transform hidePos;
    private Vector3 initialPos;
    public float speed;
    [SerializeField] AudioSource _myAudio;
    [SerializeField] Animator _myAnim;
    public float time;
    private Collider _col;
    public RabbitHide2 hideScript;
    public GameObject area;
    public RabbitNew myScript;
    private Animation _animation;
    public ParticleSystem effectEat;

    private void Awake()
    {
        gameObject.AddComponent<Grabbable>();    
    }

    private void Start()
    {
        initialPos = transform.position;
        _col = GetComponent<Collider>();
        _col.enabled = false;
        _animation = GetComponent<Animation>();
        _animation.Stop();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.137f, transform.position.z);
        if (carrot.carrotInArea)
        {
            GoToCarrot();
            Destroy(hideScript);
            _col.enabled = true;
        }

        FixesAnimsWalkIdle(); // FIX QUE ARREGLA LAS ANIMACIONES TRABADAS DEL IDLE Y WALK DEL CONEJO.
    }

    void FixesAnimsWalkIdle()
    {
        if (transform.position.x >= 9.30f) _myAnim.enabled = false;
        if (transform.position.x < 9.30f) _myAnim.enabled = true;
        if (transform.position.x < 7.50f)
        {
            _myAnim.SetBool("Run", false);
            _myAnim.SetBool("Idle", true);
        }
    }

    public void GoToCarrot()
    {
        Vector3 a = gameObject.transform.position;
        Vector3 b = carrotPos.position;
        transform.position = Vector3.Lerp(a, b, time * Time.deltaTime);
        _myAnim.SetBool("Run", true);
        _myAnim.SetBool("Idle", false);
        transform.rotation = Quaternion.Euler(0, -90, 0);
        float distance;
        distance = Vector3.Distance(transform.position, carrotPos.position);
        _animation.Play();
        if (distance <= 1.5)
        {
            _myAnim.enabled = false;
            Destroy(area);
            Destroy(myScript);
        }
    }

    public void GoToCave()
    {
        Vector3 a = gameObject.transform.position;
        Vector3 b = hidePos.position;
        transform.position = Vector3.Lerp(a, b, time * Time.deltaTime);
        _myAnim.SetBool("Run", true);
        _myAnim.SetBool("Idle", false);
        transform.LookAt(hidePos);
    }

    public void Out()
    {
        _animation.enabled = true;
        Vector3 a = gameObject.transform.position;
        Vector3 b = initialPos;
        transform.position = Vector3.Lerp(a, b, time * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }

}
