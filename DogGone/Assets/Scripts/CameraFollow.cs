using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    bool FreeCam = false; //False will have the locked camera on the map. True will track the player no matter what
    void Start(){ //Gets the player object on start
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update(){ //Camera has a set Y position, the camera constantly tracks the players X value to move with it
        Vector3 temp = transform.position;
        temp.x = playerTransform.position.x;
        //if (temp.x >= -11.5 && FreeCam == false){
            transform.position = temp;
        //}
        Vector3 temp2 = transform.position;
        temp2.y = playerTransform.position.y;
        if (temp2.y >= -1 && temp2.y <= 4 && FreeCam == false){
            transform.position = temp2;
        }
        
        //Could potentially improve later with taller levels or levels with edge walls where if the x/y is more then the min it stops scrolling and then will go back to tracking the dog once in the range
        //So if the players get(x) is less then a value at the start of screen then stop camera tracking so that camera will not show too much offscreen
        //Same could be used for get(y) too where it will track if you go above the point where near the bottom is the floor so it will scroll with you
    }
}
