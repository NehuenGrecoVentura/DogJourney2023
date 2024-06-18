using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class NPCZone3 : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] Collider _myCol;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] GameObject _iconInteract;
    [SerializeField] Animator _myAnim;

    [Header("DIALOGUE")]
    [SerializeField] Dialogue _dialogue;
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] TMP_Text _txtName;
    [SerializeField] string _nameNPC;
    [SerializeField] Button _buttonConfirm;

    [Header("MESSAGE")]
    [SerializeField] BoxMessages _boxMessages;
    [SerializeField, TextArea(4,6)] string[] _messages;

    [Header("AUDIO")]
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioClip _soundConfirm;

    [Header("QUEST")]
    [SerializeField] QuestUI _questUI;
    [SerializeField] int _woodRequired = 10;
    [SerializeField] CharacterInventory _inventory;
    private bool _questActive = false;
    private bool _questCompleted = false;

    [Header("NEXT QUEST")]
    [SerializeField] Manager _gm;
    [SerializeField] GameObject _npcsHouses;
    [SerializeField] Camera _camNextQuest;
    [SerializeField] Camera _camCinematic;

    [Header("MOVE")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed = 5f;
    [SerializeField] RuntimeAnimatorController _animMove;
    [SerializeField] RuntimeAnimatorController _animIdle;

    void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.transform.DOScale(0f, 0f);
        _npcsHouses.SetActive(false);
        _camNextQuest.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_questActive)
        {
            if (_inventory.greenTrees >= _woodRequired)
            {
                _questUI.TaskCompleted(2);
                _questUI.AddNewTask(3, "Go back to the mountain");
                _questCompleted = true;
                _questActive = false;
            }

            else
            {
                _questUI.AddNewTask(2, "Get wood to carry the mountain (" + _inventory.greenTrees.ToString() + "/" + _woodRequired.ToString() + ")");
                _questCompleted = false;
                _questActive = true;
            }     
        }
    }

    private void Confirm()
    {
        _myCol.enabled = false;
        _dialogue.canTalk = false;
        _dialogue.Close();
        _iconInteract.transform.DOScale(0f, 0.5f);
        _myAudio.PlayOneShot(_soundConfirm);
        _myAnim.SetBool("Quest", true);
        _questUI.ActiveUIQuest("A Little Fire", "Travel back to the chairlift", "Get wood to carry the mountain (" + _inventory.greenTrees.ToString() + "/" + _woodRequired.ToString() + ")", string.Empty);
        _questActive = true;
    }

    private void SetDialogue()
    {
        _iconInteract.transform.DOScale(0.01f, 0.5f);
        _buttonConfirm.onClick.AddListener(() => Confirm());

        if (!_questActive)
        {
            for (int i = 0; i < _dialogue._lines.Length; i++)
                _dialogue._lines[i] = _lines[i];
        }

        _dialogue.gameObject.SetActive(true);
        _dialogue.Set(_nameNPC);
        _dialogue.playerInRange = true;
        _dialogue.canTalk = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (_myCol.enabled && !_questActive && !_questCompleted) SetDialogue();
            else if (_questCompleted) _iconInteract.transform.DOScale(0.01f, 0.5f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _questCompleted && Input.GetKeyDown(_keyInteract))
        {
            StartCoroutine(Message(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _dialogue.playerInRange = false;
            _iconInteract.transform.DOScale(0f, 0.5f);
        }
    }

    private IEnumerator Message(Character player)
    {
        _myCol.enabled = false;
        _iconInteract.transform.DOScale(0f, 0.5f);
        player.FreezePlayer();
        _boxMessages.SetMessage("SNOW NPC");

        yield return new WaitForSeconds(1f);
        _boxMessages.ShowMessage(_messages[0]);

        yield return new WaitForSeconds(3f);
        _boxMessages.CloseMessage();
        player.DeFreezePlayer();

        yield return new WaitForSeconds(1f);
        _boxMessages.DesactivateMessage();
    }

    public void MoveToBrazier(Transform brazierPos)
    {
        if (brazierPos != null)
        {
            Vector3 moveDirection = (brazierPos.position - transform.position).normalized;
            _rb.MovePosition(_rb.position + moveDirection * _speed * Time.fixedDeltaTime);
            _myAnim.runtimeAnimatorController = _animMove;
        }
    }

    public void SetIdle()
    {
        _myAnim.runtimeAnimatorController = _animIdle;
        _myAnim.SetBool("Quest", true);
    }

    private IEnumerator FinalMessage(Brazier brazier, Character player, GameObject cinematic, CameraOrbit camPlayer)
    {
        _boxMessages.SetMessage("NPC Snow");
        _boxMessages.ShowMessage(_messages[1]);
        
        yield return new WaitForSeconds(3f);
        _boxMessages.CloseMessage();
        player.DeFreezePlayer();
        player.enabled = true;
        _gm.QuestCompleted();

        yield return new WaitForSeconds(1f);
        _boxMessages.DesactivateMessage();
        _camCinematic.gameObject.SetActive(false);
        _camNextQuest.gameObject.SetActive(true);
        _npcsHouses.SetActive(true);

        yield return new WaitForSeconds(2f);
        _camNextQuest.gameObject.transform.DOMove(player.transform.position, 4f);

        yield return new WaitForSeconds(5f);
        camPlayer.gameObject.SetActive(true);
        Destroy(_camNextQuest.gameObject);
        Destroy(cinematic);
        Destroy(brazier);
        Destroy(this);
    }

    public void ActiveFinal(Brazier brazier, Character player, GameObject cinematic, CameraOrbit camPlayer)
    {
        StartCoroutine(FinalMessage(brazier, player, cinematic, camPlayer));
    }
}