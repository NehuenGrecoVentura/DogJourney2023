using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniApple : MonoBehaviour
{
    [SerializeField] Transform _initialPos;
    [SerializeField] Transform _exitPos;

    [SerializeField] Character _player;
    private Rigidbody _rbPlayer;
    private Animator _animPlayer;
    [SerializeField] float _speedPlayer = 5f;

    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] Camera _camMiniGame;
    private bool _isActive = false;

    [SerializeField] GameObject _objectToSpawn;
    [SerializeField] GameObject _objectFromSpawn;
    [SerializeField] RuntimeAnimatorController _animController;
    private RuntimeAnimatorController _initialController;


    private void Awake()
    {
        _rbPlayer = _player.GetComponent<Rigidbody>();
        _animPlayer = _player.GetComponent<Animator>();
    }

    void Start()
    {
        _camMiniGame.gameObject.SetActive(false);
        _initialController = _animPlayer.runtimeAnimatorController;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!_isActive)
            {
                _camPlayer.gameObject.SetActive(false);
                _camMiniGame.gameObject.SetActive(true);
                _player.transform.position = _initialPos.position;
                _player.ItemsPicked(false, false, false, true);
                _player.rabbitPicked = true;
                _player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                _player.enabled = false;
                _animPlayer.runtimeAnimatorController = _animController;
                _isActive = true;
            }

            else
            {
                _camPlayer.gameObject.SetActive(true);
                _camMiniGame.gameObject.SetActive(false);
                _animPlayer.runtimeAnimatorController = _initialController;
                _player.transform.position = _exitPos.position;
                _player.rabbitPicked = true;
                _player.enabled = true;
                _isActive = false;
            }
        }

        if (_isActive && Input.GetKeyDown(KeyCode.Space)) SpawnObject();
    }

    private void SpawnObject()
    {
        // Obtener el ancho y largo del objeto de spawn
        float halfWidth = _objectFromSpawn.transform.localScale.x / 2f;
        float halfLength = _objectFromSpawn.transform.localScale.z / 2f;

        // Generar coordenadas aleatorias dentro del área de spawn
        float randomX = Random.Range(-halfWidth, halfWidth);
        float randomZ = Random.Range(-halfLength, halfLength);

        // Crear posición de instancia
        Vector3 spawnPosition = new Vector3(
            _objectFromSpawn.transform.position.x + randomX,
            _objectFromSpawn.transform.position.y,
            _objectFromSpawn.transform.position.z + randomZ
        );

        // Instanciar objeto en la posición generada
        Instantiate(_objectToSpawn, spawnPosition, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        // Obtener la entrada horizontal (A o D, o las flechas izquierda y derecha)
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Calcular el vector de movimiento basado en la entrada y la velocidad
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f) * _speedPlayer;

        // Aplicar el movimiento al Rigidbody
        _rbPlayer.velocity = movement;

        if (moveHorizontal != 0) _animPlayer.SetBool("Move", true);
        else _animPlayer.SetBool("Move", false);

    }
}