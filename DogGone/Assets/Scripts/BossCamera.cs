using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera : MonoBehaviour{
    private Transform playerTransform;
    bool FreeCam = false; //False will have the locked camera on the map. True will track the player no matter what
    bool Lock = false;

    void Start(){ //Gets the player object on start
        gameObject.transform.position = new Vector3(-26.1f, 2, -10);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update(){ //Camera has a set Y position, the camera constantly tracks the players X value to move with it
        Vector3 temp = transform.position;
        temp.x = playerTransform.position.x;
        if(temp.x >= -8){
            Lock = true;
        }else if (temp.x >= -26.1 && temp.x <= -8 && Lock == false){
            transform.position = temp;
        }
        Vector3 temp2 = transform.position;
        temp2.y = playerTransform.position.y;
        if (temp2.y >= 2 && temp2.y <= 6 && FreeCam == false)
        {
            transform.position = temp2;
        }
    }
}
