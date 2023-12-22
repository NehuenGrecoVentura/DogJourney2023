using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] GameObject[] _objsToDesactive;
    public float durationCinematic;
 
    void Start()
    {
        ObjStatus(false);   
    }

    public void ObjStatus(bool active)
    {
        foreach (var obj in _objsToDesactive)
            obj.SetActive(active);
    }
}