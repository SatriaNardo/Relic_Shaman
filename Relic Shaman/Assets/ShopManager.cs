using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject Confirmation;
    public GameObject DontHaveShard;
    public GameObject WallGuide;
    public GameObject DashGuide;
    public GameObject DoubleGuide;
    public GhostShard ghostShard;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddMaxHealth()
    {
        if(PlayerController.Instance.maxHealth < 10)
        {
            if (ghostShard.shard >= 500)
            {
                PlayerController.Instance.maxHealth += 1;
                PlayerController.Instance.health = PlayerController.Instance.maxHealth;
                HeartController.Instance.UpdateHeartOnBuying();
                ghostShard.shard -= 500;
                ghostShard.UpdateCounter();
                SaveData.Instance.SavePlayerData();
            }
            else
            {
                DontHaveShard.SetActive(true);
            }
        }
        else
        {
            Confirmation.SetActive(true);
            
        }
    }
    public void AddMaxMana()
    {
        if (PlayerController.Instance.manaOrbs < 3)
        {
            if (ghostShard.shard >= 500)
            {
                PlayerController.Instance.manaOrbs += 1;
                ghostShard.shard -= 500;
                ghostShard.UpdateCounter();
                SaveData.Instance.SavePlayerData();
            }
            else
            {
                DontHaveShard.SetActive(true);
            }
        }
        else
        {
            Confirmation.SetActive(true);
        }


    }
    public void unlockWallJump()
    {
        if(!PlayerController.Instance.unlockedWallJump)
        {
            if (ghostShard.shard >= 500)
            {
                PlayerController.Instance.unlockedWallJump = true;
                SaveData.Instance.SavePlayerData();
                ghostShard.shard -= 500;
                ghostShard.UpdateCounter();
                WallGuide.SetActive(true);
            }
            else
            {
                DontHaveShard.SetActive(true);
                
            }
        }
        else
        {
            Confirmation.SetActive(true);
        }

    }
    public void unlockDoubleJump()
    {
        if(PlayerController.Instance.maxAirJumps == 0)
        {
            if (ghostShard.shard >= 500)
            {
                PlayerController.Instance.maxAirJumps = 1;
                ghostShard.shard -= 500;
                ghostShard.UpdateCounter();
                SaveData.Instance.SavePlayerData();
                DoubleGuide.SetActive(true);
            }
            else
            {
                DontHaveShard.SetActive(true);
            }
        }
        else
        {
            Confirmation.SetActive(true);
            
        }

    }
    public void unlockDash()
    {
        if(!PlayerController.Instance.unlockedDash)
        {
            if (ghostShard.shard >= 500)
            {
                PlayerController.Instance.unlockedDash = true;
                ghostShard.shard -= 500;
                SaveData.Instance.SavePlayerData();
                DashGuide.SetActive(true);
            }
            else
            {
                DontHaveShard.SetActive(true);
                
            }
        }
        else
        {
            Confirmation.SetActive(true);
        }
    }
}
