using UnityEngine;
using TMPro;

public class Bush : MonoBehaviour
{
    public float amountHit = 200f;
    [SerializeField] KeyCode _inputInteractive = KeyCode.Mouse0;
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textAmount;
    [SerializeField] GameObject _decal;
    [SerializeField] HealthBarBush _hitBar;

    private DoTweenManager _message;
    private CharacterInventory _invetory;
    private AudioSource _myAudio;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _message = FindObjectOfType<DoTweenManager>();
        _invetory = FindObjectOfType<CharacterInventory>();
    }

    private void Start()
    {
        _decal.SetActive(false);
        _hitBar.gameObject.SetActive(false);
        _boxMessage.gameObject.SetActive(false);
        _myAudio.Stop();
    }

    private void FocusToBrush(Character player)
    {
        _decal.gameObject.SetActive(true);
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

            if (Input.GetKey(_inputInteractive))
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
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _decal.SetActive(false);
            _hitBar.gameObject.SetActive(false);
        }
    }
}