using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public static SpawnBoss Instance;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject boss;
    [SerializeField] Vector2 exitDirection;
    BoxCollider2D col;
    bool callOnce;
    private void Awake()
    {
        if (BeguGanjang.Instance != null)
        {
            Destroy(BeguGanjang.Instance);
            callOnce = false;
            col.isTrigger = true;
        }

        if (GameManager.Instance.BeguGajangDefeated)
        {
            callOnce = true;
        }
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(!callOnce)
            {
                StartCoroutine(WalkIntoRoom());
                callOnce = true;
            }
        }
    }
    IEnumerator WalkIntoRoom()
    {
        StartCoroutine(PlayerController.Instance.WalkintoNewScene(exitDirection, 1));
        //PlayerController.Instance.GetComponent().cutscene = true;
        yield return new WaitForSeconds(1f);
        col.isTrigger = false;
        Instantiate(boss, spawnPoint.position, Quaternion.identity);
    }
    public void isNotTrigger()
    {
        col.isTrigger = true;
    }    
}
