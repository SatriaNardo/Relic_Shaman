using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentShop : MonoBehaviour
{
    bool ShopIsActive = false;
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (GameManager.Instance.ableToShop)
            {
                if (Input.GetButtonDown("Interacted") && ShopIsActive == false)
                {
                    UIManager.Instance.shopTalent.SetActive(true);
                    ShopIsActive = true;
                }
                else if (Input.GetButtonDown("Interacted") && ShopIsActive == true)
                {
                    UIManager.Instance.shopTalent.SetActive(false);
                    ShopIsActive = false;
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(UIManager.Instance.shopTalent.activeInHierarchy)
        {
            UIManager.Instance.shopTalent.SetActive(false);
        }
    }
}
