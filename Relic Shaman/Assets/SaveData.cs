using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]   
public struct SaveData
{
    public static SaveData Instance;
    public HashSet<string> sceneNames;

    public string benchSceneName;
    public Vector2 benchPos;

    public int playerHealth;
    public int playerMaxHealth;
    public float playerMana;
    public int playerManaOrbs;
    public float playerOrb0Fill, playerOrb1Fill, playerOrb2Fill;
    public Vector2 playerPosition;
    public string lastScene;

    public bool playerUnlockedWallJump;
    public bool playerUnlockedDash;
    public bool playerUnlockedKnife;
    public bool playerUnlockedAttack;
    public int playerDoubleJump;
    public int ShardCount;
    public void Initialize()
    {
        if(!File.Exists(Application.persistentDataPath + "/save.bench.data"))
        {
            BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.bench.data"));
        }
        if (!File.Exists(Application.persistentDataPath + "/save.player.data"))
        {
            BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.player.data"));
        }
        if (!File.Exists(Application.persistentDataPath + "/save.boss.data"))
        {
            BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.boss.data"));
        }
        if (sceneNames == null)
        {
            sceneNames = new HashSet<string>(); 
        }
    }

    public void SaveBench()
    {
        using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.bench.data")))
        {
            writer.Write(benchSceneName);
            writer.Write(benchPos.x);
            writer.Write(benchPos.y);   
        }
    }
    public void SavePlayerData()
    {
        using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.player.data")))
        {
            playerHealth = PlayerController.Instance.Health;
            writer.Write(playerHealth);

            playerMaxHealth = PlayerController.Instance.maxHealth;
            writer.Write(playerMaxHealth);

            playerMana = PlayerController.Instance.Mana;
            writer.Write(playerMana);

            playerManaOrbs = PlayerController.Instance.manaOrbs;
            writer.Write(playerManaOrbs);

            playerOrb0Fill = PlayerController.Instance.manaOrbsHandler.orbFills[0].fillAmount;
            writer.Write(playerOrb0Fill);
            playerOrb1Fill = PlayerController.Instance.manaOrbsHandler.orbFills[1].fillAmount;
            writer.Write(playerOrb1Fill);
            playerOrb2Fill = PlayerController.Instance.manaOrbsHandler.orbFills[2].fillAmount;
            writer.Write(playerOrb2Fill);

            playerUnlockedWallJump = PlayerController.Instance.unlockedWallJump;
            writer.Write(playerUnlockedWallJump);

            playerUnlockedDash = PlayerController.Instance.unlockedDash;
            writer.Write(playerUnlockedDash);

            playerUnlockedKnife = PlayerController.Instance.unlockKnife;
            writer.Write(playerUnlockedKnife);

            playerUnlockedAttack = PlayerController.Instance.ableToAttack;
            writer.Write(playerUnlockedAttack);

            playerDoubleJump = PlayerController.Instance.maxAirJumps;
            writer.Write(playerDoubleJump);

            playerPosition = PlayerController.Instance.transform.position;
            writer.Write(playerPosition.x);
            writer.Write(playerPosition.y);

            lastScene = SceneManager.GetActiveScene().name;
            writer.Write(lastScene);

        }
    }
    //TheHollowKnight
    public bool BeguDefeated;

    public void SaveBossData()
    {
        using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.boss.data")))
        {
            BeguDefeated = GameManager.Instance.BeguGajangDefeated;

            writer.Write(BeguDefeated);
        }
    }

    public void LoadBossData()
    {
        if (File.Exists(Application.persistentDataPath + "/save.Boss.data"))

        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.boss.data")))
            {
                BeguDefeated = reader.ReadBoolean();

                GameManager.Instance.BeguGajangDefeated = BeguDefeated;
            }
        }
        else
        {
            Debug.Log("Boss doesnt exist");
        }
    }

    public void LoadBench()
    {
        if(File.Exists(Application.persistentDataPath + "/save.bench.data")) 
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.bench.data")))
            {
                benchSceneName = reader.ReadString();
                benchPos.x = reader.ReadSingle();
                benchPos.y = reader.ReadSingle();   
            }
        }
        else
        {
            Debug.Log("Bench doesnt exist");
        }
    }
    public void LoadPlayerData()
    {
        if (File.Exists(Application.persistentDataPath + "/save.player.data"))
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.player.data")))
            {
                playerHealth = reader.ReadInt32();
                playerMaxHealth = reader.ReadInt32();
                playerMana = reader.ReadSingle();
                playerManaOrbs = reader.ReadInt32();
                playerOrb0Fill = reader.ReadSingle();
                playerOrb1Fill = reader.ReadSingle();
                playerOrb2Fill = reader.ReadSingle();
                playerUnlockedWallJump = reader.ReadBoolean();
                playerUnlockedDash = reader.ReadBoolean();
                playerUnlockedKnife = reader.ReadBoolean();
                playerUnlockedAttack = reader.ReadBoolean();
                playerDoubleJump = reader.ReadInt32();
                playerPosition.x = reader.ReadSingle();
                playerPosition.y = reader.ReadSingle();
                lastScene = reader.ReadString();
                SceneManager.LoadScene(lastScene);
                
                PlayerController.Instance.transform.position = playerPosition;
                PlayerController.Instance.maxHealth = playerMaxHealth;
                PlayerController.Instance.Health = playerHealth;
                PlayerController.Instance.Mana = playerMana;
                PlayerController.Instance.manaOrbs = playerManaOrbs;
                PlayerController.Instance.manaOrbsHandler.orbFills[0].fillAmount = playerOrb0Fill;
                PlayerController.Instance.manaOrbsHandler.orbFills[1].fillAmount = playerOrb1Fill;
                PlayerController.Instance.manaOrbsHandler.orbFills[2].fillAmount = playerOrb2Fill;
                PlayerController.Instance.unlockedWallJump = playerUnlockedWallJump;
                PlayerController.Instance.unlockedDash = playerUnlockedDash;
                PlayerController.Instance.unlockKnife = playerUnlockedKnife;
                PlayerController.Instance.ableToAttack = playerUnlockedAttack;
                PlayerController.Instance.maxAirJumps = playerDoubleJump;


            }
            Debug.Log("Load Player Data");
        }
        else
        {
            Debug.Log("File Doesnt Exist");
            PlayerController.Instance.maxHealth = 3;
            PlayerController.Instance.Health = PlayerController.Instance.maxHealth;
            PlayerController.Instance.Mana = 0.5f;
            PlayerController.Instance.manaOrbs = 0;
            PlayerController.Instance.unlockedWallJump = false;
            PlayerController.Instance.unlockedDash = false;
            PlayerController.Instance.maxAirJumps = 0;
            PlayerController.Instance.unlockKnife= false;   
        }
    }
    private string SaveFilePathBench
    {
        get { return Application.persistentDataPath + "/save.bench.data"; }
    }
    private string SaveFilePathPlayer
    {
        get { return Application.persistentDataPath + "/save.player.data"; }
    }
    private string SaveFilePathBoss
    {
        get { return Application.persistentDataPath + "/save.boss.data"; }
    }
    public void Delete()
    {
        try
        {
            File.Delete(SaveFilePathPlayer);
            File.Delete(SaveFilePathBench);
            File.Delete(SaveFilePathBoss);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
