using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RepairFence : MonoBehaviour
{
    [SerializeField] GameObject _iconMaterial;
    [SerializeField] GameObject _cinematic;
    [SerializeField] Image _fadeOut;
    private CameraOrbit _camPlayer;
    private Character _player;
    private Collider _myCol;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
        _player = FindObjectOfType<Character>();
    }

    private void Start()
    {
        _iconMaterial.transform.DOScale(0f, 0f);
        _cinematic.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconMaterial.transform.DOScale(1.5f, 0.5f);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(KeyCode.Space)) 
            StartCoroutine(Repair());
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconMaterial.transform.DOScale(0f, 0.5f);
    }

    private IEnumerator Repair()
    {
        Destroy(_myCol);
        Destroy(_iconMaterial.gameObject);

        _player.isConstruct = true;
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);

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


        
        //yield return new WaitForSeconds(4f);
        _fadeOut.DOColor(Color.clear, 2f);
        yield return new WaitForSeconds(2);
        _player.isConstruct = false;
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _camPlayer.gameObject.SetActive(true);
        Destroy(_cinematic);
        Destroy(this);
    }
}
