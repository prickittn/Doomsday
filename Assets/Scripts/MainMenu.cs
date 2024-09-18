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
    MenuAudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("MenuAudio").GetComponent<MenuAudioManager>();
    }

    public void StartOriginal()
    {
        audioManager.PlaySFX(audioManager.menuClick);
        audioManager.StopMusic();
        DontDestroyOnLoad(audioManager);
        SceneManager.LoadScene(OriginalGameScene);
    }

    public void StartUpdated()
    {
        audioManager.StopMusic();
        SceneManager.LoadScene(UpdatedGameScene);
    }

    public void StartCredits()
    {
        audioManager.PlaySFX(audioManager.menuClick);
        DontDestroyOnLoad(audioManager);
        SceneManager.LoadScene(CreditsScene);
    }

    public void StartVersionNotes()
    {
        audioManager.PlaySFX(audioManager.menuClick);
        DontDestroyOnLoad(audioManager);
        SceneManager.LoadScene(VersionNotesScene);
    }

    public void Quitgame()
    {
        audioManager.PlaySFX(audioManager.menuClick);
        Application.Quit();
    }
}
