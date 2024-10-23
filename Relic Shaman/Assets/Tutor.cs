using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutor : MonoBehaviour
{
    [SerializeField] public FadeUI tutorPage;
    [SerializeField] private float fadeTime;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            tutorPage.FadeUIIn(fadeTime);
            Destroy(gameObject);
        }
        
       
    }
        
}
