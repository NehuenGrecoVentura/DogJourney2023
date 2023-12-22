using System;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private Tree _tree;
    private Grabbable _grabbable;
    public GameObject iconAxe;
    bool _isChop = false;
    bool _chopTree1 = false;
    bool _chopTree2 = false;

    private void Awake()
    {
        _grabbable = gameObject.AddComponent<Grabbable>();
    }

    private void Start()
    {
        iconAxe.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _grabbable.IsGrabbed)
            Chop();

        if (_isChop) iconAxe.gameObject.SetActive(false);
        if (_chopTree1 && _chopTree2) iconAxe.gameObject.SetActive(false);


    }

    private void Chop()
    {
        if (_tree == null) return;

        _tree.Fall();
        _isChop = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Tree tree))
        {
            _tree = tree;
            if (!_isChop) iconAxe.gameObject.SetActive(true);
            else iconAxe.gameObject.SetActive(false);




            if (_tree.gameObject.name == "Tree 1" && !_chopTree1) iconAxe.gameObject.SetActive(true);
            if (_tree.gameObject.name == "Tree 2" && !_chopTree2) iconAxe.gameObject.SetActive(true);
            if (_tree.gameObject.name == "Tree 1" && _chopTree1) iconAxe.gameObject.SetActive(false);
            if (_tree.gameObject.name == "Tree 2" && _chopTree2) iconAxe.gameObject.SetActive(false);




            if (_tree.gameObject.name == "Tree 1" && Input.GetMouseButtonDown(0))
            {
                _chopTree1 = true;
                print("ARBOL CAIDO 1");
                _tree.gameObject.GetComponent<Tree>().enabled = false;


            }
            if (_tree.gameObject.name == "Tree 2" && Input.GetMouseButtonDown(0))
            {
                _chopTree2 = true;
                print("ARBOL CAIDO 2");
                _tree.gameObject.GetComponent<Tree>().enabled = false;


            }








        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Tree tree))
        {
            _tree = tree;
            if (!_isChop) iconAxe.gameObject.SetActive(true);
            else iconAxe.gameObject.SetActive(false);



            if (_tree.gameObject.name == "Tree 1" && !_chopTree1) iconAxe.gameObject.SetActive(true);
            if (_tree.gameObject.name == "Tree 2" && !_chopTree2) iconAxe.gameObject.SetActive(true);
            if (_tree.gameObject.name == "Tree 1" && _chopTree1) iconAxe.gameObject.SetActive(false);
            if (_tree.gameObject.name == "Tree 2" && _chopTree2) iconAxe.gameObject.SetActive(false);












            if (_tree.gameObject.name == "Tree 1" && Input.GetMouseButtonDown(0))
            {
                _chopTree1 = true;
                print("ARBOL CAIDO 1");
                _tree.gameObject.GetComponent<Tree>().enabled = false;



            }
            if (_tree.gameObject.name == "Tree 2" && Input.GetMouseButtonDown(0))
            {
                _chopTree2 = true;
                print("ARBOL CAIDO 2");
                _tree.gameObject.GetComponent<Tree>().enabled = false;


            }



        }
    }





    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Tree tree) && tree == _tree)
        {
            _tree = null;
            iconAxe.gameObject.SetActive(false);
        }

    }
}
