using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadForestAreas : MonoBehaviour{
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
    public GameObject LevelBlock11;
    public GameObject ELevelBlock1;
    public GameObject ELevelBlock2;
    public GameObject ELevelBlock3;
    public GameObject ELevelBlock4;
    public GameObject ELevelBlock5;
    public GameObject ELevelBlock6;
    public GameObject ELevelBlock7;
    public GameObject ELevelBlock8;
    public GameObject ELevelBlock9;
    public GameObject ELevelBlock10;
    public GameObject ELevelBlock11;
    public GameObject HLevelBlock1;
    public GameObject HLevelBlock2;
    public GameObject HLevelBlock3;
    public GameObject HLevelBlock4;
    public GameObject HLevelBlock5;
    public GameObject HLevelBlock6;
    public GameObject HLevelBlock7;
    public GameObject HLevelBlock8;
    public GameObject HLevelBlock9;
    public GameObject HLevelBlock10;
    public GameObject HLevelBlock11;
    
    void Start(){
        int Areas = 4; //How many Areas are going to generate
        int Offset; //Stores how much offset a Area needs when being placed
        int RandNum; //Stores a random number to decide what Area to generate
        int Difficulty = 1; //Stores the difficulty of the game
        bool Shop = false; //Stores whether or not a shop spawned yet so we can't have duplicates even though its a 1/121 chance for even 1 duplicate
        for (int i = 0; i < Areas; i++){
            Offset = 40; //Inital offset of 40 as an Area is 40 units long
            RandNum = Random.Range(1, 12); //Generates a number 1 to 11
            if (Shop == true && RandNum == 11){ //Only checks if its generating a shop if a shop has already generated
                RandNum = Random.Range(1, 11);} //Generates a number 1 to 10 so it cannot roll the shop
            if(RandNum == 11){Shop = true;} //If its going to spawn a shop set the bool that a shop has spawned
            Offset = Offset + ((i - 1) * 40); //Offsets the inital position by how many Areas have been loaded
            Offset = Offset - ((RandNum-1)*45); //Offsets the Area by the position of the prefab because I am an idiot and didn't stack them
            Vector3 StartPosition = new Vector3(Offset, -63, 0); //Sets the placement position of the Area
            //Mega if statement to decide what Level Area to instantiate
            if (SceneManager.GetActiveScene().buildIndex == 2){
                if     (RandNum == 1) {Instantiate(ELevelBlock1, StartPosition, Quaternion.identity);}
                else if(RandNum == 2) {Instantiate(ELevelBlock2, StartPosition, Quaternion.identity);}
                else if(RandNum == 3) {Instantiate(ELevelBlock3, StartPosition, Quaternion.identity);}
                else if(RandNum == 4) {Instantiate(ELevelBlock4, StartPosition, Quaternion.identity);}
                else if(RandNum == 5) {Instantiate(ELevelBlock5, StartPosition, Quaternion.identity);}
                else if(RandNum == 6) {Instantiate(ELevelBlock6, StartPosition, Quaternion.identity);}
                else if(RandNum == 7) {Instantiate(ELevelBlock7, StartPosition, Quaternion.identity);}
                else if(RandNum == 8) {Instantiate(ELevelBlock8, StartPosition, Quaternion.identity);}
                else if(RandNum == 9) {Instantiate(ELevelBlock9, StartPosition, Quaternion.identity);}
                else if(RandNum == 10){Instantiate(ELevelBlock10,StartPosition, Quaternion.identity);}
                else if(RandNum == 11){Instantiate(ELevelBlock11,StartPosition, Quaternion.identity);}
            }else if (SceneManager.GetActiveScene().buildIndex == 3){
                if     (RandNum == 1) {Instantiate(LevelBlock1, StartPosition, Quaternion.identity);}
                else if(RandNum == 2) {Instantiate(LevelBlock2, StartPosition, Quaternion.identity);}
                else if(RandNum == 3) {Instantiate(LevelBlock3, StartPosition, Quaternion.identity);}
                else if(RandNum == 4) {Instantiate(LevelBlock4, StartPosition, Quaternion.identity);}
                else if(RandNum == 5) {Instantiate(LevelBlock5, StartPosition, Quaternion.identity);}
                else if(RandNum == 6) {Instantiate(LevelBlock6, StartPosition, Quaternion.identity);}
                else if(RandNum == 7) {Instantiate(LevelBlock7, StartPosition, Quaternion.identity);}
                else if(RandNum == 8) {Instantiate(LevelBlock8, StartPosition, Quaternion.identity);}
                else if(RandNum == 9) {Instantiate(LevelBlock9, StartPosition, Quaternion.identity);}
                else if(RandNum == 10){Instantiate(LevelBlock10,StartPosition, Quaternion.identity);}
                else if(RandNum == 11){Instantiate(LevelBlock11,StartPosition, Quaternion.identity);}
            }else if (Difficulty == 3){
                if     (RandNum == 1) {Instantiate(HLevelBlock1, StartPosition, Quaternion.identity);}
                else if(RandNum == 2) {Instantiate(HLevelBlock2, StartPosition, Quaternion.identity);}
                else if(RandNum == 3) {Instantiate(HLevelBlock3, StartPosition, Quaternion.identity);}
                else if(RandNum == 4) {Instantiate(HLevelBlock4, StartPosition, Quaternion.identity);}
                else if(RandNum == 5) {Instantiate(HLevelBlock5, StartPosition, Quaternion.identity);}
                else if(RandNum == 6) {Instantiate(HLevelBlock6, StartPosition, Quaternion.identity);}
                else if(RandNum == 7) {Instantiate(HLevelBlock7, StartPosition, Quaternion.identity);}
                else if(RandNum == 8) {Instantiate(HLevelBlock8, StartPosition, Quaternion.identity);}
                else if(RandNum == 9) {Instantiate(HLevelBlock9, StartPosition, Quaternion.identity);}
                else if(RandNum == 10){Instantiate(HLevelBlock10,StartPosition, Quaternion.identity);}
                else if(RandNum == 11){Instantiate(HLevelBlock11,StartPosition, Quaternion.identity);}
            }
        }
    }
}