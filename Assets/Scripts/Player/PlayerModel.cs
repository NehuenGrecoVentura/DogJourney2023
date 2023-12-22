using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerModel : MonoBehaviour
{
    public Transform Hand { get => _hand; }
    private IController _controller;
    private Interactor _interactor;
    [SerializeField]
    private Transform _hand;
    private Animation _myAnim;

    private void Awake()
    {
        _controller = new PlayerController(this);
        _interactor = gameObject.AddComponent<Interactor>();
        _myAnim = GetComponent<Animation>();
    }

    private void Update()
    {
        _controller.OnUpdate();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        Crouch();
    }

    public void Interact()
    {
        _interactor.OnInteract();
    }

    void Crouch()
    {
        _myAnim["Crouch idle"].speed = 0.05f;
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.D))
        {
            _myAnim.Play("Crouch walk");
            print("ESTOY EN SIGILO");
        }

        else if (Input.GetKey(KeyCode.LeftControl)) _myAnim.Play("Crouch idle");
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            _myAnim.Stop("Crouch walk");
            _myAnim.Stop("Crouch idle");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Final Level 1")
        {
            SceneManager.LoadScene(2);
        }
    }
}
