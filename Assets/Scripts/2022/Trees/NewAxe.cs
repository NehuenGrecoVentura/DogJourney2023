using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAxe : MonoBehaviour
{
    
    [SerializeField] private KeyCode action = KeyCode.Mouse0;
    [SerializeField] private GameObject iconInteractive;
    [SerializeField] private int damage;
    [SerializeField] private int baseDamage;
    [SerializeField] private int sharpness;
    [SerializeField] private bool isSharp;
    [SerializeField] private bool isLooted;
    [SerializeField] private GameObject floorAxe;
    [SerializeField] private GameObject handAxe;
    [SerializeField] private Leñador player;
    [SerializeField] private TreesTest temp;

    private void Start()
    {
        iconInteractive.gameObject.SetActive(false);
        handAxe.SetActive(false);
    }

    private void Update()
    {
        sharpCheck();
        if (isLooted)
        {
            transform.position = player.transform.position;
        }
        if (temp != null)
        {
            if (Input.GetKeyUp(action))
            {
                temp.getHit(damage);
            }
        }
    }

    private void sharpCheck()
    {
        if (isSharp)
        {
            damage = baseDamage * 2;
        }
        if (sharpness > 0)
        {
            isSharp = true;
        }
        else
        {
            isSharp = false;
        }
        
    }

    public void GetPicked()
    {
        if (isLooted == false)
        {
            iconInteractive.gameObject.SetActive(false);
            isLooted = true;
            floorAxe.SetActive(false);
            handAxe.SetActive(true);
        }
    }

    public void GetDroped(Transform trans)
    {
        if (isLooted)
        {
            iconInteractive.gameObject.SetActive(true);
            floorAxe.SetActive(true);
            floorAxe.transform.position = trans.position;
            handAxe.SetActive(false);
            isLooted = false;
        }
       
    }

    public void ChangeInteract(bool change)
    {
        iconInteractive.SetActive(change);
    }

    private void OnTriggerStay(Collider other)
    {
        temp = other.GetComponent<TreesTest>();
    }
    

    private void OnTriggerExit(Collider other)
    {
        temp = null;
    }
}
