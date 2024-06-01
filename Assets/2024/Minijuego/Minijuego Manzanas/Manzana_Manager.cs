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

    [Header("UI SCORE")]
    [SerializeField] GameObject _canvasScore;

    [Header("INTRO")]
    [SerializeField] Image _fadeOut;
    [SerializeField] Image[] _count;
    [SerializeField] Camera _camIntro;
    [SerializeField] BoxMessages _boxMessage;
    [SerializeField, TextArea(4, 6)] string _message;
    private bool _firstContact = true;

    private void Start()
    {
        Random();
        _myAudio = GetComponent<AudioSource>();
        _radar = FindObjectOfType<LocationQuest>();
        _inventory = FindObjectOfType<CharacterInventory>();
        Gaming = false;

        _canvasScore.SetActive(false);
        _fadeOut.DOColor(Color.clear, 0f);
        _camIntro.enabled = false;
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
            //Gaming = !Gaming;
            //Game();

            StartCoroutine(BeginPlay());
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

    private IEnumerator BeginPlay()
    {
        _boxMessage.SetMessage("Mini Game");

        _character.FreezePlayer();
        _fadeOut.DOColor(Color.black, 1f);

        yield return new WaitForSeconds(2f);
        _camIntro.enabled = true;
        _camPlayer.enabled = false;
        _fadeOut.DOColor(Color.clear, 1f);

        if (_firstContact)
        {
            yield return new WaitForSeconds(1f);
            _camIntro.transform.DOMove(_camApple.transform.position, 3f);

            yield return new WaitForSeconds(3f);
            _boxMessage.ShowMessage(_message);

            yield return new WaitForSeconds(3f);
            _boxMessage.CloseMessage();

            for (int i = 0; i < _count.Length; i++)
            {
                _count[i].DOColor(Color.white, 0.5f);
                yield return new WaitForSeconds(1f);
                _count[i].DOColor(Color.clear, 0.5f);
            }

            _camIntro.enabled = false;
            Gaming = !Gaming;
            Game();

            yield return new WaitForSeconds(0.5f);
            _boxMessage.DesactivateMessage();
            _firstContact = false;
        }

        else
        {
            yield return new WaitForSeconds(1f);
            Gaming = !Gaming;
            Game();
        }
    }

    private IEnumerator ExitGame()
    {
        Gaming = false;
        _canvasScore.SetActive(false);
        _fadeOut.DOColor(Color.black, 0.5f);
        yield return new WaitForSeconds(1f);
        _fadeOut.DOColor(Color.clear, 1f);
        Game();
    }
}