using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCityAreas : MonoBehaviour{
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
        int Areas = 5; //How many Areas are going to generate
        int Offset; //Stores how much offset a Area needs when being placed
        int RandNum; //Stores a random number to decide what Area to generate
        int Shop = -1; //Stores what number object will have the guaranteed shop in it
        for (int i = 0; i < Areas; i++){
            if (Shop == -1){Shop = Random.Range(0, 5);} //Generates a number 0 to 4 for where the shop will spawn
            Offset = 40; //Inital offset of 40 as an Area is 40 units long
            if (Shop == i){RandNum = 10;} //If its supposed to be the one shop area make it the shop
            else{RandNum = Random.Range(1, 10);} //Otherwise make it generate one of the other random areas

            Offset = Offset + ((i - 1) * 40); //Offsets the inital position by how many Areas have been loaded
            Offset = Offset - ((RandNum - 1) * 45); //Offsets the Area by the position of the prefab because I am an idiot and didn't stack them
            Vector3 StartPosition = new Vector3(Offset, -63, 0); //Sets the placement position of the Area

            //Mega if statement to decide what Level Area to instantiate
            if      (RandNum == 1) {Instantiate(LevelBlock1,  StartPosition, Quaternion.identity);}
            else if (RandNum == 2) {Instantiate(LevelBlock2,  StartPosition, Quaternion.identity);}
            else if (RandNum == 3) {Instantiate(LevelBlock3,  StartPosition, Quaternion.identity);}
            else if (RandNum == 4) {Instantiate(LevelBlock4,  StartPosition, Quaternion.identity);}
            else if (RandNum == 5) {Instantiate(LevelBlock5,  StartPosition, Quaternion.identity);}
            else if (RandNum == 6) {Instantiate(LevelBlock6,  StartPosition, Quaternion.identity);}
            else if (RandNum == 7) {Instantiate(LevelBlock7,  StartPosition, Quaternion.identity);}
            else if (RandNum == 8) {Instantiate(LevelBlock8,  StartPosition, Quaternion.identity);}
            else if (RandNum == 9) {Instantiate(LevelBlock9,  StartPosition, Quaternion.identity);}
            else if (RandNum == 10){Instantiate(LevelBlock10, StartPosition, Quaternion.identity);}
        }
    }
}