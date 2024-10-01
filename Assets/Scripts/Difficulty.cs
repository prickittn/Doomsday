using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Difficulty : MonoBehaviour
{

    public string EasyGameScene;
    public string MediumGameScene;
    public string HardGameScene;

    public void LoadEasy()
    {
        SceneManager.LoadScene(EasyGameScene);
    }

    public void LoadMedium()
    {
        SceneManager.LoadScene(MediumGameScene);
    }

    public void LoadHard()
    {
        SceneManager.LoadScene(HardGameScene);
    }
}
