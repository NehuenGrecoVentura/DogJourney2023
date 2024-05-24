using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    private void Start()
    {
        _myAudio = GetComponent<AudioSource>();
        _radar = FindObjectOfType<LocationQuest>();
        _inventory = FindObjectOfType<CharacterInventory>();
        Gaming = false;
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
            
        }
    }
}

