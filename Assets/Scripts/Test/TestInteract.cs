using UnityEngine;

public class TestInteract : MonoBehaviour, IInteractable
{
    protected Prompt _prompt;

    private void Start()
    {
        _prompt = gameObject.AddComponent<Prompt>();
    }

    public virtual void PerformAction()
    {
        LogMessage();
    }

    private static void LogMessage()
    {
        Debug.Log("I am interacting");
    }

    /// <summary>
    /// Si más de un objeto del tipo Interactor entran, van a apagar el prompt
    /// Podria usar una corutina con WaiUntil() para chequear si aun hay Interactors antes de apagar el prompt
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interactor interactor))
            _prompt.TooglePrompt();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interactor interactor))
            _prompt.TooglePrompt();
    }
}
