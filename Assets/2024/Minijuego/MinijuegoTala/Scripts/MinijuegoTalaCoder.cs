using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using DG.Tweening;
using Random = System.Random;

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
    
    [SerializeField] private int ID_Arrow_D;
    [SerializeField] private GameObject ArrowD;
    private MinijuegoFlecha CodeArrowD;
    [SerializeField] private Transform DSpawn;
    
    [SerializeField] private int ID_Arrow_E;
    [SerializeField] private GameObject ArrowE;
    private MinijuegoFlecha CodeArrowE;
    [SerializeField] private Transform ESpawn;
    
    [SerializeField] private int ID_Arrow_F;
    [SerializeField] private GameObject ArrowF;
    private MinijuegoFlecha CodeArrowF;
    [SerializeField] private Transform FSpawn;

     public Transform GamePoint;
     public Transform outPoint;
     public Transform startPoint;
    
    [SerializeField] private bool ImActive;
    [SerializeField] private bool WaitingForPlayer;
    [SerializeField] public bool Done;
    
    [SerializeField] private int ActiveInt;
    [SerializeField] private int PlayerInt;
    [SerializeField] public int Rounds;

    [SerializeField] private MinijuegoTalaManager manager;
    
    private void Start()
    {
        ActiveInt = 0;
        PlayerInt = 8;
        WaitingForPlayer = true;
        Rounds = -1;
    }

    public void Reset()
    {
        Rounds++;
        transform.position = startPoint.position;
        Destroy(ArrowA);
        Destroy(ArrowB);
        Destroy(ArrowC);
        Destroy(ArrowD);
        Destroy(ArrowE);
        Destroy(ArrowF);
        ArrowA = null;
        CodeArrowA = null;
        ArrowB = null;
        CodeArrowB = null;
        ArrowC = null;
        CodeArrowC = null;
        ArrowD = null;
        CodeArrowD = null;
        ArrowE = null;
        CodeArrowE = null;
        ArrowF = null;
        CodeArrowF = null;
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

        if (Rounds >= 3)
        {
            ID_Arrow_D = UnityEngine.Random.Range(0, 4);
            ArrowD = Instantiate(Arrows[ID_Arrow_D]);
            CodeArrowD = ArrowD.GetComponent<MinijuegoFlecha>();
            CodeArrowD.Queue();
            ArrowD.transform.position = DSpawn.position;
        }
        if (Rounds >= 6)
        {
            ID_Arrow_E = UnityEngine.Random.Range(0, 4);
            ArrowE = Instantiate(Arrows[ID_Arrow_E]);
            CodeArrowE = ArrowE.GetComponent<MinijuegoFlecha>();
            CodeArrowE.Queue();
            ArrowE.transform.position = ESpawn.position;
        }
        if (Rounds >= 9)
        {
            ID_Arrow_F = UnityEngine.Random.Range(0, 4);
            ArrowF = Instantiate(Arrows[ID_Arrow_F]);
            CodeArrowF = ArrowF.GetComponent<MinijuegoFlecha>();
            CodeArrowF.Queue();
            ArrowF.transform.position = FSpawn.position; 
        }
    } 

    public void GoTo()
    {
        
        if (!Done)
        {
            transform.position = GamePoint.position;
            ImActive = true;

            //transform.DOMove(GamePoint.position, 0.5f);
            //ImActive = true;
        }

        else if(Done)
        {
            manager.ResetWood();
            transform.position = outPoint.position;
            ImActive = false;
           
            //transform.DOMove(outPoint.position, 0.5f);
            //ImActive = false;
        }

        //ArrowA.transform.position = ASpawn.position;
        //ArrowB.transform.position = BSpawn.position;
        //ArrowC.transform.position = CSpawn.position;


        ArrowA.transform.DOMove(ASpawn.position, 0.5f);
        ArrowB.transform.DOMove(BSpawn.position, 0.5f);
        ArrowC.transform.DOMove(CSpawn.position, 0.5f);
        if (Rounds >= 3)
        {
            ArrowD.transform.DOMove(DSpawn.position, 0.5f);
        }

        if (Rounds >= 6)
        {
            ArrowE.transform.DOMove(ESpawn.position, 0.5f);
        }

        if (Rounds >= 9)
        {
            ArrowF.transform.DOMove(FSpawn.position, 0.5f);
        }

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
                if (Rounds >= 3)
                {
                    ActiveInt++;
                    WaitingForPlayer = true;
                }
                else
                {
                    ActiveInt = 0; 
                    Done = true;
                }
            }
        }
        else if (ActiveInt == 3)
        {
            CodeArrowD.Active();
            if (!WaitingForPlayer)
            {
                if (PlayerInt == ID_Arrow_D)
                {
                    CodeArrowD.Right();
                    manager.GoodClick();
                }
                else
                {
                    CodeArrowD.Wrong();
                    manager.WrongClick();
                }
                transform.position = outPoint.position;
                if (Rounds >= 6)
                {
                    ActiveInt++;
                    WaitingForPlayer = true;
                }
                else
                {
                    ActiveInt = 0; 
                    Done = true;
                }
            }
        }
        else if (ActiveInt == 4)
        {
            CodeArrowE.Active();
            if (!WaitingForPlayer)
            {
                if (PlayerInt == ID_Arrow_E)
                {
                    CodeArrowE.Right();
                    manager.GoodClick();
                }
                else
                {
                    CodeArrowE.Wrong();
                    manager.WrongClick();
                }
                transform.position = outPoint.position;
                if (Rounds >= 9)
                {
                    ActiveInt++;
                    WaitingForPlayer = true;
                }
                else
                {
                    ActiveInt = 0; 
                    Done = true;
                }
            }
        }
        else if (ActiveInt == 5)
        {
            CodeArrowF.Active();
            if (!WaitingForPlayer)
            {
                if (PlayerInt == ID_Arrow_F)
                {
                    CodeArrowF.Right();
                    manager.GoodClick();
                }
                else
                {
                    CodeArrowF.Wrong();
                    manager.WrongClick();
                }
                ActiveInt = 0;
                Done = true;
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
