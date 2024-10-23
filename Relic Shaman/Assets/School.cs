using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class School : MonoBehaviour
{
    bool interactable;
    public GameObject instruction;
    void Update()
    {
        if (Input.GetButtonDown("Interacted") && interactable == true)
        {
            SceneManager.LoadScene("CutsceneSchool");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            interactable = true;
            instruction.SetActive(true);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactable = false;
            instruction.SetActive(false);
        }
    }
}
