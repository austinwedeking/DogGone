using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;

    private Vector3 playerVector;
    private Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerVector.y = playerTransform.position.y;
        playerVector.x = playerTransform.position.x;
        playerVector.z = -10f;
        gameObject.transform.position = new Vector3(playerVector.x, playerVector.y, playerVector.z);
    }
}
