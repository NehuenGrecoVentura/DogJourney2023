using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChopping : MonoBehaviour
{
    [SerializeField] GameObject _treeFall;

    private void Start()
    {
        _treeFall.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _treeFall.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
