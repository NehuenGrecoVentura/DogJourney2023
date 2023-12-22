using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionsLvl2 : MonoBehaviour
{
    private float _jumpForce;
    private Rigidbody _rb;
    public DogsMove dog1;
    public DogsMove dog2;
    public GeiserController checkButton;
    public Transform _dog1;
    public Transform _dog2;
    public GameObject textDogs;
    [HideInInspector] public bool upFinal = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        textDogs.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ending" && (dog1.transform.position.z > 213.52 && dog2.transform.position.z > 213.52)) SceneManager.LoadScene(3);
        if (other.gameObject.name == "Ending" && (dog1.transform.position.z < 213.52 && dog2.transform.position.z < 213.52)) textDogs.gameObject.SetActive(true);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 13 && other.gameObject.name=="Sensor Geiser 1")
        {
            _jumpForce = 200000f;
            _rb.AddForce(transform.up * _jumpForce * Time.deltaTime);
        }

        if (other.gameObject.layer == 13 && other.gameObject.name == "Sensor Geiser 2")
        {
            _jumpForce = 200000f;
            _rb.AddForce(transform.up * _jumpForce * Time.deltaTime);
        }

        if (other.gameObject.layer == 13 && other.gameObject.name == "Sensor Geiser 3")
        {
            _jumpForce = 200000f;
            _rb.AddForce(transform.up * _jumpForce * Time.deltaTime);
        }

        if (other.gameObject.layer == 13 && other.gameObject.name == "Sensor Geiser 4")
        {
            _jumpForce = 200000f;
            _rb.AddForce(transform.up * _jumpForce * Time.deltaTime);
        }

        if (other.gameObject.layer == 13 && other.gameObject.name == "Sensor Geiser 4" && checkButton.allActive) // SI EL TERCER BOTON ESTÁ ACTIVO Y PASO POR EL TERCER GEISER, LOS PERROS YA ESTÁN ARRIBA
        {
            _jumpForce = 200000f;
            _rb.AddForce(transform.up * _jumpForce * Time.deltaTime);
            upFinal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        textDogs.gameObject.SetActive(false);
    }
}
