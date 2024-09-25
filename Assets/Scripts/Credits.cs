using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

    public string BackScene;

    public void GoBack()
    {
        SceneManager.LoadScene(BackScene);
    }
}
