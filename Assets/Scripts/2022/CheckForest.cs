using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckForest : MonoBehaviour
{
    [Header("TEXTS CONFIG")]
    [SerializeField] TMP_Text _text;
    [SerializeField] string _nextMessage;

    [Header("UI CONFIG")]
    public GameObject _uiDogHelp;

    [Header("AUDIO CONFIG")]
    public AudioClip sadDogSound;
    public AudioSource asDog;

    [Header("TRANSFORM CONFIG")]
    [SerializeField] Transform movePos;
    public Transform _nextLocation;

    [Header("ICONS CONFIG")]
    public GameObject iconDialogueCry;
    public Image iconDogCryQuest;
    public Image iconForest;

    private void Start()
    {
        iconDialogueCry.gameObject.SetActive(false);
        _uiDogHelp.SetActive(false);
        iconDogCryQuest.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        var playerWithAxe = other.CompareTag("Axe");

        if (playerWithAxe)
        {
            _text.text = _nextMessage;
            iconDialogueCry.gameObject.SetActive(true);
            asDog.PlayOneShot(sadDogSound);
            _uiDogHelp.SetActive(true);
            iconDogCryQuest.gameObject.SetActive(true);
            Destroy(iconForest);
            Destroy(gameObject);
        }

        if (player != null && !playerWithAxe)
            transform.position = movePos.position;
    }
}