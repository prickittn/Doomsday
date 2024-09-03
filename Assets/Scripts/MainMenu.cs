using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string OriginalGameScene;
    public string UpdatedGameScene;
    public string CreditsScene;
    public string VersionNotesScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartOriginal()
    {
        SceneManager.LoadScene(OriginalGameScene);
    }

    public void StartUpdated()
    {
        SceneManager.LoadScene(UpdatedGameScene);
    }

    public void StartCredits()
    {
        SceneManager.LoadScene(CreditsScene);
    }

    public void StartVersionNotes()
    {
        SceneManager.LoadScene(VersionNotesScene);
    }

    public void Quitgame()
    {
        Application.Quit();
    }
}
