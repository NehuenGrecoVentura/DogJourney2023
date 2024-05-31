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

    [Header("UI SCORE")]
    [SerializeField] Image[] _spritesApples;
    [SerializeField] TMP_Text _textScore;
    [SerializeField] int Score;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void AddScore()
    {
        Score++;
        _textScore.text = "Score: " + Score.ToString();
    }

    public void RemoveLive()
    {
        //Lives--;

        if (Lives > 0)
        {
            Lives--;

            // Esta funcion quita el último icono de vida
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

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.name == "OutBoxI")
        //{
        //    transform.position = Derecha.position;

        //}
        //if(other.gameObject.name == "OutBoxD")
        //{
        //    transform.position = Izquierda.position;
        //}

        // Fix para que no se buguee el mesh del player cuando cambia de posición
        if (other.gameObject.name == "OutBoxI")
        {
            StartCoroutine(SpawnSide(Derecha.position));

        }
        if (other.gameObject.name == "OutBoxD")
        {
            StartCoroutine(SpawnSide(Izquierda.position));
        }
    }

    private IEnumerator SpawnSide(Vector3 pos)
    {
        transform.position = pos;
        transform.position = new Vector3(transform.position.x, 9.59f, transform.position.z);
        _myCol.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _myCol.enabled = true;
    }
}