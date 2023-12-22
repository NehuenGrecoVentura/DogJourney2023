using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _keyInteractive = KeyCode.Q;
    public CharacterInventory inventory;

    [Header("MESSAGE SLIDER")]
    [SerializeField] Sprite _iconBox;
    [SerializeField] string _messageText;
    private MessageSlide _messageSlide;

    private void Awake()
    {
        _messageSlide = FindObjectOfType<MessageSlide>();
    }

    private void Start()
    {
        _iconInteractive.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var dog = other.GetComponent<Dog>();
        if (dog != null)
        {
            if (!inventory.upgradeLoot)
            {
                inventory.upgradeLoot = true;
                Destroy(gameObject);
            }

            else _messageSlide.ShowMessage(_messageText, _iconBox);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (Input.GetKeyDown(_keyInteractive))
                _iconInteractive.SetActive(false);

            else _iconInteractive.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteractive.SetActive(false);
    }
}