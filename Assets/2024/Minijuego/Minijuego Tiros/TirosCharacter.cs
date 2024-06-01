using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TirosCharacter : MonoBehaviour
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
    [SerializeField] private Transform spawnPointGun;
    [SerializeField] private float ShootCD;
    [SerializeField] private float timer;
    [SerializeField] private bool CanShoot;
    public bool GameOver;
    // [SerializeField] Image bar;

    [Header("MESH PLAYER")]
    [SerializeField] Animator _animPlayer;

    [Header("UI SCORE")]
    [SerializeField] int Score;
    [SerializeField] TMP_Text _textScore;
    [SerializeField] Image[] _spritesLifes;

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

            // Esta funcion quita el último icono de vida activo
            if (Lives >= 0) _spritesLifes[Lives].gameObject.SetActive(false);
            else return;
        }

        else return;
    }

    public void Reset()
    {
        _rb.velocity = Vector3.zero;
        Score = 0;
        Lives = MaxLives;

        foreach (Image icon in _spritesLifes)
        {
            icon.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Lives <= 0) { GameOver = true; }
        if (!GameOver)
        {
            CheckGun();
            CheckMove();
        }

    }

    void CheckGun()
    {
        timer += Time.deltaTime;
        if (timer >= ShootCD)
        {
            CanShoot = true;
        }
        else
        {
            CanShoot = false;
        }
        if (CanShoot)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Spawn();
                timer = 0;
            }
        }
    }

    void CheckMove()
    {

        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            Run(true);
        }
        else
        {
            Run(false);
        }
        var hor = Input.GetAxis("Horizontal");
        if (hor != 0)
        {
            Move(new Vector3(-hor, 5f, 0));
            _animPlayer.SetBool("Move", true);
        }
        else
        {
            _rb.velocity = Vector3.zero;
            _animPlayer.SetBool("Move", false);
        }
    }

    public void Move(Vector3 dir)
    {
        dir.y = 5f;
        dir.z = 0;
        _rb.velocity = dir * speed;
    }

    void Spawn()
    {
        var b = BalaFactory.Instance.pool.GetObject();
        b.transform.position = spawnPointGun.position + transform.forward;
        b.transform.forward = spawnPointGun.forward;
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
        if (other.gameObject.name == "OutBoxI")
        {
            transform.position = Derecha.position;
        }
        if (other.gameObject.name == "OutBoxD")
        {
            transform.position = Izquierda.position;
        }
    }
}
