using System.Collections;
using UnityEngine;

public class WheelRot : MonoBehaviour
{
    [SerializeField] GameObject[] _wheels;

    void Start()
    {
        StartCoroutine(Rot());
    }

    IEnumerator Rot()
    {
        int currentIndex = 0;
        while (true)
        {
            for (int i = 0; i < _wheels.Length; i++)
            {
                _wheels[i].SetActive(false);
            }

            _wheels[currentIndex].SetActive(true);
            currentIndex = (currentIndex + 1) % _wheels.Length;
            yield return new WaitForSeconds(0.1f);
        }
    }
}