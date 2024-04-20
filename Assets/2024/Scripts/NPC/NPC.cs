using UnityEngine;

public class NPC : MonoBehaviour
{
    private Animator _myAnim;

    private void Awake()
    {
        _myAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (gameObject.tag == "Sit")
            {
                _myAnim.SetBool("Sit", false);
                _myAnim.SetBool("Sit Interact", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (gameObject.tag == "Sit")
            {
                _myAnim.SetBool("Sit", true);
                _myAnim.SetBool("Sit Interact", false);
            }
        }
    }
}
