using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    #region FIELDS

    public AudioClip mainMenuMusic;
    public AudioClip gameSceneMusic;
    public AudioClip[] LevelsMusic;
    private AudioSource audioSource;

    #endregion FIELDS

    #region UNITY METHODS

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    #endregion UNITY METHODS

    #region METHODS

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
                PlayMusic(mainMenuMusic);
                break;

            case "GameScene":
                PlayMusic(gameSceneMusic);
                break;

            default:
                if (scene.name.StartsWith("Level"))
                {
                    int levelIndex = int.Parse(scene.name.Substring(5)) - 1;
                    if (levelIndex < LevelsMusic.Length)
                    {
                        PlayMusic(LevelsMusic[levelIndex]);
                    }
                }
                break;
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    #endregion METHODS
}