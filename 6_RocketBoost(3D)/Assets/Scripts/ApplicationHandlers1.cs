using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationHandlers1 : MonoBehaviour
{
    public void GameStart()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }

    public void GameEnd()
    {
        Application.Quit();
        Debug.Log("yup");
    }

}
