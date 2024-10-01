using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWinScreenHard : MonoBehaviour
{

    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene("Update Hard");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
