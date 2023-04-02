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

    void Start() {
        int Areas = 1;
        int Offset;
        int RandNum;
        for (int i = 0; i < Areas; i++){
            Offset = 40;
            RandNum = Random.Range(1, 11); //Generates a number 1 to 10
            Offset = Offset + ((i - 1) * 40);
            Offset = Offset - ((RandNum-1)*45);
            Vector3 StartPosition = new Vector3(Offset, -63, 0);
            
            if (RandNum == 1){
                Instantiate(LevelBlock1, StartPosition, Quaternion.identity);
            }
            if (RandNum == 2){
                Instantiate(LevelBlock2, StartPosition, Quaternion.identity);
            }
            if (RandNum == 3){
                Instantiate(LevelBlock3, StartPosition, Quaternion.identity);
            }
            if (RandNum == 4){
                Instantiate(LevelBlock4, StartPosition, Quaternion.identity);
            }
            if (RandNum == 5){
                Instantiate(LevelBlock5, StartPosition, Quaternion.identity);
            }
            if (RandNum == 6){
                Instantiate(LevelBlock6, StartPosition, Quaternion.identity);
            }
            if (RandNum == 7){
                Instantiate(LevelBlock7, StartPosition, Quaternion.identity);
            }
            if (RandNum == 8){
                Instantiate(LevelBlock8, StartPosition, Quaternion.identity);
            }
            if (RandNum == 9){
                Instantiate(LevelBlock9, StartPosition, Quaternion.identity);
            }
            if (RandNum == 10){
                Instantiate(LevelBlock10, StartPosition, Quaternion.identity);
            }
        }

        //Ok so hear me out we have a offset value that is calculated with both the [n]th tile that is currently being placed and with what number is being stored
        //Because all of the levels were not painted on top of eachother I have to do some extra work to fix this issue I didn't know would happen
        //The [n]th position is easy, just adjust the offset of the X value for where its places by multiplying it with how many times its been through the for loop
        //This insures no ofset at 0 as 0*40=0 and then accurately offsets all future placements
        //But as they are not equal in position we need to 0 them all out first so we have to check with the random number generated what offset needs to be applied to align it correctly
        //You would have to subtract 45*# area is being generated so that its centered again
        
    }
}
