using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mailbox : MonoBehaviour
{
    [SerializeField] Image _mail;
    [SerializeField] GameObject simbolQuest;
    [SerializeField] TMP_Text _textAxe;
    [SerializeField] AudioClip _soundNotification;
    
    [Header("ICON LOCATION QUEST")]
    [SerializeField] GameObject _iconLocation;
    [SerializeField] Image _iconQuest;
    [SerializeField] Transform _nextLocation;

    AudioSource _myAudio;
    Character2022 _player;
    Mailbox _mailBox;
    LocationQuest _map;
    [SerializeField] private GameObject containBox;
    [SerializeField] private GameObject _panelMail;
    
    [Header("AXE CONFIG")]
    [SerializeField] BoxCollider _axeCol;
    [SerializeField] GameObject _arrowAxe;

    //[Header("CAMERA CONFIG")]
    //[SerializeField] Cinemachine.CinemachineFreeLook _cam;
    CameraOrbit _cam;

    void Awake()
    {
        _mailBox = GetComponent<Mailbox>();
        _myAudio = GetComponent<AudioSource>();
        _player = FindObjectOfType<Character2022>();
        _map = FindObjectOfType<LocationQuest>();
        _cam = FindObjectOfType<CameraOrbit>();
    }

    void Start()
    {
        _iconQuest.gameObject.SetActive(false);
        _iconLocation.gameObject.SetActive(false);
        _mail.gameObject.SetActive(false);
        _textAxe.gameObject.SetActive(false);
        _axeCol.enabled = false;
        _arrowAxe.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Character2022>();
        if (player != null)
        {
            simbolQuest.SetActive(false); // DESAPARECE EL SIGNO ! DEL BUZON
            _mail.gameObject.SetActive(true); // SE ACTIVA LA CARTA DE LA MISION
            containBox.transform.position = _player.transform.position;
            containBox.SetActive(true);
            _player.speed = 0; // EL JUGADOR QUEDA PARADO EN EL MAIL
            //_cam.enabled = false; // DESACTIVO EL MOVIMIENTO DE LA CAMARA CUANDO INTERACTUO CON EL BUZON
            _cam.sensitivity = new Vector2(0,0);
            Destroy(_panelMail); // SE DESTRUYE EL MENSAJE DE IR AL BUZON
            _player.EjecuteAnim("Idle");
            Time.timeScale = 0;
        }
    }

    public void Accept()
    {      
        _player.speed = 10f;
        _myAudio.PlayOneShot(_soundNotification);
        containBox.SetActive(false);
        Destroy(_mail);
        _textAxe.gameObject.SetActive(true);
        _iconLocation.gameObject.SetActive(true);
        _iconQuest.gameObject.SetActive(true);
        _axeCol.enabled = true;
        _arrowAxe.SetActive(true);
        //_cam.enabled = true;
        _cam.sensitivity = new Vector2(3, 3);
        _map.target = _nextLocation;
        Destroy(_mailBox);
    }
}