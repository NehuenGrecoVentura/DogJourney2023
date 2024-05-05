using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DigBait : MonoBehaviour
{
    [SerializeField] KeyCode _keyInteractive = KeyCode.Mouse0;
    [SerializeField] float _amountHit = 200f;
    [SerializeField] RectTransform _rectLoot;
    private CharacterInventory _inventory;
    private FishingQuest2 _quest;
    private DoTweenTest _doTween;
    private AudioSource _myAudio;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();

        _inventory = FindObjectOfType<CharacterInventory>();
        _quest = FindObjectOfType<FishingQuest2>();
        _doTween = FindObjectOfType<DoTweenTest>();
    }

    private void FocusToDig(Character player)
    {
        Vector3 pos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        player.gameObject.transform.LookAt(pos);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            FocusToDig(player);
            if (Input.GetKey(_keyInteractive) && _inventory.shovelSelected)
            {
                if (!_myAudio.isPlaying) _myAudio.Play();

                player.HitDig();
                _amountHit--;

                if (_amountHit <= 0)
                {
                    _amountHit = 0;
                    _quest.baitPicked++;
                    _doTween.ShowLootCoroutine(_rectLoot);
                    Destroy(gameObject);
                }
            }
        }
    }
}