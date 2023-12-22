using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour, IInteractable
{
    public bool IsGrabbed { get => isGrabbed; }

    private Prompt _prompt;
    private Transform _interactorHand;
    private bool isGrabbed = false;

    private Collider _collider;

    private void Start()
    {
        _prompt = gameObject.AddComponent<Prompt>();
        _collider = GetComponent<Collider>();
    }

    public void PerformAction()
    {
        if (_interactorHand != null && !isGrabbed)
            BeGrabbed();
        else
            BeReleased();
    }

    private void BeGrabbed()
    {
        if (_interactorHand.childCount > 0) return;
        _prompt.TooglePrompt();
        transform.SetParent(_interactorHand);
        transform.position = _interactorHand.position;
        transform.rotation = _interactorHand.rotation;
        _collider.isTrigger = true;
        isGrabbed = true;
    }

    public void BeReleased()
    {
        transform.SetParent(null);
        transform.rotation = Quaternion.identity;
        _collider.isTrigger = false;
        isGrabbed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interactor interactor))
        {
            _prompt.TooglePrompt();
            _interactorHand = interactor.Hand;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interactor interactor))
        {
            _prompt.TooglePrompt();
            _interactorHand = null;
        }
    }
}
