using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using System.Collections;

public class Apple : MonoBehaviour
{
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive;
    [HideInInspector] public bool isUpgraded = false;


    void Start()
    {
        _iconInteractive.SetActive(false);
        _iconInteractive.transform.DOScale(0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
       
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteractive.transform.DOScale(0f, 0.5f);
    }
}