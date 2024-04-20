using UnityEngine;

public class PickRabbit : MonoBehaviour
{
    [Header("INTERACTIVE CONFIG")]
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.F;
    [SerializeField] GameObject _rabbitEscapeGO;

    [Header("POSITIONS CONFIG")]
    [SerializeField] Transform _handPos;
    [SerializeField] Transform _escapePos;

    public bool rabbitReadyToEscape = false;


    private void Start()
    {
        _iconInteractive.SetActive(false);
        _rabbitEscapeGO.SetActive(false);
    }

    private void Update()
    {
        transform.position = _handPos.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Escape Rabbit"))
        {
            if (!Input.GetKeyDown(_buttonInteractive))
                _iconInteractive.SetActive(true);

            else
            {
                _iconInteractive.SetActive(false);
                _rabbitEscapeGO.SetActive(true);
                rabbitReadyToEscape = true;
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Escape Rabbit"))
        {
            if (!Input.GetKeyDown(_buttonInteractive))
                _iconInteractive.SetActive(true);

            else
            {
                _iconInteractive.SetActive(false);
                _rabbitEscapeGO.SetActive(true);
                rabbitReadyToEscape = true;
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Escape Rabbit")) _iconInteractive.SetActive(false);
    }
}