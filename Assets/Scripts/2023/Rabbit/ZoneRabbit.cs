using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneRabbit : MonoBehaviour
{
    [SerializeField] RabbitEntity _rabbit;

    [SerializeField] GameObject _carrotInPlace;

    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.F;

    [HideInInspector] public bool _carrotInArea = false;
    private Collider _col;


    private void Awake()
    {
        _col = GetComponent<Collider>();
    }

    private void Start()
    {
        _iconInteractive.SetActive(false);
        _carrotInPlace.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        var carrot = other.gameObject.GetComponent<CarrotRabbit>();

        if (player != null) _rabbit.Hide(); // SI DETECTA AL JUGADOR, EL CONEJO SE ESCONDE.
        if (carrot != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive))
                _iconInteractive.SetActive(true);

            else // SI DEJO LA ZANAHORIA EN EL AREA EL CONEJO VA A COMERLA
            {
                _carrotInArea = true;
                //StartCoroutine(_rabbit.GoToCarrot());
                _rabbit.StartCoroutine(_rabbit.GoToCarrot());
                _carrotInPlace.SetActive(true);
                Destroy(_iconInteractive);
                Destroy(carrot.gameObject);
                Destroy(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            _iconInteractive.SetActive(false);
            _rabbit.Out();
        }
            
            
    }
}