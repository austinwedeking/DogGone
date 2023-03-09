using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    private int numEnemies;
    [SerializeField] private int loseSceneIndex;
    [SerializeField] private int winSceneIndex;

    private void Awake()
    {
        numEnemies = 0;
    }

    public void IncrementEnemies()
    {
        ++numEnemies;
    }

    public void DecrementEnemies()
    {
        --numEnemies;
        if (numEnemies <= 0) { Win(); }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(loseSceneIndex);
    }

    public void Win()
    {
        SceneManager.LoadScene(winSceneIndex);
    }
}
