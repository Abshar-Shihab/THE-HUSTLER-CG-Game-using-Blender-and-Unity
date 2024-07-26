using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject completeLevelUI;
    public GameObject failedLevelUI;
    bool gameHasEnded = false;
    public void EndGame()
    {
        //Debug.Log("Game Over");
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            failedLevelUI.SetActive(true);
            Invoke("Restart", 10f); // to invoke a function with delay in secs
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompleteLevel()
    {
        Debug.Log("Level Completed");
        completeLevelUI.SetActive(true);
        Invoke("NextLevel", 10f);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
