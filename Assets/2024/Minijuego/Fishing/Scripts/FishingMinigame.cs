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
    [SerializeField] private float HookPower; //Cuanto sube el gancho
    [SerializeField] private float GravityPower; //Cuanto baja el gancho

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
    [SerializeField] TMP_Text[] _textAmount;
    public int fishedPicked = 0;

    [Header("AUDIOS")]
    private AudioSource _myAudio;

    //private void Awake()
    //{
    //    _myAudio = GetComponent<AudioSource>();
    //    _radar = FindObjectOfType<LocationQuest>();
    //}

    //private void Start()
    //{
    //    _spriteFishWin.transform.DOScale(0f, 0f);
    //    _canvasRenderFish.SetActive(false);

    //    foreach (var item in _textAmount)
    //        item.gameObject.SetActive(false);
    //}

    //void Update()
    //{
    //    CameraChange();
    //    if (Gaming)
    //    {
    //        FishMove();
    //        playerControl();
    //        Catch();
    //    }
    //}

    //void FishMove() //Movimiento del pes
    //{

    //    fishSpeed += Time.deltaTime * FishSpeedMult;
    //    FishTimer -= Time.deltaTime;
    //    CheckDir();

    //    if (FishTimer < 0)
    //    {
    //        FishTimer = Random.Range(0.1f, 1) * TimeMult; //El pez tiene un nuevo random timer
    //        fishSpeed = 0;
    //        fishRandomPos = Random.Range(topPoint.position.y, BotPoint.position.y); //El pez va a un nuevo random entre bot y top
    //        fishDestination.transform.position = new Vector3(BotPoint.position.x, fishRandomPos, BotPoint.position.z); // Muevo el objetivo del pez
    //    }

    //    Fish.transform.position =
    //        Vector3.Lerp(Fish.transform.position, fishDestination.transform.position, fishSpeed); //Movimiento del pez al objetivo
    //}

    //private void CheckDir()
    //{
    //    if (Fish.transform.position.y > fishDestination.transform.position.y) _spriteFish.flipY = true;
    //    else _spriteFish.flipY = false;
    //}

    //void Catch() //Atrapar al pez
    //{
    //    Distance = Fish.transform.position.y - HookTrasn.position.y;
    //    if (Distance < CaptureRange && Distance > -CaptureRange) //Si esta dentro del rango capture crece
    //    {
    //        Capture += Time.deltaTime / DifficultyCapture;
    //    }
    //    else //Si esta fuera del rango capture disminuye
    //    {
    //        Capture -= Time.deltaTime * DifficultyEscape;
    //    }

    //    if (Capture > 1) //Clamp de capture en max
    //    {
    //        Capture = 1;
    //        Debug.Log("Winner");
    //        Victory = true;
    //        StartCoroutine(Reset());
    //        //Quit();
    //        //_spriteFishWin.transform.DOScale(1f, 0.5f);
    //        //Fish.SetActive(false);
    //        //_spriteFishWin.transform.DOScale(1f, 0.5f).OnComplete(() => _spriteFishWin.transform.DOScale(0f, 1f));
    //    }

    //    if (Capture < 0) // Clamp de capture en min
    //    {
    //        Capture = 0;
    //        Debug.Log("Loser");
    //        Victory = false;
    //        //Quit();
    //    }
    //    //ChargeBar.transform.localScale = new Vector3(2, Capture, 0.8f); // el 2 y el 0.8 son los valores que tienen en el editor
    //    ChargeBar.transform.localScale = new Vector3(transform.localScale.x, Capture, 0.8f); // el 2 y el 0.8 son los valores que tienen en el editor
    //}

    //private IEnumerator Reset()
    //{
    //    if (Victory)
    //    {
    //        fishedPicked++;
    //        _textAmount[1].text = fishedPicked.ToString();

    //        Gaming = false;
    //        Fish.SetActive(false);
    //        HookTrasn.gameObject.SetActive(false);
    //        _myAudio.Play();
    //        _spriteFishWin.transform.DOScale(1f, 0.5f).OnComplete(() => _spriteFishWin.transform.DOScale(0f, 1f));
    //        yield return new WaitForSeconds(3f);
    //        _myAudio.Stop();
    //        overWatch = 0;
    //        start = false;
    //        Gaming = false;
    //        Capture = CaptureResetScore;
    //        fishDestination.transform.position = StartPos.position;
    //        FishTimer = 0;
    //        HookTrasn.position = StartPos.position;
    //        Fish.transform.position = StartPos.position;
    //        Fish.SetActive(true);
    //        HookTrasn.gameObject.SetActive(true);
    //        Gaming = true;
    //        Victory = false;
    //    }
    //}

    //void playerControl() //Subir o bajar la barra
    //{
    //    //Cuando apretas espacio, el gancho sube
    //    if (Input.GetKey(KeyCode.Space))  
    //        HookTrasn.transform.position += Vector3.up * HookPower;

    //    //Si soltas espacio el gancho baja
    //    else HookTrasn.transform.position += Vector3.down * GravityPower;

    //    //Clamp por arriba para que no se pase del top
    //    if (HookTrasn.transform.position.y >= topPoint.position.y) 
    //        HookTrasn.position = topPoint.position;

    //    //Clamp abajo para que no se pase del bot
    //    if (HookTrasn.transform.position.y <= BotPoint.position.y) 
    //        HookTrasn.position = BotPoint.position;
    //}

    //void CameraChange() //Cambiar a minijuego / juego
    //{
    //    //if (Input.GetKeyDown(KeyCode.Y)) //Aca se cambia todo
    //    if (start && overWatch == 0) //Para iniciar,que algun codigo ponga start en true
    //    {
    //        _canvasRenderFish.SetActive(true);

    //        foreach (var item in _textAmount)
    //            item.gameObject.SetActive(true);

    //        overWatch++;
    //        if (Gaming == true)
    //        {
    //            Gaming = false;
    //            Capture = CaptureResetScore;
    //            fishDestination.transform.position = StartPos.position;
    //            FishTimer = 0;
    //            HookTrasn.position = StartPos.position;
    //            Fish.transform.position = StartPos.position;
    //        }
    //        else
    //        {
    //            Gaming = true;
    //        }
    //        if (Gaming == true)
    //        {
    //            //camer1.SetActive(false);
    //            camer2.SetActive(true);
    //            //_character.enabled = false;

    //            _character.speed = 0;
    //            _character.FreezePlayer(RigidbodyConstraints.FreezeAll);
    //            _radar.StatusRadar(false);

    //            DifficultyCapture = Random.Range(DifficultyCaptureMin, DifficultyCaptureMax);
    //            DifficultyEscape = Random.Range(DifficultyEscapeMin, DifficultyEscapeMax);
    //        }

    //        if (Gaming == false)
    //        {
    //            //camer1.SetActive(true);
    //            camer2.SetActive(false);
    //            //_character.enabled = true;

    //            _character.speed = _character.speedAux;
    //            _character.FreezePlayer(RigidbodyConstraints.FreezeRotation);
    //            _radar.StatusRadar(true);
    //        }
    //    }
    //}

    //public void Quit()
    //{
    //    _canvasRenderFish.SetActive(false);

    //    foreach (var item in _textAmount)
    //        item.gameObject.SetActive(false);

    //    _myAudio.Stop();
    //    overWatch = 0;
    //    start = false;
    //    Gaming = false;
    //    Capture = CaptureResetScore;
    //    fishDestination.transform.position = StartPos.position;
    //    FishTimer = 0;
    //    HookTrasn.position = StartPos.position;
    //    Fish.transform.position = StartPos.position;
    //    camer1.SetActive(true);
    //    camer2.SetActive(false);
    //    _character.speed = _character.speedAux;
    //    _character.FreezePlayer(RigidbodyConstraints.FreezeRotation);
    //    _radar.StatusRadar(true);
    //    fishedPicked = 0;
    //}

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _radar = FindObjectOfType<LocationQuest>();
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
    }

    private void Update()
    {
        //CameraChange();
        //if (Gaming)
        //{
        //    FishMove();
        //    PlayerControl();
        //    Catch();
        //}



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
                //camer1.SetActive(false);
                //camer2.SetActive(true);

                _camPlayer.enabled = false;
                _camFish.enabled = true;

                _character.speed = 0;
                _character.FreezePlayer(RigidbodyConstraints.FreezeAll);
                _radar.StatusRadar(false);
            }

            else
            {
                //camer1.SetActive(true);
                //camer2.SetActive(false);

                _camPlayer.enabled = true;
                _camFish.enabled = false;

                _character.speed = _character.speedAux;
                _character.FreezePlayer(RigidbodyConstraints.FreezeRotation);
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

        //camer1.SetActive(true);
        //camer2.SetActive(false);

        _camPlayer.enabled = true;
        _camFish.enabled = false;

        _character.speed = _character.speedAux;
        _character.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _radar.StatusRadar(true);
        fishedPicked = 0;
    }
}