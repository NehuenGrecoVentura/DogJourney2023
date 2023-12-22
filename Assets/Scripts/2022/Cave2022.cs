using UnityEngine;

public class Cave2022 : MonoBehaviour
{
    [Header("INTERACTIVE CONFIG")]
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.F;

    [SerializeField] GameObject _carrotGO;

    RabbitCave _rabbit;
    public bool carrotInArea = false;

    [Header("AUDIO CONFIG")]
    [SerializeField] AudioClip _soundRabbit;
    AudioSource _myAudio;

    void Awake()
    {
        _rabbit = FindObjectOfType<RabbitCave>();
        _myAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _iconInteractive.SetActive(false);
        _carrotGO.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        var carrot = other.GetComponent<CarrotRabbit>();

        if (player != null) _rabbit.Hide(); // SI DETECTA AL JUGADOR, EL CONEJO SE ESCONDE.
        if (carrot != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive)) // SI ME PONGO EN EL ÁREA DE LA CUEVA CON LA ZANAHORIA AGARRADA
                _iconInteractive.SetActive(true);

            else // SI DEJO LA ZANAHORIA EN EL AREA EL CONEJO VA A COMERLA
            {
                carrotInArea = true;
                _carrotGO.SetActive(true);
                Destroy(carrot.gameObject);
                Destroy(_iconInteractive);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        var carrot = other.GetComponent<CarrotRabbit>();

        if (player != null) _rabbit.Hide(); // SI DETECTA AL JUGADOR, EL CONEJO SE ESCONDE.
        if (carrot != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive)) // SI ME PONGO EN EL ÁREA DE LA CUEVA CON LA ZANAHORIA AGARRADA
                _iconInteractive.SetActive(true);

            else // SI DEJO LA ZANAHORIA EN EL AREA EL CONEJO VA A COMERLA
            {
                carrotInArea = true;
                _carrotGO.SetActive(true);
                Destroy(carrot.gameObject);
                Destroy(_iconInteractive);
                Destroy(gameObject);
            }
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        var carrot = other.GetComponent<CarrotRabbit>();
        if (carrot != null) _iconInteractive.SetActive(false);
        if (player != null)
        {
            _rabbit.Out(); // SI EL JUGADOR SE VA EL CONEJO VUELVE A SALIR.
            _myAudio.PlayOneShot(_soundRabbit);
        }         
    }
}