using System.Collections;
using UnityEngine;

public class ItemZone3 : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] SphereCollider _detector;
    [SerializeField] Collider _myCol;
    [SerializeField] DigZone3 _myDig;

    [Header("CAMS")]
    [SerializeField] Camera _myCam;
    [SerializeField] CameraOrbit _camPlayer;

    [Header("ItemsManager Reference")]
    [SerializeField] ItemsManager _itemsManager;

    void Start()
    {
        _myDig.gameObject.SetActive(false);
        _myCam.gameObject.SetActive(false);
        _myCol.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var dog = other.GetComponent<Dog>();
        if (dog != null && _detector.enabled) StartCoroutine(Found(dog));
    }

    private IEnumerator Found(Dog dog)
    {
        dog.transform.LookAt(transform);
        dog.Stop();
        Destroy(_detector);
        _myDig.gameObject.SetActive(true);

        _camPlayer.gameObject.SetActive(false);
        _myCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        _camPlayer.gameObject.SetActive(true);
        Destroy(_myCam.gameObject);
        NotifyItemsManager();
        Destroy(gameObject);
    }

    private void NotifyItemsManager()
    {
        if (_itemsManager != null)
        {
            _itemsManager.RemoveObjectFromList(gameObject);
        }
    }
}