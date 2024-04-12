using UnityEngine;

public class TreeWithBall : MonoBehaviour
{
    [SerializeField] Animator[] _gatesDoors;
    [SerializeField] DogBall _dogBall;
    private TreeRegenerative _tree;
    private OrderDog _orders;

    [Header("MESSAGE")]
    [SerializeField] string _messageText;
    [SerializeField] Sprite _iconMessage;
    private MessageSlide _message;

    private void Awake()
    {
        _tree = GetComponent<TreeRegenerative>();
        _message = FindObjectOfType<MessageSlide>();
        _orders = FindObjectOfType<OrderDog>();
    }

    private void Start()
    {
        _dogBall.enabled = false;
    }

    private void Update()
    {
        if (_tree.amountHit <= 2)
        {
            foreach (var gate in _gatesDoors)
                gate.enabled = true;

            _dogBall.enabled = true;
            _message.ShowMessage(_messageText, _iconMessage);
            _orders.activeOrders = true;
            Destroy(this);
        }
    }
}