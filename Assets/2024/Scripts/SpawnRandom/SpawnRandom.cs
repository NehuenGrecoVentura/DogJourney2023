using UnityEngine;
using System.Collections.Generic;

public class SpawnRandom : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) SpawnObject();
    }

    void SpawnObject()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No hay puntos de aparición disponibles.");
            return;
        }

        // Encontrar los índices de los puntos de aparición no utilizados
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!usedSpawnPointIndices.Contains(i))
            {
                availableIndices.Add(i);
            }
        }

        // Verificar si quedan puntos de aparición disponibles
        if (availableIndices.Count == 0)
        {
            Debug.LogWarning("¡Todos los puntos de aparición han sido utilizados!");
            return;
        }

        // Elegir un punto de aparición aleatoriamente de los puntos disponibles
        int randomIndex = Random.Range(0, availableIndices.Count);
        int chosenIndex = availableIndices[randomIndex];
        Transform spawnPoint = spawnPoints[chosenIndex];

        // Spawnear el objeto en el punto seleccionado
        Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);

        // Agregar el índice del punto de aparición utilizado a la lista de utilizados
        usedSpawnPointIndices.Add(chosenIndex);
    }
}