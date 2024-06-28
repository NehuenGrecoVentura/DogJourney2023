using System.Collections;
using UnityEngine;
using TMPro;

public class Dig : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] KeyCode _inputInteractive = KeyCode.Mouse0;
    private AudioSource _myAudio;
    private Collider _myCol;

    [Header("HIT")]
    [SerializeField] HitBarFlower _hitBar;
    public float amountHit = 200f;
    private float _initialHit;

    [Header("INVENTORY UI")]
    [SerializeField] RectTransform _boxSlide;
    [SerializeField] TMP_Text _textAmount;
    private DoTweenManager _message;
    private CharacterInventory _invetory;

    [Header("RESPAWN")]
    [SerializeField] float _timeToRespawn = 5f;
    [SerializeField] MeshRenderer[] _myMeshes;
    private SpawnRandom _random;
    
    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _myCol = GetComponent<Collider>();

        _message = FindObjectOfType<DoTweenManager>();
        _invetory = FindObjectOfType<CharacterInventory>();
        _random = FindObjectOfType<SpawnRandom>();
    }

    private void Start()
    {
        _hitBar.gameObject.SetActive(false);
        _initialHit = amountHit;
    }

    private void FocusToFlower(Character player)
    {
        Vector3 pos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        player.gameObject.transform.LookAt(pos);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _hitBar.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled)
        {
            if (!Input.GetKey(_inputInteractive))
            {
                _hitBar.gameObject.SetActive(true);
                player.enabled = true;
                player.MainAnim();
            }

            else
            {
                if (_invetory.shovelSelected)
                {
                    FocusToFlower(player);
                    player.HitDig();
                    player.enabled = false;
                    amountHit--;
                    if (!_myAudio.isPlaying) _myAudio.Play();
                    _hitBar.Bar();
                }
            }

            if (amountHit <= 0)
            {
                amountHit = 0;
                _myAudio.Stop();
                _invetory.flowers++;
                //if (_invetory.flowers < 4) _message.ShowUI("+1", _boxSlide, _textAmount); // CAMBIAR CUANDO EXPANDAMOS A LA ISLA 2 ESTA LINEA.
                _message.ShowUI("+1", _boxSlide, _textAmount);
                _hitBar.gameObject.SetActive(false);
                StartCoroutine(Respawn());
                player.DeFreezePlayer();
                player.enabled = true;
                player.MainAnim();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled)
        {
            _myAudio.Stop();
            _hitBar.gameObject.SetActive(false);
            player.enabled = true;
            player.MainAnim();
        }
    }

    private IEnumerator Respawn()
    {
        _hitBar.gameObject.SetActive(false);

        foreach (var item in _myMeshes)
        {
            item.enabled = false;
        }

        _myCol.enabled = false;
        yield return new WaitForSeconds(_timeToRespawn);
        _random.SpawnObject(transform);

        foreach (var item in _myMeshes)
        {
            item.enabled = true;
        }

        _myCol.enabled = true;
        amountHit = _initialHit;
        _hitBar.Bar();
    }
}