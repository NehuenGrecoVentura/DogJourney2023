using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDogSelect : MonoBehaviour
{
    [SerializeField] GameObject _dog1Selected;
    [SerializeField] GameObject _dog2Selected;

    void Start()
    {
        SelectDog1();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectDog1();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectDog2();
        }
    }

    void SelectDog1()
    {
        _dog1Selected.SetActive(true);
        _dog2Selected.SetActive(false);
    }

    void SelectDog2()
    {

        _dog1Selected.SetActive(false);
        _dog2Selected.SetActive(true);
    }
}