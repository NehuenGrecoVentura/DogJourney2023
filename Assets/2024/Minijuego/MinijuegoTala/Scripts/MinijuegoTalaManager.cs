using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private int Score;
    private void Start()
    {
        _myAudio = GetComponent<AudioSource>();
        _radar = FindObjectOfType<LocationQuest>();
        _inventory = FindObjectOfType<CharacterInventory>();
        Gaming = false;
        SpawnCoder();
        
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
            if (Timer >= MaxTimer)
            {
                Gaming = false;
                Game();
                Debug.Log("Perdiste");
                Debug.Log("tu escore fue de " + Score);
            }
            if (Move1)
            {
                CallCoder();
                Move1 = false;
            }
            CheckDones();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Gaming = !Gaming;
            Game();
        }
    }
    private void Game()
    {
        if (Gaming)
        {
            _camPlayer.enabled = false;
            _camTala.enabled = true;
            _character.speed = 0;
            _character.FreezePlayer();
            _radar.StatusRadar(false);
            Reset();
            CallCoder();
        }
        else
        {
            _camPlayer.enabled = true;
            _camTala.enabled = false;
            _character.speed = _character.speedAux;
            _character.DeFreezePlayer();
            _radar.StatusRadar(true);
        }
    }
}
