using System.Collections;
using UnityEngine;
using DG.Tweening;

public class HouseBuild : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] KeyCode _keyBuild = KeyCode.Space;
    [SerializeField] Collider _myCol;
    [SerializeField] MeshRenderer _myMesh;
    [SerializeField] NPCHouses _npcQuest;
    
    [Header("AMOUNT")]
    [SerializeField] int _woodRequired = 10;
    [SerializeField] int _nailRequired = 10;
    [SerializeField] CharacterInventory _inventory;

    [Header("AUDIO")]
    [SerializeField] AudioSource _myAudio;

    [Header("NOTIFICATION")]
    [SerializeField] DoTweenTest _doTween;
    [SerializeField] RectTransform _slideNotif;

    [SerializeField] Transform[] _iconsMaterials;
    private Transform _parentIconsMaterials;
    [SerializeField] GameObject _houseToBuild;

    [Header("CAMERAS")]
    [SerializeField] CameraOrbit _camPlayer;
    [SerializeField] Camera _camHouse;

    [Header("MESSAGE")]
    [SerializeField] BoxMessages _boxMessage;
    [SerializeField, TextArea(4,6)] string _message;
    [SerializeField] string _nameNPC;

    void Start()
    {
        foreach (Transform item in _iconsMaterials)
        {
            item.DOScale(0f, 0f);
        }

        _parentIconsMaterials = _iconsMaterials[0];
        _houseToBuild.SetActive(false);
        _camHouse.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            foreach (Transform item in _iconsMaterials)
            {
                item.DOScale(1.5f, 0.5f);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKey(KeyCode.Space))
        {
            if (_inventory.nails >= _nailRequired && _inventory.greenTrees >= _woodRequired && _npcQuest.itemsFound > 0 && !_inventory.shovelSelected)
            {
                player.Build();

                foreach (Transform item in _iconsMaterials)
                {
                    item.DOScale(0f, 0.5f);
                }

                StartCoroutine(Build(player));
            }

            else
            {
                if (_inventory.nails < _nailRequired) StartCoroutine(Feedback(0));
                if (_inventory.greenTrees < _woodRequired) StartCoroutine(Feedback(1));
                if (_npcQuest.itemsFound <= 0) _doTween.ShowLootCoroutine(_slideNotif);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            foreach (Transform item in _iconsMaterials)
            {
                item.DOScale(0f, 0.5f);
            }
        }
    }

    private IEnumerator Feedback(int index)
    {
        _iconsMaterials[index].GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1f);
        _iconsMaterials[index].GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator Build(Character player)
    {
        _myAudio.Play();
        _myMesh.enabled = false;
        _myCol.enabled = false;
        Destroy(_parentIconsMaterials.gameObject);

        player.Build();

        float elapsedTime = 0f;

        while (elapsedTime < 3f)
        {
            if (elapsedTime > 0f) player.Build();

            // Incrementa el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Espera un corto tiempo antes de la próxima iteración
            yield return null;
        }

        _boxMessage.SetMessage(_nameNPC);
        _boxMessage.ShowMessage(_message);
        _camPlayer.gameObject.SetActive(false);
        _camHouse.gameObject.SetActive(true);

        yield return new WaitForSeconds(4f);
        _boxMessage.CloseMessage();
        _boxMessage.DesactivateMessage();
        _camPlayer.gameObject.SetActive(true);
        Destroy(_camHouse.gameObject);

        player.isConstruct = false;
        player.DeFreezePlayer();
        _houseToBuild.SetActive(true);

        _inventory.greenTrees -= _woodRequired;
        _inventory.nails -= _nailRequired;
        if (_inventory.greenTrees <= 0) _inventory.greenTrees = 0;
        if (_inventory.nails <= 0) _inventory.nails = 0;

        _npcQuest.HouseBuilded();
        Destroy(gameObject);
    }
}
