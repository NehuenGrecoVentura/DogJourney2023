using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SimonManager : MonoBehaviour
{
    [SerializeField] private bool Gaming;
    [SerializeField] private CameraOrbit _camPlayer;
    [SerializeField] private Camera _camSimon;
    [SerializeField] private Character _character;
    [SerializeField] private LocationQuest _radar;
    [SerializeField] private AudioSource _myAudio;
    [SerializeField] private CharacterInventory _inventory;
    [SerializeField] private SimonBoton[] Botons;
     private int SimMax;
    [SerializeField] private int SimStart;
    [SerializeField] private float StartSimTime;
    public  List<int> userList, SimonList;
    public bool SimonDiciendo;

    [Header("UI SCORE")]
    [SerializeField] GameObject _canvasScore;
    [SerializeField] PuestoSimonDice _puestoSimon;
    [SerializeField] TMP_Text _txtScore;
    [SerializeField] int _score;
    private Collider _colPuesto;

    [Header("INTRO")]
    [SerializeField] Image _fadeOut;
    [SerializeField] Camera _camIntro;

    private void Start()
    {
        _myAudio = GetComponent<AudioSource>();
        _radar = FindObjectOfType<LocationQuest>();
        _inventory = FindObjectOfType<CharacterInventory>();
        Gaming = false;

        _canvasScore.SetActive(false);
        _fadeOut.DOColor(Color.clear, 0f);
        _camIntro.gameObject.SetActive(false);
        _camSimon.gameObject.SetActive(false);
        _colPuesto = _puestoSimon.GetComponent<Collider>();
    }

    public void Reset()
    {
        SimMax = SimStart;
        StartCoroutine(SimonDice());
    }

    private void Next()
    {
        StartCoroutine(SimonDice());
    }
    
    IEnumerator SimonDice()
    {
        Debug.Log("Observe and prepare");
        LockMouse();
        yield return new WaitForSeconds(1);
        SimonDiciendo = true;
        userList = new List<int>();
        SimonList = new List<int>();
        for (int i = 0; i < SimMax; i++)
        {
            int rng = Random.Range(0, 4);
            SimonList.Add(rng);
            Botons[rng].Click();
            yield return new WaitForSeconds(StartSimTime);
        }
        SimMax++;
        UnlockMouse();
        SimonDiciendo = false;
    }

    public void PlayerClicking(SimonBoton b, AudioSource audioSource, AudioClip soundButton, AudioClip soundError)
    {
        userList.Add(b.ID);
        if(userList[userList.Count-1] != SimonList[userList.Count-1])
        {
            audioSource.PlayOneShot(soundError);
            Debug.Log("GameOver");
            Gaming = false;
            Game();
        }
        else if (userList.Count == SimonList.Count)
        {
            audioSource.PlayOneShot(soundButton);
            Next();
            _score += 10;
            _txtScore.text = "SCORE: " + _score.ToString();
            Debug.Log("next Lvl");
        }
    }
    
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.N))
    //    {
    //        Gaming = !Gaming;
    //        Game();
    //    }
    //}   

    private void Game()
    {
        if (Gaming)
        {
            //_camPlayer.enabled = false;
            _camPlayer.gameObject.SetActive(false);
            //_camSimon.enabled = true;
            _camSimon.gameObject.SetActive(true);
            _character.speed = 0;
            _character.FreezePlayer();
            _radar.StatusRadar(false);
            _canvasScore.SetActive(true);
            UnlockMouse();
            Reset();
        }
        else
        {
            //_camPlayer.enabled = true;
            _camPlayer.gameObject.SetActive(true);
            //_camSimon.enabled = false;
            _camSimon.gameObject.SetActive(false);
            _character.speed = _character.speedAux;
            _character.DeFreezePlayer();
            _radar.StatusRadar(true);
            LockMouse();
            
            _colPuesto.enabled = true;
            _canvasScore.SetActive(false);
        }
    }

    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
    }
}