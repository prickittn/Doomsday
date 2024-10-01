using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreenMedium : MonoBehaviour
{

    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene("Update Medium");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
