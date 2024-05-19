using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
    private bool _canScared = false;

    [Header("MESSAGE")]
    [SerializeField, TextArea(4, 6)] string[] _message;
    [SerializeField] BoxMessages _boxMessage;

    OrderDog _order;
    private void Awake()
    {
        _order = FindObjectOfType<OrderDog>();
    }

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
        //if (Input.GetKeyDown(KeyCode.Space) && _canScared)
        //{
        //    if(_ladrones[0].gameObject.activeSelf || _ladrones[1].gameObject.activeSelf || _ladrones[2].gameObject.activeSelf || _ladrones[3].gameObject.activeSelf || _ladrones[4].gameObject.activeSelf || _ladrones[5].gameObject.activeSelf)
        //            StartCoroutine(PlayCinematicScared());
        //}     

        if (Input.GetKeyDown(KeyCode.Space) && _canScared && CheckThieft())
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
        Destroy(_myCol);
        _player.FreezePlayer();
        _camPlayer.gameObject.SetActive(false);
        _dogCam.gameObject.SetActive(false);
        _dogThieft.gameObject.SetActive(true);
        _cinematic.SetActive(true);
        _questApple.SpawnActive();

        _dog.Stop();
        _dog.GetComponent<NavMeshAgent>().enabled = false;
        _trolley.GetComponent<NavMeshAgent>().enabled = false;
        _dog.transform.parent.transform.position = _posDog.position;
        _trolley.gameObject.transform.position = _posDog.position;
        _dog.GetComponent<NavMeshAgent>().enabled = true;
        _trolley.GetComponent<NavMeshAgent>().enabled = true;
        _dog.transform.LookAt(_thieft.transform);

        yield return new WaitForSeconds(3f);
        _boxMessage.ShowMessage(_message[0]);

        yield return new WaitForSeconds(5f);
        _boxMessage.CloseMessage();

        yield return new WaitForSeconds(1f);
        _boxMessage.ShowMessage(_message[1]);
        Destroy(_dogThieft.gameObject);
        _dogCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);
        _canScared = true;
        _player.DeFreezePlayer();
        Destroy(_dogCam.gameObject);
        _camPlayer.gameObject.SetActive(true);
        _cinematic.SetActive(false);

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

        yield return new WaitForSeconds(3f);
        _boxMessage.ShowMessage(_message[2]);

        yield return new WaitForSeconds(3f);
        _boxMessage.CloseMessage();
        _player.DeFreezePlayer();
        _camPlayer.gameObject.SetActive(true);
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