using UnityEngine;
using UnityEngine.UI;

public class Axe2022 : MonoBehaviour
{
    public KeyCode buttonInteractive = KeyCode.F;
    public KeyCode buttonDrop = KeyCode.G;
    public GameObject arrowAxe;
    public GameObject iconInteractive;
    public Text textAxe;
    public Text textForest;
    public Transform hand;
    bool _isLoot = false;
    public GameObject checkpoint;

    void Start()
    {
        arrowAxe.gameObject.SetActive(true);
        iconInteractive.gameObject.SetActive(false);
        textForest.gameObject.SetActive(false);
    }

    void Update()
    {
        LootAxe();
        DropAxe();    
    }

    public void LootAxe()
    {
        if (_isLoot)
        {
            arrowAxe.gameObject.SetActive(false);
            transform.SetParent(hand);
            transform.position = hand.position;
            transform.rotation = hand.rotation;
        }
    }

    public void DropAxe()
    {
        if (_isLoot && Input.GetKeyDown(buttonDrop))
        {
            arrowAxe.gameObject.SetActive(true);
            transform.parent = null;
            hand.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            _isLoot = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !_isLoot && !Input.GetKeyDown(buttonInteractive)) iconInteractive.gameObject.SetActive(true);
        else if (other.gameObject.tag == "Player" && !_isLoot && Input.GetKeyDown(buttonInteractive))
        {
            checkpoint.SetActive(true);
            iconInteractive.gameObject.SetActive(false);
            textAxe.gameObject.SetActive(false);
            textForest.gameObject.SetActive(true);
            _isLoot = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !_isLoot && !Input.GetKeyDown(buttonInteractive)) iconInteractive.gameObject.SetActive(true);
        else if (other.gameObject.tag == "Player" && !_isLoot && Input.GetKeyDown(buttonInteractive))
        {
            checkpoint.SetActive(true);
            iconInteractive.gameObject.SetActive(false);
            textAxe.gameObject.SetActive(false);
            textForest.gameObject.SetActive(true);
            _isLoot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !_isLoot) iconInteractive.gameObject.SetActive(false);
    }
}