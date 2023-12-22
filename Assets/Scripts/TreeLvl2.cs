using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeLvl2 : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private Animation talar;
    public BoxCollider rocks;
    public Tree tree;
    public Image iconAxe;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (tree.destroyCol)
        {
            Destroy(rocks);
            Destroy(iconAxe);
        }
            
            
            
    }



    public void Fall()
    {
        Debug.Log("I'm falling");
        _anim.SetBool("IsChopped", true);
        talar.Play();
        Destroy(rocks);
        
    }
}
