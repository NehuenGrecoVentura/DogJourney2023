using UnityEngine;
using TMPro;

public class TreeBall : MonoBehaviour
{
    [Header("PARTICLES CONFIG")]
    //[SerializeField] ParticleSystem _particleHearth;
    [SerializeField] ParticleSystem _particleCry;
    [SerializeField] GameObject _iconDogCry;
    [SerializeField] GameObject _blockBridgeWay;
    
    [Header("UI CONFIG")]
    [SerializeField] GameObject _uiQuestTrees;
    [SerializeField] GameObject _uiToDesactivate;
    [SerializeField] TMP_Text _textQuest;
    CutTree _buttonInteractive;
    BallDog _ballDog;
    DogOrder2022 _dogOrder;
    [SerializeField] Dog2022 _dog;
    ArrowQuest[] _arrowsQuests;

    TreeBall _myScript;
    [SerializeField] Animator _animBall;

    [Header("AUDIO CONFIG")]
    [SerializeField] AudioClip _soundQuest;
    AudioSource _myAudio;

    [Header("UI STATE QUEST")]
    [SerializeField] UIQuestStatus _status;

    public FirstMessage _messageControlDog;

    

    void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _buttonInteractive = GetComponent<CutTree>();
        _myScript = GetComponent<TreeBall>();
        _ballDog = FindObjectOfType<BallDog>();
        _dogOrder = FindObjectOfType<DogOrder2022>();
        _arrowsQuests = FindObjectsOfType<ArrowQuest>();

        
    }

    void Start()
    {
       

        _status.gameObject.SetActive(false);
        //_particleHearth.Stop();
        _dogOrder.enabled = false;
        _ballDog.enabled = false;
        _uiQuestTrees.SetActive(false);
        foreach (var arrow in _arrowsQuests) arrow.gameObject.SetActive(false);
        _messageControlDog.gameObject.SetActive(false);
    }

    public void UnlockedDog()
    {
        _messageControlDog.gameObject.SetActive(true);
        _myAudio.PlayOneShot(_soundQuest);
        _textQuest.gameObject.SetActive(false);
        Destroy(_uiToDesactivate);
        foreach (var arrow in _arrowsQuests) arrow.gameObject.SetActive(true);
        _uiQuestTrees.SetActive(true);
        _ballDog.enabled = true;
        Destroy(_blockBridgeWay);
        Destroy(_particleCry);
        Destroy(_iconDogCry);
        //_particleHearth.Play();
        _dog.enabled = true;
        _dog.OrderGO();
        _ballDog.ActiveAnim();
        _dogOrder.enabled = true;
        Destroy(_animBall, 2f);
        _status.gameObject.SetActive(true);
        Destroy(_myScript);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Axe" && Input.GetKeyDown(_buttonInteractive.buttonCutTree)) UnlockedDog();
    }
}