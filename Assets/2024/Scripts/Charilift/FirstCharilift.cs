using UnityEngine;

public class FirstCharilift : MonoBehaviour
{
    [SerializeField] Collider _myCol;
    [SerializeField] LocationQuest _radar;
    [SerializeField] NPCZone3 _target;
    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled && Input.GetKeyDown(KeyCode.F))
        {
            _radar.StatusRadar(true);
            _radar.target = _target.transform;
            Destroy(this);
        }
    }
}
