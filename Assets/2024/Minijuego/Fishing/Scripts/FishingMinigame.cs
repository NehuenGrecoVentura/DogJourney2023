using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class FishingMinigame : MonoBehaviour
{
    //CameraControl
    [Header("CAMERA CONTROL")]
    //[SerializeField] private GameObject camer1; //Camara del juego
    //[SerializeField] private GameObject camer2; //Camara del minijuego

    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] Camera _camFish;

    [SerializeField] private Character _character; //Control del player

    //FishMove
    [Header("FISH MOVE")]
    [SerializeField] private Transform topPoint;  // posicion mas arriba de la barra
    [SerializeField] private Transform BotPoint; //posicion mas abajo de la barra
    [SerializeField] private GameObject Fish; // el pez
    float fishRandomPos; //La posicion random del pez
    [SerializeField] GameObject fishDestination; //El vector random del pez
    float FishTimer; //El tiempo hasta que el pez se mueve
    [SerializeField] private float TimeMult;  // el multiplicador de cuanto tarda el pez en moverse 
    public float fishSpeed; //Velocidad del pez
    public float FishSpeedMult; // Multiplicador de velocidad del pez

    //Hook move
    [Header("HOOK MOVE")]
    [SerializeField] private Transform HookTrasn; //El gancho
    public float HookPower; //Cuanto sube el gancho
    [SerializeField] private float GravityPower; //Cuanto baja el gancho
    private Vector3 _initialScale, _initialPos;

    //Catch
    [Header("CATCH")]
    [SerializeField] private GameObject ChargeBar; //Barra de progreso
    [SerializeField] private float Capture; //Porcentaje de la barra
    [SerializeField] private float Distance; //Distancia entre el gancho y el pez medido en eje Y
    [SerializeField] private float DifficultyCaptureMax; //Dificultad al atrapar
    [SerializeField] private float DifficultyCaptureMin; //Dificultad al atrapar
    [SerializeField] private float DifficultyCapture;
    [SerializeField] private float DifficultyEscapeMax; //Dificultad del pez de escapar
    [SerializeField] private float DifficultyEscapeMin; //Dificultad del pez de escapar
    [SerializeField] private float DifficultyEscape;
    [SerializeField] private float CaptureRange; //Rango en la que el pez es atrapado

    // Reset
    [Header("RESET")]
    [SerializeField] private Transform StartPos; //Posicion inicial de las cosas
    [SerializeField] public bool Gaming; // Si tas en el minijuego o no


    [Header("Victory")]
    public bool Victory; //Si el bool es true gano el juego, si perdio va a ser false

    [SerializeField] private float CaptureResetScore; //a cuanto se va a reiniciar el capture
    private LocationQuest _radar;
    public bool start;
    private int overWatch;


    [Header("FISH MESH")]
    [SerializeField] GameObject _fishMesh;
    [SerializeField] SpriteRenderer _spriteFish;
    [SerializeField] SpriteRenderer _spriteFishWin;
    [SerializeField] GameObject _canvasRenderFish;

    [Header("UI")]
    [SerializeField] Image[] _count;
    public TMP_Text[] _textAmount;
    public int fishedPicked = 0;

    [Header("AUDIOS")]
    private AudioSource _myAudio;

    private CharacterInventory _inventory;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();

        _radar = FindObjectOfType<LocationQuest>();
        _inventory = FindObjectOfType<CharacterInventory>();
    }

    private void Start()
    {
        _camFish.enabled = false;
        _spriteFishWin.transform.DOScale(0f, 0f);
        //_canvasRenderFish.SetActive(false);

        foreach (TMP_Text text in _textAmount)
        {
            text.gameObject.SetActive(false);
        }

        _initialScale = HookTrasn.localScale;
        _initialPos = HookTrasn.transform.position;
    }

    private void Update()
    {
        CameraChange();
        if (Gaming)
        {
            FishMove();
            PlayerControl();
            Catch();
        }
    }

    private void FishMove()
    {
        fishSpeed += Time.deltaTime * FishSpeedMult;
        FishTimer -= Time.deltaTime;
        CheckDir();

        if (FishTimer < 0)
        {
            FishTimer = Random.Range(0.1f, 1) * TimeMult;
            fishSpeed = 0;
            float fishRandomPos = Random.Range(topPoint.position.y, BotPoint.position.y);
            fishDestination.transform.position = new Vector3(BotPoint.position.x, fishRandomPos, BotPoint.position.z);
        }

        Fish.transform.position = Vector3.Lerp(Fish.transform.position, fishDestination.transform.position, fishSpeed);
    }

    private void CheckDir()
    {
        _spriteFish.flipY = (Fish.transform.position.y > fishDestination.transform.position.y);
    }

    private void Catch()
    {
        float distance = Fish.transform.position.y - HookTrasn.position.y;

        if (Mathf.Abs(distance) < CaptureRange)
            Capture += Time.deltaTime / Random.Range(DifficultyCaptureMin, DifficultyCaptureMax);

        else Capture -= Time.deltaTime * Random.Range(DifficultyEscapeMin, DifficultyEscapeMax);
        Capture = Mathf.Clamp01(Capture);
        ChargeBar.transform.localScale = new Vector3(transform.localScale.x, Capture, 0.8f);

        if (Capture >= 1)
        {
            Victory = true;
            StartCoroutine(ResetGame());
        }
        else if (Capture <= 0)
        {
            Victory = false;
            StartCoroutine(ResetGame());
        }
    }

    private IEnumerator ResetGame()
    {
        if (Victory)
        {
            fishedPicked++;
            _inventory.fishes++;
            _textAmount[1].text = fishedPicked.ToString();

            Gaming = false;
            Fish.SetActive(false);
            HookTrasn.gameObject.SetActive(false);
            _myAudio.Play();
            _spriteFishWin.transform.DOScale(1f, 0.5f).OnComplete(() => _spriteFishWin.transform.DOScale(0f, 1f));
            yield return new WaitForSeconds(3f);
            _myAudio.Stop();
            overWatch = 0;
            start = false;
            Capture = CaptureResetScore;
            fishDestination.transform.position = StartPos.position;
            FishTimer = 0;
            HookTrasn.position = StartPos.position;
            Fish.transform.position = StartPos.position;
            Fish.SetActive(true);
            HookTrasn.gameObject.SetActive(true);

            HookTrasn.localScale = _initialScale;
            HookTrasn.transform.position = _initialPos;

            _inventory.baits--;
            Gaming = true;
        }
    }

    public void Reset()
    {
        StartCoroutine(ResetGame());
    }


    private void PlayerControl()
    {
        if (Input.GetKey(KeyCode.Space))
            HookTrasn.transform.position += Vector3.up * HookPower;

        else HookTrasn.transform.position += Vector3.down * GravityPower;
        HookTrasn.position = new Vector3(HookTrasn.position.x, Mathf.Clamp(HookTrasn.position.y, BotPoint.position.y, topPoint.position.y), HookTrasn.position.z);
    }

    private void CameraChange()
    {
        if (start && overWatch == 0)
        {
            ActivateMinigameUI();
            overWatch++;
            Gaming = !Gaming;

            if (Gaming)
            {
                _camPlayer.enabled = false;
                _camFish.enabled = true;

                _character.speed = 0;
                _character.FreezePlayer();
                _radar.StatusRadar(false);
            }

            else
            {
                _camPlayer.enabled = true;
                _camFish.enabled = false;

                _character.speed = _character.speedAux;
                _character.DeFreezePlayer();
                _radar.StatusRadar(true);
            }
        }
    }

    private void ActivateMinigameUI()
    {
        _canvasRenderFish.SetActive(true);
        foreach (TMP_Text text in _textAmount)
        {
            text.gameObject.SetActive(true);
        }
    }

    public void Quit()
    {
        _canvasRenderFish.SetActive(false);
        foreach (TMP_Text text in _textAmount)
        {
            text.gameObject.SetActive(false);
        }
        _myAudio.Stop();
        overWatch = 0;
        start = false;
        Gaming = false;
        Capture = CaptureResetScore;
        fishDestination.transform.position = StartPos.position;
        FishTimer = 0;
        HookTrasn.position = StartPos.position;
        Fish.transform.position = StartPos.position;

        _camPlayer.enabled = true;
        _camFish.enabled = false;

        _character.speed = _character.speedAux;
        _character.DeFreezePlayer();
        _radar.StatusRadar(true);
        fishedPicked = 0;
    }
}