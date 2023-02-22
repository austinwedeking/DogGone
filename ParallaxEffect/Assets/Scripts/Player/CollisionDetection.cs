using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private PlayerData player;

    private void Awake()
    {
        player = gameObject.GetComponent<PlayerData>();
        if (player == null) { Debug.LogError("Could not find and object with a PlayerData script"); }
    }


    //TODO: this is a potentially un-needed script, remove if no longer needed


}
