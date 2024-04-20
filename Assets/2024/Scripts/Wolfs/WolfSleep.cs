using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSleep : MonoBehaviour
{
    [SerializeField] GameObject _gameOver;
    public Transform restartPlayer;
    private Manager _manager;

    private void Awake()
    {
        _manager = FindObjectOfType<Manager>();
    }

    private void Start()
    {
        _gameOver.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            transform.LookAt(player.gameObject.transform.position);
            _manager.GameOver(_gameOver, 10f, "CAREFUL! YOU HAVE WOKE UP ALL THE WOLVES", restartPlayer);
        }
    }
}
