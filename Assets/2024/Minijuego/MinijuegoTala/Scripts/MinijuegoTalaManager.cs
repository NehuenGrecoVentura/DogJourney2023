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
    [SerializeField] private CharacterInventory _inventory;
    
    [SerializeField] private MinijuegoTalaCoder Coder1;
    [SerializeField] private MinijuegoTalaCoder Coder2;
    private int CoderActive;
    [SerializeField] private bool Move1;
    
    [Header("UI SCORE")]
    [SerializeField] GameObject _canvasScore;
    [SerializeField] public  int Score;
    [SerializeField] TMP_Text _txtScore;
    [SerializeField] TMP_Text _txtScoreChain;
    [SerializeField] PuestoTala _puestoTala;
    [SerializeField] Image _fadeOut;
    private Collider _colPuesto;
    private ChainParkQuest _chainQuest;

    [Header("TIMER")]
    [SerializeField] private float Timer;
    [SerializeField] private float MaxTimer;
    [SerializeField] Slider _sliderTimer;
    [SerializeField] Image _sliderColor;
    [SerializeField] Image _sliderColorBackgorund;
    [SerializeField] TMP_Text _txtTimer;
    private Color _initialColor;
    private Color _initialColorBackground;

    [Header("AUDIO")]
    [SerializeField] private AudioSource _myAudio;
    [SerializeField] private AudioClip _soundGood;
    [SerializeField] private AudioClip _soundWrong;

    [Header("ASSETS")]
    [SerializeField] GameObject _axe;
    [SerializeField] GameObject _wood;
    [SerializeField] TrunksCut _trunksCut;
    [SerializeField] ParticleSystemRenderer _particleWood;
    [SerializeField] Transform _posStartWood;
    [SerializeField] Transform _posOutWood;
    [SerializeField] Transform _posGameWood;

    private void Awake()
    {
        _chainQuest = FindObjectOfType<ChainParkQuest>();
    }

    private void Start()
    {
        _myAudio = GetComponent<AudioSource>();
        _radar = FindObjectOfType<LocationQuest>();
        _inventory = FindObjectOfType<CharacterInventory>();
        Gaming = false;
        SpawnCoder();
        _canvasScore.SetActive(false);
        _txtScoreChain.gameObject.SetActive(false);
        _trunksCut.gameObject.SetActive(false);
        _colPuesto = _puestoTala.GetComponent<Collider>();

        _sliderTimer.maxValue = MaxTimer;
        _sliderTimer.value = MaxTimer;
        _initialColor = _sliderColor.color;
        _initialColorBackground = _sliderColorBackgorund.color;
        _wood.transform.position = _posStartWood.position;
        Timer = MaxTimer;
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
        //Timer = Timer - 0.2f;
        Timer += 0.2f;
        
        Score++;
        _inventory.tickets++;

        //if (_chainQuest != null && _chainQuest.questActive) _chainQuest.AddScore(Score, _txtScoreChain);

        _txtScore.text = "SCORE: " + Score.ToString();

        if (_chainQuest != null && _chainQuest.questActive)
            _txtScoreChain.text = "TOTAL SCORE: " + _inventory.tickets.ToString();
        
        StopCoroutine(MoveAxe());
        StartCoroutine(Feedback(Color.green));
        _axe.GetComponent<Animator>().Play("Cortar");
        StartCoroutine(MoveAxe());
        _myAudio.PlayOneShot(_soundGood);
    }

    private IEnumerator Feedback(Color color)
    {
        _sliderColor.color = color;
        Color darkerColor = Color.Lerp(color, Color.black, 0.5f);
        _sliderColorBackgorund.color = darkerColor;
        yield return new WaitForSeconds(0.5f);
        _sliderColor.color = _initialColor;
        _sliderColorBackgorund.color = _initialColorBackground;
    }

    private IEnumerator MoveAxe()
    {
        _particleWood.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _particleWood.enabled = false;
    }

    public void WrongClick()
    {
        //Timer = Timer + 1f;
        Timer -= 1f;

        Score--;
        if (Score <= 0) Score = 0;
        _txtScore.text = "SCORE: " + Score.ToString();

        _inventory.tickets--;
        if (_inventory.tickets <= 0) _inventory.tickets = 0;

        if (_chainQuest != null && _chainQuest.questActive)
            _txtScoreChain.text = "TOTAL SCORE: " + _inventory.tickets.ToString();
        
        StartCoroutine(Feedback(Color.red));
        _myAudio.PlayOneShot(_soundWrong);
    }

    private void Update()
    {
        if (Gaming)
        {
            //Timer += Time.deltaTime;
            Timer -= Time.deltaTime;
            _sliderTimer.value = Timer;

            int minutes = Mathf.FloorToInt(Timer / 60);  // Calcula los minutos
            int seconds = Mathf.FloorToInt(Timer % 60);  // Calcula los segundos
            _txtTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);  // Formatea el texto para mostrar minutos y segundos

            if (Timer <= 0) StartCoroutine(ExitGame());

            //if (Timer >= MaxTimer)
            //{
            //    //Gaming = false;
            //    //Game();
            //    //Debug.Log("Perdiste");
            //    //Debug.Log("tu escore fue de " + Score);

            //    StartCoroutine(ExitGame());
            //}

            if (Move1)
            {
                CallCoder();
                Move1 = false;
            }

            CheckDones();
        }
        if (Input.GetKeyDown(KeyCode.T)) StartGame();
        //{
        //    Gaming = !Gaming;
        //    Game();
        //}
    }
    private void Game()
    {
        if (Gaming)
        {
            //Timer = 0;
            Coder1.Rounds = 0;

            Coder2.Rounds = 0;
            CoderActive = 0;
            Reset();
            CallCoder();

            //_camPlayer.enabled = false;
            _camPlayer.gameObject.SetActive(false);
            //_camTala.enabled = true;
            _camTala.gameObject.SetActive(true);
            _character.speed = 0;
            _character.FreezePlayer();
            _radar.StatusRadar(false);
            _canvasScore.SetActive(true);

            if (_chainQuest != null && _chainQuest.questActive)
                _txtScoreChain.gameObject.SetActive(true);

            else _txtScoreChain.gameObject.SetActive(false);
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
        _wood.transform.position = _posStartWood.position;
        _wood.transform.DOMove(_posGameWood.position, 0.5f);

        Score = 0;
        Timer = MaxTimer;
        _txtScore.text = "SCORE: " + Score.ToString();

        if (_chainQuest != null && _chainQuest.questActive) 
            _txtScoreChain.gameObject.SetActive(true);

        else _txtScoreChain.gameObject.SetActive(false);
    }

    private IEnumerator ExitGame()
    {
        Gaming = false;
        _canvasScore.SetActive(false);
        _txtScoreChain.gameObject.SetActive(false);
        _fadeOut.DOColor(Color.black, 0.5f);
        yield return new WaitForSeconds(1f);
        _fadeOut.DOColor(Color.clear, 1f);
        Game();
        _colPuesto.enabled = true;
        Timer = 0;
        Reset();
        CallCoder();
    }

    private IEnumerator ResetWoodCoroutine()
    {
        _trunksCut.gameObject.SetActive(true);
        _wood.transform.DOMove(_posOutWood.position, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _trunksCut.gameObject.SetActive(false);
        _wood.transform.position = _posStartWood.position;
        _wood.transform.DOMove(_posGameWood.position, 0.5f);
    }

    public void ResetWood()
    {
        StartCoroutine(ResetWoodCoroutine());
    }
}