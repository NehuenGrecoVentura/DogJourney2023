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

    [Header("MESSAGE")]
    [SerializeField, TextArea(4, 6)] string _message;
    [SerializeField] BoxMessages _boxMessage;

    private void Start()
    {
        _cinematic.SetActive(false);
        _myCol.enabled = false;
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
        _cinematic.SetActive(true);
        _questApple.SpawnActive();

        _dog.GetComponent<NavMeshAgent>().enabled = false;
        _trolley.GetComponent<NavMeshAgent>().enabled = false;
        _dog.transform.parent.transform.position = _posDog.position;
        _trolley.gameObject.transform.position = _posDog.position;
        _dog.GetComponent<NavMeshAgent>().enabled = true;
        _trolley.GetComponent<NavMeshAgent>().enabled = true;

        yield return new WaitForSeconds(3f);
        _boxMessage.ShowMessage(_message);

        yield return new WaitForSeconds(5f);
        _boxMessage.CloseMessage();
        _player.DeFreezePlayer();
        _camPlayer.gameObject.SetActive(true);
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