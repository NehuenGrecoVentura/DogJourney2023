using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TirosManager : MonoBehaviour
{
    [SerializeField] private TirosCharacter TP;
    [SerializeField] private bool Gaming;
    [SerializeField] private CameraOrbit _camPlayer;
    [SerializeField] private Camera _camApple;
    [SerializeField] private Character _character;
    [SerializeField] private LocationQuest _radar;
    [SerializeField] private AudioSource _myAudio;
    [SerializeField] private CharacterInventory _inventory;

    [Header("UI SCORE")]
    [SerializeField] GameObject _canvasScore;
    [SerializeField] PuestoTiros _puestoTiros;
    [SerializeField] TMP_Text _txtScoreChain;
    private Collider _colPuesto;
    private ChainParkQuest _chainQuest;

    [Header("FADE OUT")]
    [SerializeField] Image _fadeOut;

    private void Awake()
    {
        _colPuesto = _puestoTiros.GetComponent<Collider>();
        _chainQuest = FindObjectOfType<ChainParkQuest>();
    }

    private void Start()
    {
        _myAudio = GetComponent<AudioSource>();
        _radar = FindObjectOfType<LocationQuest>();
        _inventory = FindObjectOfType<CharacterInventory>();
        Gaming = false;
        _canvasScore.SetActive(false);
        _fadeOut.DOColor(Color.clear, 0f);
        _camApple.gameObject.SetActive(false);
        _txtScoreChain.gameObject.SetActive(false);
    }
    
    private void Reset()
    {
        TP.Reset();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Gaming = !Gaming;
            Game();
        }
        if (Gaming)
        {
            if (TP.GameOver)
            {
                //Debug.Log("perdiste");
                //Gaming = false;
                //Game();

                StartCoroutine(ExitGame());
            }
        }
    }
    private void Game()
    {
        if (Gaming)
        {
            Reset();
            TP.GameOver = false;
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
            TP.GameOver = true;
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
        _txtScoreChain.gameObject.SetActive(false);
        _fadeOut.DOColor(Color.black, 0.5f);
        yield return new WaitForSeconds(1f);
        _fadeOut.DOColor(Color.clear, 1f);
        Game();
        _colPuesto.enabled = true;
    }

    public void StartGame()
    {
        Gaming = !Gaming;
        Game();

        if(_chainQuest != null && _chainQuest.questActive)
        {
            _txtScoreChain.gameObject.SetActive(true);
            _txtScoreChain.text = "TOTAL SCORE: " + _inventory.tickets.ToString();
        }
            
        else _txtScoreChain.gameObject.SetActive(false);
    }
}