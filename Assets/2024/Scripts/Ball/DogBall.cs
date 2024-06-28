using UnityEngine;

public class DogBall : MonoBehaviour
{
    [SerializeField] float AxisY;
    [SerializeField] Terrain _terrain;

    void Update()
    {
        // Obt�n la posici�n actual del objeto.
        Vector3 currentPosition = transform.position;

        // Calcula la altura del terreno en esa posici�n.
        float terrainHeight = _terrain.SampleHeight(currentPosition);

        // Actualiza la posici�n del objeto para que est� sobre el terreno.
        currentPosition.y = terrainHeight + AxisY;

        // Aplica la nueva posici�n al objeto.
        transform.position = currentPosition;
    }

    public void SetTerrain(Terrain terrain)
    {
        _terrain = terrain;
    }
}