using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepWolfAlert : MonoBehaviour
{
    [SerializeField] private SleepWolf _scriptSleepWolf;
    [SerializeField] private Transform _player;
    private Animator _myAnim;
    private float timeRestartLevel = 10;
    private float speedToRestart = 10f;
    public Rigidbody _playerStop;
    public GameObject activeAlertSimbol;
    public GameObject zzzSimbols;
    [HideInInspector]
    public bool touchWolf = true;
    public bool wolfWakeUp = false;
    public GameObject textLose;

    private void Start()
    {
        _myAnim = GetComponent<Animator>();
        SleepAnim();
        activeAlertSimbol.gameObject.SetActive(false);
        textLose.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (_scriptSleepWolf.detected)
        {
            _playerStop.constraints = RigidbodyConstraints.FreezePosition;
            Destroy(zzzSimbols);
            activeAlertSimbol.gameObject.SetActive(true);

            AlertAnim();
            /*timeRestartLevel -= speedToRestart * Time.deltaTime;
            if (timeRestartLevel <= 0) SceneManager.LoadScene(1);*/
            textLose.gameObject.SetActive(true);
            _playerStop.constraints = RigidbodyConstraints.FreezePosition;
        }
        else SleepAnim();
    }


    private void SleepAnim()
    {
        _myAnim.SetBool("Sleep", true);
        _myAnim.SetBool("Alert", false);

    }

    private void AlertAnim()
    {
        _myAnim.SetBool("Sleep", false);
        _myAnim.SetBool("Alert", true);
        transform.LookAt(_player);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            wolfWakeUp = true;
        }
    }



}
