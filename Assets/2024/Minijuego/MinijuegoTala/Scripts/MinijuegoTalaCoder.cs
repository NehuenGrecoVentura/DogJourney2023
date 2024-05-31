using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class MinijuegoTalaCoder : MonoBehaviour
{
    [SerializeField] private GameObject[] Arrows;
    
    [SerializeField] private int ID_Arrow_A;
    [SerializeField] private GameObject ArrowA;
    private MinijuegoFlecha CodeArrowA;
    [SerializeField] private Transform ASpawn;
    
    [SerializeField] private int ID_Arrow_B;
    [SerializeField] private GameObject ArrowB;
    private MinijuegoFlecha CodeArrowB;
    [SerializeField] private Transform BSpawn;
    
    [SerializeField] private int ID_Arrow_C;
    [SerializeField] private GameObject ArrowC;
    private MinijuegoFlecha CodeArrowC;
    [SerializeField] private Transform CSpawn;

     public Transform GamePoint;
     public Transform outPoint;
     public Transform startPoint;
    
    [SerializeField] private bool ImActive;
    [SerializeField] private bool WaitingForPlayer;
    [SerializeField] public bool Done;
    
    [SerializeField] private int ActiveInt;
    [SerializeField] private int PlayerInt;

    [SerializeField] private MinijuegoTalaManager manager;
    
    private void Start()
    {
        ActiveInt = 0;
        PlayerInt = 8;
        WaitingForPlayer = true;
    }

    public void Reset()
    {
        transform.position = startPoint.position;
        Destroy(ArrowA);
        Destroy(ArrowB);
        Destroy(ArrowC);
        ArrowA = null;
        CodeArrowA = null;
        ArrowB = null;
        CodeArrowB = null;
        ArrowC = null;
        CodeArrowC = null;
        WaitingForPlayer = true;
        Done = false;
    }


    public void GenerateRandom()
    {
        transform.position = startPoint.position;
        
        ID_Arrow_A = UnityEngine.Random.Range(0, 4);
        ArrowA = Instantiate(Arrows[ID_Arrow_A]);
        CodeArrowA = ArrowA.GetComponent<MinijuegoFlecha>();
        CodeArrowA.Queue();
        ArrowA.transform.position = ASpawn.position;
        
        ID_Arrow_B = UnityEngine.Random.Range(0, 4);
        ArrowB = Instantiate(Arrows[ID_Arrow_B]);
        CodeArrowB = ArrowB.GetComponent<MinijuegoFlecha>();
        CodeArrowB.Queue();
        ArrowB.transform.position = BSpawn.position;
        
        ID_Arrow_C = UnityEngine.Random.Range(0, 4);
        ArrowC = Instantiate(Arrows[ID_Arrow_C]);
        CodeArrowC = ArrowC.GetComponent<MinijuegoFlecha>();
        CodeArrowC.Queue();
        ArrowC.transform.position = CSpawn.position;
    }

    public void GoTo()
    {
        if (!Done)
        {
            transform.position = GamePoint.position;
            ImActive = true;
        }
        else if(Done)
        {
            transform.position = outPoint.position;
            ImActive = false;
        }
        ArrowA.transform.position = ASpawn.position;
        ArrowB.transform.position = BSpawn.position;
        ArrowC.transform.position = CSpawn.position;
    }
    
    private void updateArrows()
    {
        if (ActiveInt == 0)
        {
            CodeArrowA.Active();
            if (!WaitingForPlayer)
            {
                if (PlayerInt == ID_Arrow_A)
                {
                    CodeArrowA.Right();
                    manager.GoodClick();
                }
                else
                {
                    CodeArrowA.Wrong();
                    manager.WrongClick();
                }
                ActiveInt++;
                WaitingForPlayer = true;
            }


        }
        else if (ActiveInt == 1)
        {
            CodeArrowB.Active();
            if (!WaitingForPlayer)
            {
                if (PlayerInt == ID_Arrow_B)
                {
                    CodeArrowB.Right();
                    manager.GoodClick();
                }
                else
                {
                    CodeArrowB.Wrong();
                    manager.WrongClick();
                }
                ActiveInt++;
                WaitingForPlayer = true;
            }
           
        }
        else if (ActiveInt == 2)
        {
            CodeArrowC.Active();
            if (!WaitingForPlayer)
            {
                if (PlayerInt == ID_Arrow_C)
                {
                    CodeArrowC.Right();
                    manager.GoodClick();
                }
                else
                {
                    CodeArrowC.Wrong();
                    manager.WrongClick();
                }
                transform.position = outPoint.position;
                Done = true;
                ActiveInt = 0;
            }
        }
    }

    // IDs: Up=0 Left=1 Right=2 Down=3
    private void getPlayerActions()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W))
        {
            PlayerInt = 0;
            WaitingForPlayer = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.A))
        {
            PlayerInt = 1;
            WaitingForPlayer = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.D))
        {
            PlayerInt = 2;
            WaitingForPlayer = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.S))
        {
            PlayerInt = 3;
            WaitingForPlayer = false;
        }
    }

    private void Update()
    {
        if (manager.Gaming)
        {
            if (ImActive)
            {
                getPlayerActions();
                updateArrows();
            }
        }
       
    }
}
