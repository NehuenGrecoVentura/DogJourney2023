using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemZone3 : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] Transform _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] NPCHouses _quest;
    [SerializeField] SphereCollider _detector;
    [SerializeField] Collider _myCol;

    [Header("CAMS")]
    [SerializeField] Camera _myCam;
    [SerializeField] CameraOrbit _camPlayer;

    [Header("ItemsManager Reference")]
    [SerializeField] ItemsManager _itemsManager;

    void Start()
    {
        _myCam.gameObject.SetActive(false);
        _myCol.enabled = false;
        _iconInteract.DOScale(0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled) _iconInteract.DOScale(0.3f, 0.5f);


        var dog = other.GetComponent<Dog>();
        if (dog != null && _detector.enabled) StartCoroutine(Found(dog));
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(_keyInteract))
        {
            _quest.ItemFound();
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.DOScale(0f, 0.5f);
    }


    private IEnumerator Found(Dog dog)
    {
        dog.transform.LookAt(transform);
        dog.Stop();
        Destroy(_detector);

        _camPlayer.gameObject.SetActive(false);
        _myCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        _camPlayer.gameObject.SetActive(true);
        Destroy(_myCam.gameObject);
        _myCol.enabled = true;
        NotifyItemsManager();
    }

    private void NotifyItemsManager()
    {
        if (_itemsManager != null)
        {
            _itemsManager.RemoveObjectFromList(gameObject);
        }
    }
}