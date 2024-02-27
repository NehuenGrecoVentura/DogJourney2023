using UnityEngine;

public class TreeWithBall : MonoBehaviour
{
    [SerializeField] Animator[] _gatesDoors;
    [SerializeField] DogBall _dogBall;
    private TreeRegenerative _tree;
    private TutorialTree _tutorialTree;
    private bool _mouseTutorial = false;
    private OrderDog _orders;

    [Header("MESSAGE")]
    [SerializeField] string _messageText;
    [SerializeField] Sprite _iconMessage;
    private MessageSlide _message;

    private void Awake()
    {
        _tree = GetComponent<TreeRegenerative>();
        _message = FindObjectOfType<MessageSlide>();
        _tutorialTree = FindObjectOfType<TutorialTree>();
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

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !_mouseTutorial)
            {
                _tutorialTree.MouseSuccess();
                _mouseTutorial = true;
            }
        }
    }
}