using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleUI : MonoBehaviour
{
    [SerializeField] float _timeToLose = 3;
    [SerializeField] float _speedToLose;
    float _initialTimeToLose;
    public Image circleBar;
    public GameManager gameManager;

    private void Start()
    {
        _initialTimeToLose = _timeToLose;
        circleBar.fillAmount = 0;
        circleBar.GetComponent<Image>().color = new Color(255, 255, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) circleBar.fillAmount += _speedToLose * Time.deltaTime;
        //if (Input.GetKeyDown(KeyCode.N)) circleBar.fillAmount -= 10;
    }




    public void WolfIconDetected()
    {
        _timeToLose -= Time.deltaTime * _speedToLose;
        if (circleBar.fillAmount >= 1) circleBar.color = Color.red;
        if (_timeToLose <= 0)
        {
            _timeToLose = 0;
            gameManager.GameOver();
        }
    }

    public void ResetIconWolf()
    {
        _timeToLose = _initialTimeToLose;
        circleBar.fillOrigin = 0;
    }
}
