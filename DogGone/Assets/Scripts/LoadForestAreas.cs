using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadForestAreas : MonoBehaviour
{
    public GameObject LevelBlock1;
    public GameObject LevelBlock2;
    public GameObject LevelBlock3;
    public GameObject LevelBlock4;
    public GameObject LevelBlock5;
    public GameObject LevelBlock6;
    public GameObject LevelBlock7;
    public GameObject LevelBlock8;
    public GameObject LevelBlock9;
    public GameObject LevelBlock10;

    void Start(){
        Vector3 StartPosition = new Vector3(100, -60, 0);
        Instantiate(LevelBlock1, StartPosition, Quaternion.identity);
        Debug.Log("HERE IM HERE");
    }
}
