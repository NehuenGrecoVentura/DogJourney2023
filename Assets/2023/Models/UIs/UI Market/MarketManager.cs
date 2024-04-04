using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    private Character _player;
    private CharacterInventory _inventory;

    [SerializeField] GameObject _canvas;

    [Header("NAILS")]
    [SerializeField] int _costNails = 50;
    [SerializeField] int _amountNails = 20;

    [Header("ROPES")]
    [SerializeField] int _costRopes = 100;
    [SerializeField] int _amountRopes = 60;

    [Header("AUDIO")]
    [SerializeField] AudioClip[] _sounds;
    private AudioSource _myAudio;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _player = FindObjectOfType<Character>();
        _inventory = FindObjectOfType<CharacterInventory>();
    }

    private void Start()
    {
        _canvas.SetActive(false);
    }

    public void BuyNails()
    {
        BuyMaterial(_costNails, ref _inventory.nails, _amountNails);
    }

    public void BuyRopes()
    {
        BuyMaterial(_costNails, ref _inventory.ropes, _amountRopes);
    }

    public void OpenMarket()
    {
        _canvas.SetActive(true);
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        _player.speed = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitMarket()
    {
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _player.speed = _player.speedAux;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void BuyMaterial(int cost, ref int materialInInvetory, int addMaterial)
    {
        if (_inventory.money >= cost)
        {
            _myAudio.PlayOneShot(_sounds[1]);
            materialInInvetory += addMaterial;
            _inventory.money -= cost;
            print("COMPRE MATERIAL");
        }

        else 
        {
            _myAudio.PlayOneShot(_sounds[0]);
            print("TE FALTA PLATA");
        }              
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _canvas.SetActive(true);
            player.FreezePlayer(RigidbodyConstraints.FreezeAll);
            player.speed = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}