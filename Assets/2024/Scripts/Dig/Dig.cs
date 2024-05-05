using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dig : MonoBehaviour
{
    [SerializeField] float _amountHit = 500f;
    [SerializeField] KeyCode _inputInteractive = KeyCode.Mouse0;
    private AudioSource _myAudio;

    [Header("INVENTORY UI")]
    [SerializeField] RectTransform _boxSlide;
    [SerializeField] TMP_Text _textAmount;
    private DoTweenManager _message;
    private CharacterInventory _invetory;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _message = FindObjectOfType<DoTweenManager>();
        _invetory = FindObjectOfType<CharacterInventory>();
    }

    private void FocusToFlower(Character player)
    {
        Vector3 pos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        player.gameObject.transform.LookAt(pos);
    }


    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKey(_inputInteractive) && _invetory.shovelSelected)
        {
            if (!_myAudio.isPlaying) _myAudio.Play();
            FocusToFlower(player);
            player.HitDig();
            _amountHit--;

            if (_amountHit <= 0)
            {
                _amountHit = 0;
                //_message.ShowUI("+1", _boxMessage, _textAmount);
                //int randomIndex = Random.Range(0, 2);

                //switch (randomIndex)
                //{
                //    case 0:
                //        // Incrementa la variable seeds
                //        _invetory.seeds++;
                //        break;

                //    case 1:
                //        // Incrementa la variable flowers
                //        _invetory.flowers++;
                //        break;

                //    default:
                //        Debug.LogError("Index fuera de rango.");
                //        break;
                //}

                _invetory.flowers++;
                _message.ShowUI("+1", _boxSlide, _textAmount);
                Destroy(gameObject);
            }
        }
    }
}
