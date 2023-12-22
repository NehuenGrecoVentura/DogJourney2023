using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitEscape2022 : MonoBehaviour
{
    [HideInInspector] public bool escape = false;
    public float time = 2f;
    int _index;
    public Transform[] waypoints;
    public WolfPatrol2022 wolf;
    private Grabbable _grabbable;
    Animator _myAnim;

    void Start()
    {
        _myAnim = GetComponent<Animator>();
        _grabbable = GetComponent<Grabbable>();
    }

    void Update()
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

        if (wolf.rabbitDetected) escape = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Escape Rabbit" && Input.GetKeyDown(KeyCode.F))
        {
            escape = true;
        }
    }
}
