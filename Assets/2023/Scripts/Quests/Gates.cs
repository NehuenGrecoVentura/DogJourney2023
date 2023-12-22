using UnityEngine;
using System.Collections;

public class Gates : MonoBehaviour
{
    [SerializeField] GameObject _messageGates;
    [SerializeField] float _timeToOpen = 3f;
    public GameObject[] iconQuestHouses;
    public Collider[] colHouses;
    [SerializeField] Collider[] _myCols;
    public GameObject[] puzzleQuest6;
    private CharacterInventory _inventory;
    private Animator _myAnim;

    [Header("PURPLE TREES")]
    [HideInInspector] public GameObject[] purpleTrees;

    private HouseQuest3 _quest3;
    private HouseQuest4 _quest4;
    private HouseQuest5 _quest5;
    private HouseQuest6 _quest6;
    private LocationQuest _radar;

    [Header("MESSAGE SLIDE")]
    [SerializeField] Sprite _iconOil;
    private MessageSlide _messageSlide;

    [Header("AUDIO")]
    [SerializeField] AudioClip _audioGates;
    private AudioSource _myAudio;

    private void Awake()
    {
        _myAnim = GetComponent<Animator>();
        _myAudio = GetComponent<AudioSource>();

        purpleTrees = GameObject.FindGameObjectsWithTag("Purple Tree");
        _inventory = FindObjectOfType<CharacterInventory>();
        _quest3 = FindObjectOfType<HouseQuest3>();
        _quest4 = FindObjectOfType<HouseQuest4>();
        _quest5 = FindObjectOfType<HouseQuest5>();
        _quest6 = FindObjectOfType<HouseQuest6>();
        _radar = FindObjectOfType<LocationQuest>();
        _messageSlide = FindObjectOfType<MessageSlide>();
    }

    private void Start()
    {
        _quest3.enabled = false;
        _quest4.enabled = false;
        _quest5.enabled = false;
        _quest6.enabled = false;
        _myAnim.enabled = false;
        _messageGates.SetActive(false);

        foreach (var item in puzzleQuest6)
            item.SetActive(false);

        foreach (var house in colHouses)
            house.enabled = false;

        foreach (var icon in iconQuestHouses)
            icon.SetActive(false);

        foreach (var purpleTree in purpleTrees)
            purpleTree.GetComponent<Collider>().enabled = false;
    }

    public void UnlockNPCQuest3()
    {
        _quest3.enabled = true;
        iconQuestHouses[0].SetActive(true);
        colHouses[0].enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Character>();
        if (player != null) _messageGates.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _inventory.oils == 4)
        {
            _messageSlide.ShowMessage("HOLD TO OPEN THE DOOR", _iconOil);

            if (Input.GetKey(KeyCode.Space))
            {
                player.PlayAnim("Build");
                StartCoroutine(Build());

            }

            else StopAllCoroutines();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _inventory.oils == 4)
            StopAllCoroutines();
    }

    private IEnumerator Build()
    {
        if (!_myAudio.isPlaying)
            _myAudio.PlayOneShot(_audioGates);
        _radar.StatusRadar(false);
        yield return new WaitForSeconds(_timeToOpen);
        _myAnim.enabled = true;
        _inventory.oils = 0;

        foreach (var col in _myCols)
            Destroy(col);

        Destroy(this);
    }
}