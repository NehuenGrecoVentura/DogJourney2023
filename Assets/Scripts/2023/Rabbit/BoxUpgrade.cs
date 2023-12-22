using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxUpgrade : MonoBehaviour
{
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.Q;
    private Inventory _invetory;

    [Header("COUNTDOWN CONFIG")]
    [SerializeField] float _coolDown = 1.5f;
    public bool orderBlock = false;

    private void Awake()
    {
        _invetory = FindObjectOfType<Inventory>();
    }

    private void Start()
    {
        _iconInteractive.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var dog = other.GetComponent<Dog2022>();
        if (dog != null)
        {
            _invetory.upgrade = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();

        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive))
                _iconInteractive.gameObject.SetActive(true);

            else if (Input.GetKeyDown(_buttonInteractive) /*&& _coolDown >= 1.5f*/ && !orderBlock)
            {
                _iconInteractive.gameObject.SetActive(false);
                orderBlock = true;
                StartCoroutine(CoolDown());
            }
                
        }
    }

    IEnumerator CoolDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coolDown);
            orderBlock = false;
            StopAllCoroutines();
        }
    }
}