using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class CinematicThieft : MonoBehaviour
{
    [SerializeField] GameObject _cinematic;
    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] Character _player;
    [SerializeField] Collider _myCol;
    [SerializeField] QuestApple _questApple;
    [SerializeField] Dog _dog;
    [SerializeField] Transform _posDog;
    [SerializeField] TrolleyWood _trolley;
    [SerializeField] Camera _dogCam;
    [SerializeField] Camera _dogThieft;
    [SerializeField] Camera _scaredCam;
    [SerializeField] ThiefApple _thieft;
    [SerializeField] ThiefApple[] _ladrones;
    [SerializeField] OrderDog _order;
    [SerializeField] GameObject _targetDog;
    [SerializeField] QuestUI _questUI;
    private bool _canScared = false;

    [Header("MESSAGE")]
    [SerializeField, TextArea(4, 6)] string[] _message;
    [SerializeField] BoxMessages _boxMessage;
    [SerializeField] AudioSource _myAudio;

    private void Start()
    {
        _cinematic.SetActive(false);
        _myCol.enabled = false;
        _dogCam.gameObject.SetActive(false);
        _dogThieft.gameObject.SetActive(false);
        _scaredCam.gameObject.SetActive(false);
    }

    private void Update()
    {
        bool thiefActive = _ladrones.Any(elemento => elemento.isThief);
        if (Input.GetKeyDown(KeyCode.Space) && _canScared && CheckThieft() && thiefActive)
            StartCoroutine(PlayCinematicScared());        
    }

    bool CheckThieft()
    {
        foreach (ThiefApple ladron in _ladrones)
        {
            if (ladron.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) StartCoroutine(PlayCinematic());
    }

    private IEnumerator PlayCinematic()
    {
        _boxMessage.SetMessage("Tip");
        _player.FreezePlayer();
        Destroy(_myCol);

        _questUI.UIStatus(false);
        _camPlayer.gameObject.SetActive(false);
        _dogCam.gameObject.SetActive(false);
        _dogThieft.gameObject.SetActive(true);
        _cinematic.SetActive(true);
        _questApple.SpawnActive();

        NavMeshAgent agentDog = _dog.GetComponent<NavMeshAgent>();
        NavMeshAgent agentTrolley = _trolley.GetComponent<NavMeshAgent>();

        _targetDog.transform.position = _posDog.position;
        agentDog.enabled = false;
        agentTrolley.enabled = false;
        _dog.canTeletransport = false;
        //_dog.transform.parent.transform.position = _posDog.position;
        _dog.transform.position = _posDog.position;
        _trolley.transform.position = _dog.transform.position;
        //_dog.transform.parent.LookAt(_thieft.transform);
        _dog.transform.LookAt(_thieft.transform);

        yield return new WaitForSeconds(3f);
        _boxMessage.ShowMessage(_message[0]);
        _myAudio.Play();

        yield return new WaitForSeconds(5f);
        _boxMessage.CloseMessage();

        yield return new WaitForSeconds(1f);
        _boxMessage.ShowMessage(_message[1]);
        _myAudio.Play();
        Destroy(_dogThieft.gameObject);
        _dogCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);
        _canScared = true;


        foreach (var item in _ladrones)
        {
            item.canScared = true;
        }

        _player.DeFreezePlayer();
        Destroy(_dogCam.gameObject);
        _camPlayer.gameObject.SetActive(true);
        _cinematic.SetActive(false);
        _questUI.UIStatus(true);

        yield return new WaitForSeconds(1f);
        _boxMessage.DesactivateMessage();
    }

    private IEnumerator PlayCinematicScared()
    {
        _canScared = false;
        _boxMessage.SetMessage("Tip");
        _cinematic.SetActive(true);
        _camPlayer.gameObject.SetActive(false);
        _scaredCam.gameObject.SetActive(true);
        _player.FreezePlayer();
        _questUI.UIStatus(false);

        yield return new WaitForSeconds(3f);
        _boxMessage.ShowMessage(_message[2]);
        _myAudio.Play();

        yield return new WaitForSeconds(3f);
        _boxMessage.CloseMessage();
        _player.DeFreezePlayer();
        _camPlayer.gameObject.SetActive(true);
        _questUI.UIStatus(true);
        Destroy(_scaredCam.gameObject);
        Destroy(_cinematic);

        yield return new WaitForSeconds(1f);
        _boxMessage.DesactivateMessage();
        Destroy(gameObject);
    }

    public void Activate()
    {
        _myCol.enabled = true;
    }
}