using UnityEngine;
using TMPro;

public class ManagerQuest1 : MonoBehaviour
{
    [Header("NEXT STEP")]
    [SerializeField] string _thirdTextQuest = "Leave it in front of his house";
    [SerializeField] GameObject _arrowBox;
    [SerializeField] TMP_Text _textWoods;

    private CharacterInventory _inventory;
    private QuestManager _questManager;
    private Manager _gm;

    [Header("RADAR")]
    [SerializeField] Transform _boxPos;
    private LocationQuest _radar;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundNotification;
    private AudioSource _myAudio;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();

        _inventory = FindObjectOfType<CharacterInventory>();
        _questManager = FindObjectOfType<QuestManager>();
        _radar = FindObjectOfType<LocationQuest>();
        _gm = FindObjectOfType<Manager>();
    }

    private void Update()
    {
        CheckWoods();
    }

    private void CheckWoods()
    {

        _textWoods.text = "WOODS: " + _inventory.greenTrees.ToString() + " /" + "10";

        if (_inventory.greenTrees >= 10)
        {
            if (!_myAudio.isPlaying) 
                _myAudio.PlayOneShot(_soundNotification);

            _arrowBox.SetActive(true);
            _questManager.SecondSuccess(_thirdTextQuest);
            _radar.StatusRadar(true);
            _radar.target = _boxPos;
            _gm.GreenTreesNormal();
            Destroy(this);
        }
    }
}