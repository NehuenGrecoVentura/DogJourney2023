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
    [SerializeField] Camera _camEnding;
    [SerializeField] Camera _camTree;
    [SerializeField] Transform _posEnd;

    [Header("MESSAGE")]
    [SerializeField] TMP_Text _text;
    [SerializeField, TextArea(4, 6)] string _messsageFinal;
    [SerializeField] RectTransform _message;

    [Header("NEXT QUEST")]
    [SerializeField] LocationQuest _radar;
    [SerializeField] FirstMarket _market;

    [Header("NPC")]
    [SerializeField] GameObject _npc;
    [SerializeField] RuntimeAnimatorController _animReward;
    [SerializeField] RuntimeAnimatorController _animNormal;
    [SerializeField] GameObject _broom;
    [SerializeField] GameObject _newAxe;

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
        _npc.transform.LookAt(_player.gameObject.transform);
        _text.text = _messsageFinal;
        _message.DOAnchorPosY(-1000f, 0f);
        _message.localScale = new Vector3(1, 1, 1);
        _message.gameObject.SetActive(true);
        _message.DOAnchorPosY(70f, 0.5f);

        yield return new WaitForSeconds(5f);
        _message.gameObject.SetActive(false);
        _message.DOAnchorPosY(-1000f, 0f);

        yield return new WaitForSeconds(1f);

        _fadeOut.DOColor(Color.black, 2f).OnComplete(() =>
        {
            _player.gameObject.transform.position = _posEnd.position;
            _camEnding.gameObject.SetActive(true);
            Destroy(_cinematic);
        });

        yield return new WaitForSeconds(2f);
        _newAxe.SetActive(true);
        _npc.transform.LookAt(_player.gameObject.transform);
        _npc.GetComponent<Animator>().runtimeAnimatorController = _animReward;
        _fadeOut.DOColor(Color.clear, 2f);
        yield return new WaitForSeconds(2);
        _message.gameObject.SetActive(true);
        _message.DOAnchorPosY(70f, 0.5f);
        
        
        
        yield return new WaitForSeconds(4f);
        _message.DOAnchorPosY(-1000f, 0.5f);
        _fadeOut.DOColor(Color.black, 2f).OnComplete(() =>
        {
            Destroy(_camEnding.gameObject);
            _camTree.gameObject.SetActive(true);
        });

        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.clear, 2f);
        yield return new WaitForSeconds(2f);
        
        _message.gameObject.SetActive(true);
        _message.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(5f);
        _fadeOut.DOColor(Color.black, 2f);
        yield return new WaitForSeconds(2f);
        Destroy(_camTree.gameObject);
        _fadeOut.DOColor(Color.clear, 2f);





        _message.gameObject.SetActive(false);
        _message.DOAnchorPosY(-1000f, 0f);
        
        Destroy(_newAxe);
        _camPlayer.gameObject.SetActive(true);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _gm.QuestCompleted();
        _radar.target = _market.gameObject.transform;
        _npc.GetComponent<Animator>().runtimeAnimatorController = _animNormal;
        _broom.SetActive(true);
        Destroy(gameObject, 0.6f);
    }

















}