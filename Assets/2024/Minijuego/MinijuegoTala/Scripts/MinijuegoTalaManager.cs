using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MinijuegoTalaManager : MonoBehaviour
{
    [SerializeField] public bool Gaming;
    [SerializeField] private CameraOrbit _camPlayer;
    [SerializeField] private Camera _camTala;
    [SerializeField] private Character _character;
    [SerializeField] private LocationQuest _radar;
    [SerializeField] private AudioSource _myAudio;
    [SerializeField] private CharacterInventory _inventory;
    
    [SerializeField] private MinijuegoTalaCoder Coder1;
    [SerializeField] private MinijuegoTalaCoder Coder2;
    private int CoderActive;
    [SerializeField] private bool Move1;
    [SerializeField] private float Timer;
    [SerializeField] private float MaxTimer;
    
    [Header("UI SCORE")]
    [SerializeField] GameObject _canvasScore;
    [SerializeField] private int Score;
    [SerializeField] TMP_Text _txtScore;
    [SerializeField] PuestoTala _puestoTala;
    [SerializeField] Image _fadeOut;
    [SerializeField] Slider _sliderTimer;
    private Collider _colPuesto;


    private void Start()
    {
        _myAudio = GetComponent<AudioSource>();
        _radar = FindObjectOfType<LocationQuest>();
        _inventory = FindObjectOfType<CharacterInventory>();
        Gaming = false;
        SpawnCoder();
        _canvasScore.SetActive(false);
        _colPuesto = _puestoTala.GetComponent<Collider>();

        _sliderTimer.maxValue = MaxTimer;
        _sliderTimer.value = MaxTimer;
    }
    
    public void Reset()
    {
        Coder1.Reset();
        Coder2.Reset();
        SpawnCoder();
    }

    private void SpawnCoder()
    {
        Coder1.GenerateRandom();
        Coder2.GenerateRandom();
    }

    private void CallCoder()
    {
        if (CoderActive == 0)
        {
            Coder1.GoTo();
            Move1 = false;
            CoderActive = 1;
        }
        else if (CoderActive == 1)
        {
            Coder2.GoTo();
            Move1 = false;
            CoderActive = 0;
        }
    }

    private void CheckDones()
    {
        if (Coder1.Done) 
        {
            Coder1.GoTo();
            Reset();
            Move1 = true;
        }
        if (Coder2.Done)
        {
            Coder2.GoTo();
            Reset();
            Move1 = true;
        }
    }

    public void GoodClick()
    {
        Timer = Timer - 0.2f;
        Score++;
        _txtScore.text = "SCORE: " + Score.ToString();
    }

    public void WrongClick()
    {
        Timer = Timer + 1f;
        Score--;
    }

    private void Update()
    {
        if (Gaming)
        {
            Timer += Time.deltaTime;

            _sliderTimer.value = Timer;

            if (Timer >= MaxTimer)
            {
                //Gaming = false;
                //Game();
                //Debug.Log("Perdiste");
                //Debug.Log("tu escore fue de " + Score);

                StartCoroutine(ExitGame());




            }
            if (Move1)
            {
                CallCoder();
                Move1 = false;
            }
            CheckDones();
        }
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Gaming = !Gaming;
        //    Game();
        //}
    }
    private void Game()
    {
        if (Gaming)
        {
            //_camPlayer.enabled = false;
            _camPlayer.gameObject.SetActive(false);
            //_camTala.enabled = true;
            _camTala.gameObject.SetActive(true);
            _character.speed = 0;
            _character.FreezePlayer();
            _radar.StatusRadar(false);
            _canvasScore.SetActive(true);
            Timer = 0;
            Reset();
            CallCoder();
        }
        else
        {
            //_camPlayer.enabled = true;
            _camPlayer.gameObject.SetActive(true);
            //_camTala.enabled = false;
            _camTala.gameObject.SetActive(false);
            _character.speed = _character.speedAux;
            _character.DeFreezePlayer();
            _radar.StatusRadar(true);
        }
    }

    public void StartGame()
    {
        Gaming = !Gaming;
        Game();
    }

    private IEnumerator ExitGame()
    {
        Gaming = false;
        _canvasScore.SetActive(false);
        _fadeOut.DOColor(Color.black, 0.5f);
        yield return new WaitForSeconds(1f);
        _fadeOut.DOColor(Color.clear, 1f);
        Game();
        _colPuesto.enabled = true;
        Timer = 0;
        Reset();
        CallCoder();
    }
}
