using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string transitionedFromScene;
    public Vector2 defaultRespawnPoint;
    public Vector2 respawnPoint;
    public bool ableToShop;
    public bool BeguGajangDefeated = false;
    [SerializeField] Bench bench;

    [SerializeField] private FadeUI pauseMenu;
    [SerializeField] private float fadeTime;
    public bool gameisPaused;

    //[SerializeField] private FadeUI pauseMenu;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        SaveData.Instance.Initialize(); 
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        SaveScene();
        
        DontDestroyOnLoad(gameObject);
        bench = FindObjectOfType<Bench>();

        try
        {
            SaveData.Instance.LoadBossData();
            if(BeguGajangDefeated)
            {

            }
        } catch(Exception e)
        {
            Debug.Log("Data not found");
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !gameisPaused)
            {
                pauseMenu.FadeUIIn(fadeTime);
                Time.timeScale = 0;
                gameisPaused = true;
            }
        }
        
    }
    public void SaveGame()
    {
        SaveData.Instance.SavePlayerData();
        SaveData.Instance.benchSceneName = SceneManager.GetActiveScene().name;
        SaveData.Instance.benchPos = new Vector2(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.y);
        SaveData.Instance.SaveBench();
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1;
        gameisPaused = false;
    }
    public void SaveScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SaveData.Instance.sceneNames.Add(currentSceneName);     
    }
    public void RespawnPlayer()
    {
        SaveData.Instance.LoadBench();
        if(SaveData.Instance.benchSceneName != null)
        {
            SceneManager.LoadScene(SaveData.Instance.benchSceneName);
        }

        if (SaveData.Instance.benchPos != null)
        {
            respawnPoint = SaveData.Instance.benchPos;
        }
        else
        {
            respawnPoint = defaultRespawnPoint;
        }
        
        PlayerController.Instance.transform.position = respawnPoint;
        StartCoroutine(UIManager.Instance.DeactivateDeathScreen());
        PlayerController.Instance.Respawned();

    }
}
