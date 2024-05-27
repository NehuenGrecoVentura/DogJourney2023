using UnityEngine;
using System.Collections;

public class MessageStealth : MonoBehaviour
{
    [Header("CINEMATIC")]
    [SerializeField] GameObject _cinematic;
    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] Collider _myCol;
    [SerializeField] BoxMessages _boxMessage;
    [SerializeField, TextArea(4, 6)] string[] _messages;
    [SerializeField] Character _player;

    private void Start()
    {
        _cinematic.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled) StartCoroutine(Display(player));
    }

    private IEnumerator Display(Character player)
    {

        //Destroy(_myCol);
        _myCol.enabled = false;

        _boxMessage.SetMessage("Stealth");
        player.FreezePlayer();
        _camPlayer.gameObject.SetActive(false);
        _cinematic.SetActive(true);

        yield return new WaitForSeconds(1f);
        _boxMessage.ShowMessage(_messages[0]);

        yield return new WaitForSeconds(3f);
        _boxMessage.CloseMessage();

        yield return new WaitForSeconds(0.5f);
        _boxMessage.ShowMessage(_messages[1]);

        yield return new WaitForSeconds(3f);
        Destroy(_cinematic);
        _camPlayer.gameObject.SetActive(true);
        player.DeFreezePlayer();
        _boxMessage.CloseMessage();

        yield return new WaitForSeconds(0.5f);
        _boxMessage.DesactivateMessage();
        Destroy(_myCol);
        Destroy(this);
    }
}