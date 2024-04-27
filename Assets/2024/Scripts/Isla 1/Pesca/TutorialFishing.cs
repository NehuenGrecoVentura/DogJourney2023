using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TutorialFishing : MonoBehaviour
{
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _message;
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] TMP_Text _textName;
    [SerializeField] SpriteRenderer _arrow;
    [SerializeField] Image[] _count;
    [SerializeField] int _totalFishes = 5;
    [SerializeField] int _reward = 5;
    private FishingMinigame _fishing;
    private CharacterInventory _inventory;
    private Manager _gm;
    private bool _questActive = true;

    [Header("CINEMATIC")]
    [SerializeField] GameObject _cinematic;
    [SerializeField] Image _fadeOut;
    private CameraOrbit _camPlayer;
    private Character _player;
    private QuestUI _questUI;

    [Header("MESSAGE FINAL")]
    [SerializeField, TextArea(4,6)] string _messageFinal;

    [Header("NEXT QUEST")]
    private LocationQuest _radar;
    private QuestFence _questFence;

    private AudioSource _myAudio;
    [SerializeField] AudioClip _introSound;
    [SerializeField] AudioClip _messageSound;

    [SerializeField] Transform _posNPC;
    [SerializeField] Transform _posPlayer;

    [SerializeField] NPCFishing _npc;
    

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _fishing = FindObjectOfType<FishingMinigame>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
        _questUI = FindObjectOfType<QuestUI>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _gm = FindObjectOfType<Manager>();
        _player = FindObjectOfType<Character>();
        _questFence = FindObjectOfType<QuestFence>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    private void Start()
    {
        StartCoroutine(PlayCinematic());
    }

    private void Update()
    {
        if (_fishing.fishedPicked == _totalFishes && _questActive)
        {
            StartCoroutine(Ending());
        }
    }

    public void CheatSkip()
    {
        _gm.QuestCompleted();
        _inventory.money += _reward;
        _radar.target = _questFence.gameObject.transform;
        _questFence.enabled = true;

        NPCFishing npc = FindObjectOfType<NPCFishing>();
        if (npc != null) Destroy(npc);
        else return;
        Destroy(this);
    }

    private IEnumerator Ending()
    {
        //_fishing.Quit();
        _questActive = false;
        _textName.text = "Alice";
        _message.text = _messageFinal;

        _player.SetFishingMode(false);
        _player.GetComponent<Animator>().enabled = false;


        _player.gameObject.transform.LookAt(transform);
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);

        _fadeOut.DOColor(Color.black, 1f).OnComplete(() => Destroy(_cinematic, 1f));
        yield return new WaitForSeconds(2f);
        _fishing.Quit();
        _fadeOut.DOColor(Color.clear, 1f);
        _npc.SetPos();
        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.clear, 1f);

        


        _camPlayer.gameObject.SetActive(true);
        _player.GetComponent<Animator>().enabled = true;

        yield return new WaitForSeconds(0.5f);
        _boxMessage.gameObject.SetActive(true);
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        _myAudio.PlayOneShot(_messageSound);
        yield return new WaitForSeconds(4f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f).OnComplete(() => _boxMessage.gameObject.SetActive(false));
        yield return new WaitForSeconds(0.6f);
        //_player.speed = _player.speedAux;
        //_player.FreezePlayer(RigidbodyConstraints.FreezeRotation);

        _gm.QuestCompleted();
        _inventory.money += _reward;
        _radar.target = _questFence.gameObject.transform;
        _questFence.enabled = true;
        Destroy(this);
    }

    private IEnumerator PlayCinematic()
    {
        _questUI.UIStatus(false);
        _textName.text = "Alice";
        _player.SetFishingMode(true);
        _player.PlayAnim("Fish");
        _player.transform.rotation = Quaternion.Euler(0, 90, 0);
        _npc.SetAnimQuest();

        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        _questActive = true;
        _fadeOut.DOColor(Color.black, 1f);
        yield return new WaitForSeconds(2f);

        _player.gameObject.transform.position = _posPlayer.transform.position;
        transform.position = _posNPC.transform.position;
        
        _myAudio.PlayOneShot(_introSound);
        _fadeOut.DOColor(Color.clear, 1f);
        
        _camPlayer.gameObject.SetActive(false);
        _cinematic.SetActive(true);
        StartCoroutine(TutorialSpace());
    }

    private IEnumerator TutorialSpace()
    {
        _fishing.Gaming = false;
        _message.text = _lines[0];
        _boxMessage.DOAnchorPosY(-1000f, 0);
        _boxMessage.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        // Tutorial SPACEBAR
        _textName.text = "Alice";
        _boxMessage.gameObject.SetActive(true);
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        _myAudio.PlayOneShot(_messageSound);
        yield return new WaitForSeconds(3f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);

        // TUTRORIAL KEEP FISH
        yield return new WaitForSeconds(1f);
        _message.text = _lines[1];
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        _myAudio.PlayOneShot(_messageSound);

        // TUTORIAL SLIDER


        yield return new WaitForSeconds(3f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        yield return new WaitForSeconds(1f);
        _message.text = _lines[2];
        _arrow.DOColor(Color.white, 0.5f);
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        _myAudio.PlayOneShot(_messageSound);
        _arrow.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        _arrow.DOColor(Color.clear, 0.5f);
        yield return new WaitForSeconds(1f);

        // CONTEO
        _count[0].DOColor(Color.white, 0.5f);
        yield return new WaitForSeconds(1f);
        _count[0].DOColor(Color.clear, 0.5f);

        _count[1].DOColor(Color.white, 0.5f);
        yield return new WaitForSeconds(1f);
        _count[1].DOColor(Color.clear, 0.5f);

        _count[2].DOColor(Color.white, 0.5f);
        yield return new WaitForSeconds(1f);
        _count[2].DOColor(Color.clear, 0.5f);

        _count[3].DOColor(Color.white, 0.5f);
        yield return new WaitForSeconds(1f);
        _count[3].DOColor(Color.clear, 0.5f);

        yield return new WaitForSeconds(1f);
        _fishing.Gaming = true;
    }
}