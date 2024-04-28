using UnityEngine;
using TMPro;

public class Bush : MonoBehaviour
{
    [SerializeField] float _amountHit = 500f;
    [SerializeField] KeyCode _inputInteractive = KeyCode.Mouse0;
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textAmount;

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
        _boxMessage.gameObject.SetActive(false);
        _myAudio.Stop();
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKey(_inputInteractive))
        {
            if (!_myAudio.isPlaying) _myAudio.Play();

            Vector3 pos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            player.gameObject.transform.LookAt(pos);
            player.HitTree();
            _amountHit--;

            if (_amountHit <= 0)
            {
                _amountHit = 0;
                _message.ShowUI("+1", _boxMessage, _textAmount);
                _invetory.seeds++; 
                Destroy(gameObject);
            }
        }
    }
}