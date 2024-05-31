using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static List<int> userList, SimonList;
    public bool SimonDiciendo;
    
     
    private void Start()
    {
        _myAudio = GetComponent<AudioSource>();
        _radar = FindObjectOfType<LocationQuest>();
        _inventory = FindObjectOfType<CharacterInventory>();
        Gaming = false;
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

    public void PlayerClicking(SimonBoton b)
    {
        userList.Add(b.ID);
        if(userList[userList.Count-1] != SimonList[userList.Count-1])
        {
            Debug.Log("GameOver");
            Gaming = false;
            Game();
        }
        else if (userList.Count == SimonList.Count)
        {
            Next();
            Debug.Log("next Lvl");
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
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
            _camSimon.enabled = true;
            _character.speed = 0;
            _character.FreezePlayer();
            _radar.StatusRadar(false);
            UnlockMouse();
            Reset();
        }
        else
        {
            _camPlayer.enabled = true;
            _camSimon.enabled = false;
            _character.speed = _character.speedAux;
            _character.DeFreezePlayer();
            _radar.StatusRadar(true);
            LockMouse();
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


}
