using UnityEngine;
using System.Collections;

public class AreaWolfSleeping : MonoBehaviour
{
    private WolfSleeping[] _wolfsSleeping;
    [SerializeField] Manager _manager;

    [SerializeField] Transform _posRestart;
    [SerializeField] GameObject _cinematic;

    public bool playerInArea = false;

    [Header("GAME OVER")]
    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] Character _player;
    [SerializeField] GameObject _restart;
    [SerializeField] Collider _myCol;
    [SerializeField] WolfSleeping _wolf;

    private void Awake()
    {
        _wolfsSleeping = FindObjectsOfType<WolfSleeping>();
    }

    void Start()
    {
        _cinematic.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) playerInArea = true;
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (!Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
                StartCoroutine(PlayerDetected());

            else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
                StartCoroutine(PlayerDetected());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _myCol.enabled = true;
            StopAllCoroutines();
        }
    }

    private IEnumerator PlayerDetected()
    {
        _myCol.enabled = false;
        _wolf.PlayerDetected("CAREFUL! YOU HAVE WOKE UP ALL THE WOLVES"); // Uso de referencia a un solo lobo ya que sino en el stay se repite el game over.
        yield return new WaitForSeconds(8f);

        foreach (var item in _wolfsSleeping)
        {
            item.DownSleepWolf();
        }

        _myCol.enabled = true;
    }
}