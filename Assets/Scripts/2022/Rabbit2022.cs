using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit2022 : MonoBehaviour
{
    Rabbit2022 _myScript;
    Vector3 _initialPos;
    Animator _myAnim;
    Animation _anim;
    SphereCollider _col;
    public Transform hidePos;
    public float timeToCave;
    public Transform carrotPos;
    public GameObject areaRabbit;
    public Carrot2022 carrot;

    private void Awake()
    {
        gameObject.AddComponent<Grabbable>();
    }

    void Start()
    {
        _myScript = GetComponent<Rabbit2022>();
        _initialPos = transform.position;
        _myAnim = GetComponent<Animator>();
        _anim = GetComponent<Animation>();
        _col = GetComponent<SphereCollider>();
        _col.enabled = false;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.137f, transform.position.z);
        if (carrot.carrotInArea)
        {
            GoToCarrot();
            Destroy(areaRabbit);
            _col.enabled = true;
        }
        FixesAnimsWalkIdle();

    }

    public void GoToCave()
    {
        Vector3 a = gameObject.transform.position;
        Vector3 b = hidePos.position;
        transform.position = Vector3.Lerp(a, b, timeToCave * Time.deltaTime);
        _myAnim.SetBool("Run", true);
        _myAnim.SetBool("Idle", false);
        transform.LookAt(hidePos);
    }

    public void Out()
    {
        _anim.enabled = true;
        Vector3 a = gameObject.transform.position;
        Vector3 b = _initialPos;
        transform.position = Vector3.Lerp(a, b, timeToCave * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    public void GoToCarrot()
    {
        Vector3 a = gameObject.transform.position;
        Vector3 b = carrotPos.position;
        transform.position = Vector3.Lerp(a, b, timeToCave * Time.deltaTime);
        _myAnim.SetBool("Run", true);
        _myAnim.SetBool("Idle", false);
        transform.rotation = Quaternion.Euler(0, -90, 0);
        float distance;
        distance = Vector3.Distance(transform.position, carrotPos.position);
        _anim.Play();
        if (distance <= 1.5)
        {
            _myAnim.enabled = false;
            Destroy(areaRabbit);
            Destroy(_myScript);
        }
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
}
