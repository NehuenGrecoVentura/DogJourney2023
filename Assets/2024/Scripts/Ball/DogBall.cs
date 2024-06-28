using UnityEngine;

public class DogBall : MonoBehaviour
{
    [SerializeField] float AxisY;
    [SerializeField] Terrain _terrain;

    void Update()
    {
        // Obtén la posición actual del objeto.
        Vector3 currentPosition = transform.position;

        // Calcula la altura del terreno en esa posición.
        float terrainHeight = _terrain.SampleHeight(currentPosition);

        // Actualiza la posición del objeto para que esté sobre el terreno.
        currentPosition.y = terrainHeight + AxisY;

        // Aplica la nueva posición al objeto.
        transform.position = currentPosition;
    }

    public void SetTerrain(Terrain terrain)
    {
        _terrain = terrain;
    }
}