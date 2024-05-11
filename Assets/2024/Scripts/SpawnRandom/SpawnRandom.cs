using UnityEngine;
using System.Collections.Generic;

public class SpawnRandom : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Terrain _terrain;
    [SerializeField] GameObject containerPoints;
    [SerializeField] private Transform[] spawnPoints;
    private List<int> usedSpawnPointIndices = new List<int>();

    void Start()
    {
        // Obtener todos los hijos del contenedor
        int childCount = containerPoints.transform.childCount;
        spawnPoints = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            spawnPoints[i] = containerPoints.transform.GetChild(i);
        }

        SetPosTerrain();
    }

    public void SpawnObject(Transform myTransform)
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No hay puntos de aparici�n disponibles.");
            return;
        }

        // Encontrar los �ndices de los puntos de aparici�n no utilizados
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!usedSpawnPointIndices.Contains(i))
            {
                availableIndices.Add(i);
            }
        }

        // Verificar si quedan puntos de aparici�n disponibles
        if (availableIndices.Count == 0)
        {
            Debug.LogWarning("�Todos los puntos de aparici�n han sido utilizados!");
            return;
        }

        // Elegir un punto de aparici�n aleatoriamente de los puntos disponibles
        int randomIndex = Random.Range(0, availableIndices.Count);
        int chosenIndex = availableIndices[randomIndex];
        Transform spawnPoint = spawnPoints[chosenIndex];

        // Spawnear el objeto en el punto seleccionado
        myTransform.position = spawnPoint.position;

        // Agregar el �ndice del punto de aparici�n utilizado a la lista de utilizados
        usedSpawnPointIndices.Add(chosenIndex);
    }

    private void SetPosTerrain()
    {
        foreach (Transform t in spawnPoints)
        {
            Vector3 position = t.position;
            // Obtener la altura del terreno en la posici�n del objeto
            float terrainHeight = _terrain.SampleHeight(position);

            // Ajustar la posici�n vertical del objeto
            position.y = terrainHeight + 0.2f;

            // Asignar la nueva posici�n al objeto
            t.position = position;
        }
    }
}