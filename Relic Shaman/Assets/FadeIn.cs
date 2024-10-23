using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fadein : MonoBehaviour
{


    public float FadeRate;
    private Image image;
    private float targetAlpha;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        Material instantiatedMaterial = Instantiate<Material>(image.material);
        image.material = instantiatedMaterial;
        targetAlpha = image.material.color.a;

        Invoke("startFadein", 1);

    }

    IEnumerator FadeIn()
    {
        targetAlpha = 1.0f;
        Color curColor = image.color;
        while (Mathf.Abs(curColor.a - targetAlpha) > 0.0001f)
        {
            Debug.Log(image.material.color.a);
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, FadeRate * Time.deltaTime);
            image.color = curColor;
            yield return null;
        }
    }

    void startFadein()
    {

        StartCoroutine(FadeIn());
    }
}