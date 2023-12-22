using UnityEngine;
using System.Collections;

public class TestInteractBis : TestInteract
{
    private GameObject _particle;
    private static float OFFSET = 2f;

    private void Start()
    {
        _prompt = gameObject.AddComponent<Prompt>();
        _particle = Resources.Load("FX/InteractTest") as GameObject;        
    }

    public override void PerformAction()
    {
        ShowParticle();
    }

    private void ShowParticle()
    {
        GameObject currentParticle = Instantiate(_particle, transform.position + Vector3.up * OFFSET, Quaternion.identity);
        Destroy(currentParticle, 2f);
    }
}
