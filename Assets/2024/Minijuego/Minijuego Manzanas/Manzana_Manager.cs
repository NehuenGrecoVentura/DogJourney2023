using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    private float _initialMaxSpeed;
    private float _initialMinSpeed;
    private float _initialSpawnMinSpeed;
    private float _initialSpawnMaxSpeed;

    [Header("UI SCORE")]
    [SerializeField] GameObject _canvasScore;
    [SerializeField] PuestoManzana _puestoManzana;
    private Collider _colPuesto;

    [Header("INTRO")]
    [SerializeField] Image _fadeOut;
    [SerializeField] Camera _camIntro;

    private void Awake()
    {
        _colPuesto = _puestoManzana.GetComponent<Collider>();
    }

    private void Start()
    {
        Random();
        _myAudio = GetComponent<AudioSource>();
        _radar = FindObjectOfType<LocationQuest>();
        _inventory = FindObjectOfType<CharacterInventory>();
        Gaming = false;

        _canvasScore.SetActive(false);
        _fadeOut.DOColor(Color.clear, 0f);
        _camIntro.gameObject.SetActive(false);
        _camApple.gameObject.SetActive(false);

        _initialMaxSpeed = Maxspeed;
        _initialMinSpeed = Minspeed;
        _initialSpawnMinSpeed = SpawnerTimerMin;
        _initialSpawnMaxSpeed = SpawnerTimerMax;
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
                //Debug.Log("perdiste");
                //Gaming = false;
                //rb.velocity = Vector3.zero;
                //Game();

                StartCoroutine(ExitGame());
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
            //_camPlayer.enabled = false;
            _camPlayer.gameObject.SetActive(false);
            //_camApple.enabled = true;
            _camApple.gameObject.SetActive(true);
            _character.speed = 0;
            _character.FreezePlayer();
            _radar.StatusRadar(false);
            _canvasScore.SetActive(true);
        }
        else
        {
            Reset();
            MP.GameOver = true;
            //_camPlayer.enabled = true;
            _camPlayer.gameObject.SetActive(true);
            //_camApple.enabled = false;
            _camApple.gameObject.SetActive(false);
            _character.speed = _character.speedAux;
            _character.DeFreezePlayer();
            _radar.StatusRadar(true);
            _canvasScore.SetActive(false);
        }
    }

    private IEnumerator ExitGame()
    {
        Gaming = false;
        _canvasScore.SetActive(false);
        _fadeOut.DOColor(Color.black, 0.5f);
        yield return new WaitForSeconds(1f);
        _fadeOut.DOColor(Color.clear, 1f);
        rb.velocity = Vector3.zero;
        Game();
        _colPuesto.enabled = true;
    }

    public void StartGame()
    {
        Gaming = !Gaming;
        Game();
        ResetSpeed();
        MP.ResetScore();
        MP.ShowGlobalScore();
    }

    public void ChangeSpeed()
    {
        SpawnerTimerMax = 2;
        SpawnerTimerMin = 1;
        Maxspeed += 5;
        Minspeed += 5;
    }

    private void  ResetSpeed()
    {
        SpawnerTimerMax = _initialSpawnMaxSpeed;
        SpawnerTimerMin = _initialSpawnMinSpeed;
        Maxspeed = _initialMaxSpeed;
        Minspeed = _initialMinSpeed;
    }
}