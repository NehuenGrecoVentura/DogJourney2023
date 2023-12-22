using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimMessage : MonoBehaviour
{
    [SerializeField] Market[] _markets;
    [SerializeField] Animator _myAnim;
    [SerializeField] GameObject _canvasAnimMessage;
    [SerializeField] TMP_Text _text;
    [SerializeField] TMP_Text _textInfo;
    [SerializeField] float _timeInScreen;
    float _initialTime;

    [SerializeField] Image _iconDog;
    [SerializeField] Image _iconTrolley;
    [SerializeField] Image _iconDogSpeed;
    [SerializeField] Image _iconAxe;
    [SerializeField] Image _iconSpeedPlayer;
    [SerializeField] Image _iconTrees;

    void Start()
    {
        _initialTime = _timeInScreen;
    }

    void Update()
    {
        foreach (var market in _markets)
        {
            if (market.activeMessage)
            {
                InAnim();
                CountDown();
                if (market.upgradeAxe)
                {
                    _text.text = "YOUR HAS BEEN UPGRADED.";
                    _textInfo.text = "+ 5%";
                    _iconAxe.enabled = true;
                    _iconSpeedPlayer.enabled = false;
                    _iconTrees.enabled = false;
                    _iconDog.enabled = false;
                    _iconTrolley.enabled = false;
                    _iconDogSpeed.enabled = false;
                }

                if (market.upgradeSpeedPlayer)
                {
                    _text.text = "NOW YOU MOVE FASTER.";
                    _textInfo.text = "+ 3%";
                    _iconAxe.enabled = false;
                    _iconSpeedPlayer.enabled = true;
                    _iconTrees.enabled = false;
                    _iconDog.enabled = false;
                    _iconTrolley.enabled = false;
                    _iconDogSpeed.enabled = false;
                }

                if (market.upgradeTreeGen)
                {
                    _text.text = "NOW THE TREES GROW QUICKLY.";
                    _textInfo.text = "+ 15%";
                    _iconAxe.enabled = false;
                    _iconSpeedPlayer.enabled = false;
                    _iconTrees.enabled = true;
                    _iconDog.enabled = false;
                    _iconTrolley.enabled = false;
                    _iconDogSpeed.enabled = false;
                }

                if (market.upgradeTrolley)
                {
                    _text.text = "THE CART NOW SUPPORTS MORE WOOD.";
                    _textInfo.text = "+ 15";
                    _iconAxe.enabled = false;
                    _iconSpeedPlayer.enabled = false;
                    _iconTrees.enabled = false;
                    _iconDog.enabled = false;
                    _iconTrolley.enabled = true;
                    _iconDogSpeed.enabled = false;
                }

                if (market.upgradeSpeedDog)
                {
                    _text.text = "YOUR DOG MOVES FASTER.";
                    _textInfo.text = "+ 3%";
                    _iconAxe.enabled = false;
                    _iconSpeedPlayer.enabled = false;
                    _iconTrees.enabled = false;
                    _iconDog.enabled = false;
                    _iconTrolley.enabled = false;
                    _iconDogSpeed.enabled = true;
                }

                if (market.upgradeAddDog)
                {
                    _text.text = "ADDED A NEW DOG TO CONTROL.";
                    _textInfo.text = "+ 1";
                    _iconAxe.enabled = false;
                    _iconSpeedPlayer.enabled = false;
                    _iconTrees.enabled = false;
                    _iconDog.enabled = true;
                    _iconTrolley.enabled = false;
                    _iconDogSpeed.enabled = false;
                }
            }
        }
    }

    void InAnim()
    {
        _myAnim.SetBool("In", true);
        _myAnim.SetBool("Out", false);
    }

    void OutAnim()
    {
        _myAnim.SetBool("In", false);
        _myAnim.SetBool("Out", true);
    }

    public void CountDown()
    {
        _canvasAnimMessage.SetActive(true);
        _timeInScreen -= Time.deltaTime;
        if (_timeInScreen <= 0)
        {
            _timeInScreen = _initialTime;
            OutAnim();
            foreach (var market in _markets)
                market.activeMessage = false;
        }
    }
}