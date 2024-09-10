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
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void StartOriginal()
    {
        audioManager.PlaySFX(audioManager.menuClick);
        SceneManager.LoadScene(OriginalGameScene);
    }

    public void StartUpdated()
    {
        audioManager.PlaySFX(audioManager.menuClick);
        SceneManager.LoadScene(UpdatedGameScene);
    }

    public void StartCredits()
    {
        audioManager.PlaySFX(audioManager.menuClick);
        SceneManager.LoadScene(CreditsScene);
    }

    public void StartVersionNotes()
    {
        audioManager.PlaySFX(audioManager.menuClick);
        SceneManager.LoadScene(VersionNotesScene);
    }

    public void Quitgame()
    {
        audioManager.PlaySFX(audioManager.menuClick);
        Application.Quit();
    }
}
