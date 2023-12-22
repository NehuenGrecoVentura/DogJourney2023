using System.Collections;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    [SerializeField] Vector3 _rot;

    void Start()
    {
        StartCoroutine(Rotate(_rot));
    }

    private IEnumerator Rotate(Vector3 dir)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            transform.Rotate(dir);
        }
        
        yield return null;
    }
}