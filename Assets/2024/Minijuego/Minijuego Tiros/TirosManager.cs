using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    
    [Header("INTRO")]
    [SerializeField] Image _fadeOut;
    [SerializeField] Camera _camIntro;
    private bool _firstContact = true;

    private void Start()
    {
        _myAudio = GetComponent<AudioSource>();
        _radar = FindObjectOfType<LocationQuest>();
        _inventory = FindObjectOfType<CharacterInventory>();
        Gaming = false;
        _canvasScore.SetActive(false);
        _fadeOut.DOColor(Color.clear, 0f);
        _camIntro.enabled = false;
    }
    
    private void Reset()
    {
        TP.Reset();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !Gaming)
        {
            //Gaming = !Gaming;
            //Game();

            StartCoroutine(BeginPlay());
        }
        if (Gaming)
        {
            if (TP.GameOver)
            {
                Debug.Log("perdiste");
                Gaming = false;
                Game();
            }
        }
    }
    private void Game()
    {
        if (Gaming)
        {
            Reset();
            TP.GameOver = false;
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
            TP.GameOver = true;
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
        _character.FreezePlayer();
        _fadeOut.DOColor(Color.black, 1f);
        
        yield return new WaitForSeconds(2f);
        _camIntro.enabled = true;
        _camPlayer.enabled = false;
        _fadeOut.DOColor(Color.clear, 1f);

        if (_firstContact)
        {
            yield return new WaitForSeconds(1f);
            _camIntro.transform.DOMoveZ(_camApple.transform.position.z, 3f);

            yield return new WaitForSeconds(3f);
            _camIntro.enabled = false;
            Gaming = !Gaming;
            Game();
            _firstContact = false;
        }

        else
        {
            yield return new WaitForSeconds(1f);
            Gaming = !Gaming;
            Game();
        }
        
    }
}