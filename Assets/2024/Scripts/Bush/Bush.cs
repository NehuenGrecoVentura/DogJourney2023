using UnityEngine;
using System.Collections;
using TMPro;

public class Bush : MonoBehaviour
{
    public float amountHit = 200f;
    [SerializeField] KeyCode _inputInteractive = KeyCode.Mouse0;
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textAmount;
    [SerializeField] HealthBarBush _hitBar;
    [SerializeField] MeshRenderer[] _meshes;

    private DoTweenManager _message;
    private CharacterInventory _invetory;
    private AudioSource _myAudio;
    private SpawnRandom _random;
    private Collider _myCol;
    private float _initialHit;

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
        _boxMessage.gameObject.SetActive(false);
        _myAudio.Stop();
        _initialHit = amountHit;
    }

    private void FocusToBrush(Character player)
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
        if (player != null)
        {
            FocusToBrush(player);

            if (Input.GetKey(_inputInteractive) && !_invetory.shovelSelected)
            {
                if (!_myAudio.isPlaying) _myAudio.Play();
                player.HitTree();
                amountHit--;
                _hitBar.Bar();

                if (amountHit <= 0)
                {
                    amountHit = 0;
                    _message.ShowUI("+1", _boxMessage, _textAmount);
                    _invetory.seeds++;
                    StartCoroutine(Respawn());
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _hitBar.gameObject.SetActive(false);
    }

    private IEnumerator Respawn()
    {
        _hitBar.gameObject.SetActive(false);
        _myCol.enabled = false;

        foreach (var item in _meshes)
        {
            item.enabled = false;
        }

        yield return new WaitForSeconds(2f);
        _random.SpawnObject(transform);

        foreach (var item in _meshes)
        {
            item.enabled = true;
        }

        _myCol.enabled = true;
        amountHit = _initialHit;
        _hitBar.Bar();
    }
}