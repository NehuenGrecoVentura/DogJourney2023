using UnityEngine;

public class BallDog : MonoBehaviour
{
    [SerializeField] GameObject _mapGO;
    public Transform ground;
    Rigidbody _rb;
    Animator _myAnim;
    Collider _col;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
        _myAnim = GetComponent<Animator>();
        _myAnim.enabled = false;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, -2.13f, transform.position.z);
        
    }

    public void ActiveAnim()
    {
        _myAnim.enabled = true;
        _col.isTrigger = true;
        Destroy(_rb);
        _mapGO.SetActive(false);
    }


    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Bridge"))
    //    {
    //        transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
    //    }
    //}

}