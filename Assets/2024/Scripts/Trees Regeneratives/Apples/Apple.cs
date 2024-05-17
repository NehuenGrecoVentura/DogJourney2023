using UnityEngine;
using DG.Tweening;

public class Apple : MonoBehaviour
{
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive;
    [HideInInspector] public bool isUpgraded = false;

    void Start()
    {
        _iconInteractive.transform.DOScale(0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteractive.transform.DOScale(0.5f, 0.5f);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(_buttonInteractive))
        {
            player.rabbitPicked = true;
            player.ItemsPicked(false, false, true);
            _iconInteractive.transform.DOScale(0f, 0.5f);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteractive.transform.DOScale(0f, 0.5f);
    }
}