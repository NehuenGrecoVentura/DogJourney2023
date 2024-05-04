using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dig : MonoBehaviour
{
    [SerializeField] float _amountHit = 500f;
    [SerializeField] KeyCode _inputInteractive = KeyCode.Mouse0;
    //[SerializeField] RectTransform _boxMessage;
    //[SerializeField] TMP_Text _textAmount;

    private DoTweenManager _message;
    private CharacterInventory _invetory;
    private AudioSource _myAudio;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _message = FindObjectOfType<DoTweenManager>();
        _invetory = FindObjectOfType<CharacterInventory>();
    }

    private void Start()
    {
        //_boxMessage.gameObject.SetActive(false);
        _myAudio.Stop();
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKey(_inputInteractive) && _invetory.shovelSelected)
        {
            if (!_myAudio.isPlaying) _myAudio.Play();

            Vector3 pos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            player.gameObject.transform.LookAt(pos);
            player.HitTree();
            _amountHit--;

            if (_amountHit <= 0)
            {
                _amountHit = 0;
                //_message.ShowUI("+1", _boxMessage, _textAmount);
                int randomIndex = Random.Range(0, 2);

                switch (randomIndex)
                {
                    case 0:
                        // Incrementa la variable seeds
                        _invetory.seeds++;
                        break;

                    case 1:
                        // Incrementa la variable flowers
                        _invetory.flowers++;
                        break;

                    default:
                        Debug.LogError("Index fuera de rango.");
                        break;
                }

                Destroy(gameObject);
            }
        }
    }
}
