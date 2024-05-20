using UnityEngine;

public class TreeWithBall : MonoBehaviour
{
    [SerializeField] Animator[] _gatesDoors;
    [SerializeField] DogBall _dogBall;
    private TreeRegenerative _tree;
    private OrderDog _orders;
    [SerializeField] CinematicTrunks _cinemTrunks;

    [Header("MESSAGE")]
    [SerializeField] string _messageText;
    [SerializeField] Sprite _iconMessage;

    private void Awake()
    {
        _tree = GetComponent<TreeRegenerative>();
        _orders = FindObjectOfType<OrderDog>();
    }

    private void Start()
    {
        _dogBall.enabled = false;
        _cinemTrunks.enabled = false;
        _cinemTrunks.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_tree.amountHit <= 10)
        {
            foreach (var gate in _gatesDoors)
                gate.enabled = true;

            _dogBall.enabled = true;
            _orders.activeOrders = true;
            _cinemTrunks.gameObject.SetActive(true);
            _cinemTrunks.enabled = true;
            Destroy(this);
        }
    }
}