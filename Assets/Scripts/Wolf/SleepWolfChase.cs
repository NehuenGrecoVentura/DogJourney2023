using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepWolfChase : MonoBehaviour
{
    [SerializeField] private SleepWolf _scriptSleepWolf;
    [SerializeField] private Transform _player;
    private Animator _myAnim;
    public float speedChase;
    private float timeRestartLevel = 10;
    private float speedToRestart = 10f;
    private void Start()
    {
        _myAnim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_scriptSleepWolf.detected)
        {
            Vector3 a = gameObject.transform.position;
            Vector3 b = _player.position;
            transform.position = Vector3.Lerp(a, b, speedChase * Time.deltaTime);
            transform.LookAt(_player);
            _myAnim.SetTrigger("Detected");
            timeRestartLevel -= speedToRestart * Time.deltaTime;
            if (timeRestartLevel <= 0)
            {
                speedChase = 0;
                SceneManager.LoadScene(0);
            }
                
                
                
        }
    }



}
