using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SeedQuest : MonoBehaviour
{
    [SerializeField] GameObject _cinematic;
    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] Character _player;
    [SerializeField] Collider _myCol;
    [SerializeField] MeshRenderer _myMesh;
    [SerializeField] Image _fadeOut;
    [SerializeField] Manager _gm;

    [Header("MESSAGE")]
    [SerializeField] TMP_Text _text;
    [SerializeField, TextArea(4, 6)] string _messsageFinal;
    [SerializeField] RectTransform _message;

    [Header("NEXT QUEST")]
    [SerializeField] LocationQuest _radar;
    [SerializeField] FirstMarket _market;

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(BuildSeed());
        }
    }

    private IEnumerator BuildSeed()
    {
        Destroy(_myCol);
        _myMesh.enabled = false;

        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);
        _player.isConstruct = true;

        _camPlayer.gameObject.SetActive(false);
        _cinematic.SetActive(true);
        _fadeOut.DOColor(Color.black, 2f);

        float elapsedTime = 0f;

        while (elapsedTime < 3f)
        {
            if (elapsedTime > 0f) _player.PlayAnim("Build");

            // Incrementa el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Espera un corto tiempo antes de la próxima iteración
            yield return null;
        }


        _fadeOut.DOColor(Color.clear, 2f);
        yield return new WaitForSeconds(2);
        _player.isConstruct = false;

        _text.text = _messsageFinal;
        _message.DOAnchorPosY(-1000f, 0f);
        _message.localScale = new Vector3(1, 1, 1);
        _message.gameObject.SetActive(true);
        _message.DOAnchorPosY(70f, 0.5f);

        yield return new WaitForSeconds(5f);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _camPlayer.gameObject.SetActive(true);

        _message.gameObject.SetActive(false);
        _message.DOAnchorPosY(-1000f, 0f);

        Destroy(_cinematic);
        _gm.QuestCompleted();
        _radar.target = _market.gameObject.transform;
        Destroy(this);
    }
}
