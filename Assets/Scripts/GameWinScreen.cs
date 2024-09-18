using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWinScreen : MonoBehaviour
{

    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene("Update");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
