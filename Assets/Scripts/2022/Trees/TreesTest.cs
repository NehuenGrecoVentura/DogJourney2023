using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesTest : MonoBehaviour
{
    [SerializeField] private int HitPoints;
    [SerializeField] private int MaxHitpoints;
    [SerializeField] private GameObject TreeFall;
    [SerializeField] private GameObject sapling;
    [SerializeField] private Sapling _sapling;
    [SerializeField] private AudioSource _myAudio;
    [SerializeField] private AudioClip treeHit;
    [SerializeField] private AudioClip soundCutTree;
    Character2022 _playerAnim;

    private void Start()
    {
        _playerAnim = FindObjectOfType<Character2022>();
        HitPoints = MaxHitpoints;
        TreeFall.SetActive(false);
        sapling.SetActive(false);
    }
    
    public void getHit(int Damage)
    {
        _playerAnim.EjecuteAnim("Cut");
        HitPoints = HitPoints - Damage;
        _myAudio.PlayOneShot(treeHit);
        if (HitPoints <= 0)
        {
            Fall();
            sapling.SetActive(true);
        }
    }

    public void Fall()
    {
        _myAudio.PlayOneShot(soundCutTree);
        TreeFall.SetActive(true);
        sapling.SetActive(true);
        _sapling.setGrowTrue();
        HitPoints = MaxHitpoints;
        gameObject.SetActive(false);
    }
    
   
}
