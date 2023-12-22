using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensorWolf : MonoBehaviour
{
    [SerializeField] float _timeToLose = 3;
    [SerializeField] float _speedToLose;
    float _initialTimeToLose;
    public Image iconDetected;
    //public Image detectionBar1;
    //public Image detectionBar2;
    public Image circleBar;
    public GameManager gameManager;

    private void Start()
    {
        _initialTimeToLose = _timeToLose;
        iconDetected.gameObject.SetActive(false);
        circleBar.fillAmount = 0;
        //detectionBar1.fillAmount = 0;
        //detectionBar2.fillAmount = 0;
    }

    public void WolfIconDetected()
    {
        _timeToLose -= Time.deltaTime * _speedToLose;
        iconDetected.gameObject.SetActive(true);
        //detectionBar1.fillAmount += _speedToLose * Time.deltaTime;
        //detectionBar2.fillAmount += _speedToLose * Time.deltaTime;
        circleBar.fillAmount += _speedToLose * Time.deltaTime;
        if (_timeToLose <= 0)
        {
            _timeToLose = 0;
            gameManager.GameOver();
        }
    }

    public void ResetIconWolf()
    {
        _timeToLose = _initialTimeToLose;
        iconDetected.gameObject.SetActive(false);
        circleBar.fillAmount = 0;
        //detectionBar1.fillAmount = 0;
        //detectionBar2.fillAmount = 0;
    }
}
