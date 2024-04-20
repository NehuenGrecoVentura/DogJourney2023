using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform Hand { get => _hand; }

    private IInteractable _currentInteractable;
    private Transform _hand;

    public void OnInteract()
    {
        if (_currentInteractable == null) return;
        _currentInteractable.PerformAction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && _hand.childCount < 1)
            _currentInteractable = interactable;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && interactable == _currentInteractable)
            _currentInteractable = null;

    }
}
