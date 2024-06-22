using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manzanas_Player : MonoBehaviour
{
    [SerializeField] GameObject Box;
    [SerializeField] int Lives;
    [SerializeField] private int MaxLives; 
    [SerializeField] Rigidbody _rb;
    [SerializeField] float speed;
    [SerializeField] private float baseSpeed;
    [SerializeField] Transform Izquierda;
    [SerializeField] Transform Derecha;
    [SerializeField] float Energy;
    [SerializeField] float MaxEnergy;
    public bool GameOver;
    // [SerializeField] Image bar;

    [Header("PLAYER MESH")]
    [SerializeField] Animator _myAnim;
    [SerializeField] Collider _myCol;
    [SerializeField] GameObject _applesContainer;
    private GameObject[] _apples;
    private int _indexApple = 0;

    [Header("UI SCORE")]
    [SerializeField] Image[] _spritesApples;
    [SerializeField] TMP_Text _textScore;
    [SerializeField] TMP_Text _textScoreChain;
    [SerializeField] int Score;
    [SerializeField] ChainParkQuest _chainQuest;
    [SerializeField] Manzana_Manager _manager;
    private CharacterInventory _inventory;

    [Header("AUDIO")]
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioClip _soundGood;
    [SerializeField] AudioClip _soundFail;

    private void Awake()
    {
        _inventory = FindObjectOfType<CharacterInventory>();
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _apples = new GameObject[_applesContainer.transform.childCount];
        for (int i = 0; i < _applesContainer.transform.childCount; i++)
        {
            _apples[i] = _applesContainer.transform.GetChild(i).gameObject;
            _apples[i].SetActive(false);
        }

    }

    public void ShowGlobalScore()
    {
        if (_chainQuest != null && _chainQuest.questActive)
            _textScoreChain.gameObject.SetActive(true);

        else _textScoreChain.gameObject.SetActive(false);
    }

    public void ResetScore()
    {
        Score = 0;
        _textScore.text = "Score: " + Score.ToString();
    }

    public void AddScore()
    {
        Score++;
        _inventory.tickets++;

        if (_indexApple < _apples.Length)
        {
            _apples[_indexApple].SetActive(true);
            _indexApple++;
        }

        _manager.RespawnApplesInBarriel();

        //if (_chainQuest != null && _chainQuest.questActive) _chainQuest.AddScore(_inventory.tickets, _textScoreChain);
        if (_chainQuest != null && _chainQuest.questActive) _textScoreChain.text = "TOTAL SCORE: " + _inventory.tickets.ToString();
        _textScore.text = "Score: " + Score.ToString();
        _myAudio.PlayOneShot(_soundGood);


        if (Score == 5) // Comprueba si Score es un múltiplo de 5
        {
            _manager.ChangeSpeed();
        }

        if (Score == 10) // Comprueba si Score es un múltiplo de 5
        {
            _manager.ChangeSpeed();
        }

        if (Score == 15) // Comprueba si Score es un múltiplo de 5
        {
            //_manager.ChangeSpeed();

            _manager.Win();


        }
    }

    public void RemoveLive()
    {
        //Lives--;

        _myAudio.PlayOneShot(_soundFail);

        if (Lives > 0)
        {
            Lives--;

            // Esta funcion quita el �ltimo icono de vida
            if (Lives >= 0) _spritesApples[Lives].gameObject.SetActive(false);
            else return;
        }

        else return;
    }

    public void Reset()
    {
        _rb.velocity = Vector3.zero;
        Score = 0;
        Lives = MaxLives;

        foreach (Image icon in _spritesApples)
        {
            icon.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Lives <= 0)
        {
            GameOver = true;
        }

        if (!GameOver)
        {
            if(Input.GetKey(KeyCode.LeftShift) == true)
            {
                Run(true);
            }
            else
            {
                Run(false);
            }
            //var hor = Input.GetAxis("Horizontal");
            //if (hor != 0)
            //{
            //    Move(new Vector3(-hor, 2.85f, 0));
            //}
        }
       
    }

    private void FixedUpdate()
    {
        // Obtener la entrada horizontal (A o D, o las flechas izquierda y derecha)
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Calcular el vector de movimiento basado en la entrada y la velocidad
        Vector3 movement = new Vector3(-moveHorizontal, 0.0f, 0.0f) * speed;

        // Aplicar el movimiento al Rigidbody
        _rb.velocity = movement;

        if (moveHorizontal != 0) _myAnim.SetBool("Move", true);
        else _myAnim.SetBool("Move", false);
    }

    public void Move(Vector3 dir)
    {
        dir.y = 2.85f;
        dir.z = 0;
        _rb.velocity = dir * speed;
        Box.transform.forward = dir;
    }
    
    public void Run(bool isRun)
    {
        if (Energy > 0)
        {
            if (isRun)
            {
                speed = baseSpeed * 2.5f;
                Energy = Energy - 1f * Time.deltaTime;
            }
            if (Energy <= 0) speed = baseSpeed;
        }

        if (!isRun)
        {
            speed = baseSpeed;
            Energy = Energy + 2f * Time.deltaTime;

            if (Energy > MaxEnergy) Energy = MaxEnergy;
        }

       //bar.fillAmount = Energy / MaxEnergy;
    }

   /* private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.name == "OutBoxI")
        //{
        //    transform.position = Derecha.position;

        //}
        //if(other.gameObject.name == "OutBoxD")
        //{
        //    transform.position = Izquierda.position;
        //}

        // Fix para que no se buguee el mesh del player cuando cambia de posici�n
        if (other.gameObject.name == "OutBoxI")
        {
            StartCoroutine(SpawnSide(Derecha.position));

        }
        if (other.gameObject.name == "OutBoxD")
        {
            StartCoroutine(SpawnSide(Izquierda.position));
        }
    }*/

    private IEnumerator SpawnSide(Vector3 pos)
    {
        transform.position = pos;
        transform.position = new Vector3(transform.position.x, 9.59f, transform.position.z);
        _myCol.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _myCol.enabled = true;
    }
}