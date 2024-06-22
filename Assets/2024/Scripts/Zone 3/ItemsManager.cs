using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    public Transform playerTransform;
    public Slider slider;
    public float maxDetectionDistance = 10f; // Distancia m�xima de detecci�n
    public GameObject[] gameObjects; // Array de gameobjects que quieres evaluar

    private void Start()
    {
        slider.gameObject.SetActive(false);
    }

    void Update()
    {
        // Encontrar el objeto m�s cercano al jugador dentro de la distancia m�xima
        GameObject closestObject = null;
        float closestDistance = maxDetectionDistance; // Iniciar con la distancia m�xima permitida

        foreach (GameObject obj in gameObjects)
        {
            float distance = Vector3.Distance(playerTransform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        // Calcular el valor del slider basado en la distancia
        if (closestObject != null)
        {
            // Mapeo inverso: cuanto m�s cerca, m�s alto el slider
            float sliderValue = Mathf.InverseLerp(maxDetectionDistance, 0f, closestDistance); // Ajustar los valores seg�n tu escala de distancia
            slider.value = sliderValue;
        }
        else
        {
            // Si no hay ning�n objeto en el array dentro de la distancia m�xima, el slider queda en su valor m�nimo
            slider.value = 0f;
        }
    }

    public void RemoveObjectFromList(GameObject objToRemove)
    {
        gameObjects = System.Array.FindAll(gameObjects, obj => obj != objToRemove);
    }
}
