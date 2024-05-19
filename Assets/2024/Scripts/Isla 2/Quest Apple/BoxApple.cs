using UnityEngine;
using DG.Tweening;

public class BoxApple : MonoBehaviour
{
    [Header("AMOUNT")]
    public int total = 10;
    public int totalInBox = 0;

    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;

    private void Start()
    {
        _iconInteract.transform.DOScale(0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (player.rabbitPicked && totalInBox < total) _iconInteract.transform.DOScale(0.25f, 0.5f);
            else _iconInteract.transform.DOScale(0, 0.5f);
        }  
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && player.rabbitPicked && Input.GetKeyDown(_keyInteract))
        {
            totalInBox++;
            player.ItemsPicked(true, false, false, false);
            player.rabbitPicked = false;
            _iconInteract.transform.DOScale(0, 0.5f);
        }

        else if (player != null && !player.rabbitPicked && Input.GetKeyDown(_keyInteract) && totalInBox >= total)
        {
            player.rabbitPicked = true;
            player.ItemsPicked(false, false, false, true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.transform.DOScale(0, 0.5f);
    }

    public void RemoveSomeApples(bool isScared)
    {
        int random = Random.Range(1, 3);
        if (!isScared) totalInBox -= random;
    }
}