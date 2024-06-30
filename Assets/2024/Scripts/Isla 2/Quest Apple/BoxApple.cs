using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class BoxApple : MonoBehaviour
{
    [Header("AMOUNT")]
    public int total = 10;
    public int totalInBox = 0;
    [SerializeField] GameObject _amount1;
    [SerializeField] GameObject _amount3;
    [SerializeField] GameObject _amount5;
    [SerializeField] GameObject _amount8;
    [SerializeField] GameObject _amount10;

    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] OrderDog _order;

    [Header("NAVMESH")]
    [SerializeField] NavMeshAgent _dog;
    [SerializeField] NavMeshAgent _trolley;


    private void Start()
    {
        _iconInteract.transform.DOScale(0, 0);

        _amount1.gameObject.SetActive(false);
        _amount3.gameObject.SetActive(false);
        _amount5.gameObject.SetActive(false);
        _amount8.gameObject.SetActive(false);
        _amount10.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(totalInBox <= 0)
        {
            _amount1.gameObject.SetActive(false);
            _amount3.gameObject.SetActive(false);
            _amount5.gameObject.SetActive(false);
            _amount8.gameObject.SetActive(false);
            _amount10.gameObject.SetActive(false);
        }

        else if (totalInBox >= 2 && totalInBox < 3)
        {
            _amount1.gameObject.SetActive(true);
            _amount3.gameObject.SetActive(false);
            _amount5.gameObject.SetActive(false);
            _amount8.gameObject.SetActive(false);
            _amount10.gameObject.SetActive(false);
        }

        else if (totalInBox >= 3 && totalInBox < 5)
        {
            _amount1.gameObject.SetActive(true);
            _amount3.gameObject.SetActive(true);
            _amount5.gameObject.SetActive(false);
            _amount8.gameObject.SetActive(false);
            _amount10.gameObject.SetActive(false);
        }

        else if (totalInBox >= 5 && totalInBox < 8)
        {
            _amount1.gameObject.SetActive(true);
            _amount3.gameObject.SetActive(true);
            _amount5.gameObject.SetActive(true);
            _amount8.gameObject.SetActive(false);
            _amount10.gameObject.SetActive(false);
        }

        else if (totalInBox >= 8 && totalInBox < total)
        {
            _amount1.gameObject.SetActive(true);
            _amount3.gameObject.SetActive(true);
            _amount5.gameObject.SetActive(true);
            _amount8.gameObject.SetActive(true);
            _amount10.gameObject.SetActive(false);
        }

        else if (totalInBox >= total)
        {
            _amount1.gameObject.SetActive(true);
            _amount3.gameObject.SetActive(true);
            _amount5.gameObject.SetActive(true);
            _amount8.gameObject.SetActive(true);
            _amount10.gameObject.SetActive(true);
            _dog.enabled = true;
            _trolley.enabled = true;
            _order.activeOrders = true;
        }
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
        if (player != null && player.rabbitPicked && Input.GetKeyDown(_keyInteract) && totalInBox < total)
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
            Destroy(gameObject, 0.1f);
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