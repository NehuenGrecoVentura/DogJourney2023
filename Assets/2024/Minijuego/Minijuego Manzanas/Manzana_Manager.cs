using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manzana_Manager : MonoBehaviour
{
    [SerializeField] private float SpawnerTimer;
    [SerializeField] private float SpawnerTimerMax;
    [SerializeField] private float SpawnerTimerMin;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float Maxspeed;
    [SerializeField] private float Minspeed;
    [SerializeField] private Transform Izquierda;
    [SerializeField] private Transform Derecha;
    [SerializeField] private int RandomWay;

    [SerializeField] private Manzanas_Player MP;
    [SerializeField] private bool Gaming;
    [SerializeField] private CameraOrbit _camPlayer;
    [SerializeField] private Camera _camApple;
    [SerializeField] private Character _character;
    [SerializeField] private LocationQuest _radar;
    [SerializeField] private AudioSource _myAudio;
    [SerializeField] private CharacterInventory _inventory;

    [Header("MESH BARRIL")]
    [SerializeField] Transform _barrilMesh;
    [SerializeField] float _speedRotBarril;

    [Header("UI SCORE")]
    [SerializeField] GameObject _canvasScore;

    private void Start()
    {
        Random();
        _myAudio = GetComponent<AudioSource>();
        _radar = FindObjectOfType<LocationQuest>();
        _inventory = FindObjectOfType<CharacterInventory>();
        Gaming = false;
        _canvasScore.SetActive(false);
    }

    void Spawn()
    {
        var m = ManzanaFactory.Instance.pool.GetObject();
        m.transform.position = transform.position + transform.forward;
        m.transform.forward = transform.forward;
    }

    private void Reset()
    {
        rb.velocity = Vector3.zero;
        MP.Reset();
    }

    void move()
    {
        if (RandomWay >= 5)
        {
            rb.velocity = Vector3.left * speed;
            _barrilMesh.transform.Rotate(0, 0, _speedRotBarril * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector3.right * speed;
            _barrilMesh.transform.Rotate(0, 0, -_speedRotBarril * Time.deltaTime);
        }

    }

    void Random()
    {
        RandomWay = UnityEngine.Random.Range(0, 10);
        speed = UnityEngine.Random.Range(Minspeed, Maxspeed);
        SpawnerTimer = UnityEngine.Random.Range(SpawnerTimerMin, SpawnerTimerMax);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Gaming = !Gaming;
            Game();
        }
        if (Gaming)
        {
            if (MP.GameOver)
            {
                Debug.Log("perdiste");
                Gaming = false;
                rb.velocity = Vector3.zero;
                Game();
            }

            move();
            SpawnerTimer -= Time.deltaTime;
            if (SpawnerTimer <= 0)
            {
                Spawn();
                Random();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "OutBoxI")
        {
            transform.position = Derecha.position;
        }
        if (other.gameObject.name == "OutBoxD")
        {
            transform.position = Izquierda.position;
        }
    }

    private void Game()
    {
        if (Gaming)
        {
            Reset();
            MP.GameOver = false;
            _camPlayer.enabled = false;
            _camApple.enabled = true;
            _character.speed = 0;
            _character.FreezePlayer();
            _radar.StatusRadar(false);
            _canvasScore.SetActive(true);
        }
        else
        {
            Reset();
            MP.GameOver = true;
            _camPlayer.enabled = true;
            _camApple.enabled = false;
            _character.speed = _character.speedAux;
            _character.DeFreezePlayer();
            _radar.StatusRadar(true);
            _canvasScore.SetActive(false);
        }
    }
}