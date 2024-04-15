using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class FishingMinigame : MonoBehaviour
{
    //CameraControl
    [Header("CAMERA CONTROL")]
    [SerializeField] private GameObject camer1; //Camara del juego
    [SerializeField] private GameObject camer2; //Camara del minijuego
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
    float fishSpeed; //Velocidad del pez
    [SerializeField] private float FishSpeedMult; // Multiplicador de velocidad del pez

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
    [SerializeField] private float Difficulty; //Dificultad, cuanto mas alto, mas rapido se desaparece la barra y mas tarda en cargarse
    [SerializeField] private float CaptureRange; //Rango en la que el pez es atrapado

    // Reset
    [Header("RESET")]
    [SerializeField] private Transform StartPos; //Posicion inicial de las cosas
    [SerializeField] public bool Gaming; // Si tas en el minijuego o no

    private LocationQuest _radar;

    private void Awake()
    {
        _radar = FindObjectOfType<LocationQuest>();
    }

    void Update()
    {
        CameraChange();
        if (Gaming)
        {
            FishMove();
            playerControl();
            Catch();
        }
    }

    void FishMove() //Movimiento del pes
    {

        fishSpeed += Time.deltaTime * FishSpeedMult;
        FishTimer -= Time.deltaTime;
        if (FishTimer < 0)
        {
            FishTimer = Random.Range(0.1f, 1) * TimeMult; //El pez tiene un nuevo random timer
            fishSpeed = 0;
            fishRandomPos = Random.Range(topPoint.position.y, BotPoint.position.y); //El pez va a un nuevo random entre bot y top
            fishDestination.transform.position = new Vector3(BotPoint.position.x, fishRandomPos, BotPoint.position.z); // Muevo el objetivo del pez
        }

        Fish.transform.position =
            Vector3.Lerp(Fish.transform.position, fishDestination.transform.position, fishSpeed); //Movimiento del pez al objetivo
    }

    void Catch() //Atrapar al pez
    {
        Distance = Fish.transform.position.y - HookTrasn.position.y;
        if (Distance < CaptureRange && Distance > -CaptureRange) //Si esta dentro del rango capture crece
        {
            Capture += Time.deltaTime / Difficulty;
        }
        else //Si esta fuera del rango capture disminuye
        {
            Capture -= Time.deltaTime * Difficulty;
        }

        if (Capture > 1) //Clamp de capture en max
        {
            Capture = 1;
        }

        if (Capture < 0) // Clamp de capture en min
        {
            Capture = 0;
        }
        ChargeBar.transform.localScale = new Vector3(2, Capture, 0.8f); // el 2 y el 0.8 son los valores que tienen en el editor
    }


    void playerControl() //Subir o bajar la barra
    {
        if (Input.GetKey(KeyCode.Space))  //Cuando apretas espacio, el gancho sube
        {
            HookTrasn.transform.position += Vector3.up * HookPower;
        }
        else //Si soltas espacio el gancho baja
        {
            HookTrasn.transform.position += Vector3.down * GravityPower;
        }

        if (HookTrasn.transform.position.y >= topPoint.position.y) //Clamp por arriba para que no se pase del top
        {
            HookTrasn.position = topPoint.position;
        }
        if (HookTrasn.transform.position.y <= BotPoint.position.y) //Clamp abajo para que no se pase del bot
        {
            HookTrasn.position = BotPoint.position;
        }

    }

    void CameraChange() //Cambiar a minijuego / juego
    {
        if (Input.GetKeyDown(KeyCode.Y)) //Aca se cambia todo
        {
            if (Gaming == true)
            {
                Gaming = false;
                Capture = 0.5f;
                fishDestination.transform.position = StartPos.position;
                FishTimer = 0;
                HookTrasn.position = StartPos.position;
                Fish.transform.position = StartPos.position;
            }
            else
            {
                Gaming = true;
            }
            if (Gaming == true)
            {
                camer1.SetActive(false);
                camer2.SetActive(true);
                //_character.enabled = false;

                _character.speed = 0;
                _character.FreezePlayer(RigidbodyConstraints.FreezeAll);
                _radar.StatusRadar(false);
            }

            if (Gaming == false)
            {
                camer1.SetActive(true);
                camer2.SetActive(false);
                //_character.enabled = true;

                _character.speed = _character.speedAux;
                _character.FreezePlayer(RigidbodyConstraints.FreezeRotation);
                _radar.StatusRadar(true);
            }
        }
    }

    public void FishQuest(KeyCode keyFish)
    {
        if (Input.GetKeyDown(keyFish)) //Aca se cambia todo
        {
            if (Gaming == true)
            {
                Gaming = false;
                Capture = 0.5f;
                fishDestination.transform.position = StartPos.position;
                FishTimer = 0;
                HookTrasn.position = StartPos.position;
                Fish.transform.position = StartPos.position;
            }
            else
            {
                Gaming = true;
            }
            if (Gaming == true)
            {
                camer1.SetActive(false);
                camer2.SetActive(true);
                //_character.enabled = false;

                _character.speed = 0;
                _character.FreezePlayer(RigidbodyConstraints.FreezeAll);
                _radar.StatusRadar(false);
            }

            if (Gaming == false)
            {
                camer1.SetActive(true);
                camer2.SetActive(false);
                //_character.enabled = true;

                _character.speed = _character.speedAux;
                _character.FreezePlayer(RigidbodyConstraints.FreezeRotation);
                _radar.StatusRadar(true);
            }
        }
    }
}

