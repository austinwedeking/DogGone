using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    [SerializeField] private int numEnemies;
    [SerializeField] private int loseSceneIndex;
    [SerializeField] private int winSceneIndex;

    private void Awake()
    { //Called before start on object creation, just here to make the value start at 0
        numEnemies = 0;
    }

    public void IncrementEnemies()
    { //When an enemy spawns increment the enemy number
        ++numEnemies;
    }

    public void DecrementEnemies()
    { //When an enemy dies decrement the enemy number
        --numEnemies;
        if (numEnemies <= 0)
        { //If no enemies are alive then show the win screen
            Debug.Log("You win!");
        }
    }

    //public void GameOver()
    //{ //Loads the loose screen
    //    SceneManager.LoadScene(loseSceneIndex);
    //}

    //private void Win()
    //{ //Loads the win screen
    //    SceneManager.LoadScene(winSceneIndex);
    //}
}