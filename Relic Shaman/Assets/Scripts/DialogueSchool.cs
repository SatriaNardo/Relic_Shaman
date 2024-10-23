using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueSchool : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    [SerializeField]
    GameObject DialoguePanel;
    [SerializeField]
    GameObject Scene1;
    private int index;
    [SerializeField]
    AudioClip[] SFX;
    AudioSource playingAudio;
    CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        playingAudio = GetComponent<AudioSource>();
        canvasGroup = GetComponent<CanvasGroup>();  
        StartDialogue(); // tambahkan titik koma di sini
        
    }

    // Update is called once per frame
    void Update()
    {
        if(index == 3)
        {
            playingAudio.clip = SFX[0];
            playingAudio.Play();

        }
        if (index == 5)
        {
            StartCoroutine(FadeOut(0.25f));
            playingAudio.clip = SFX[1];
            playingAudio.Play();

        }
        if (index == 7)
        {
            Scene1.SetActive(true);
        }
        if (index == 8)
        {
            Scene1.SetActive(false);
            StartCoroutine(FadeIn(0.25f));
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(5.0f);
    }
    IEnumerator TypeLine()
    {
        // Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    IEnumerator FadeOut(float _seconds)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 1;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.unscaledDeltaTime / _seconds;
            yield return null;
        }
        yield return null;
    }
    IEnumerator FadeIn(float _seconds)
    {

        canvasGroup.alpha = 0;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.unscaledDeltaTime / _seconds;
            yield return null;
        }
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        yield return null;
    }
    void NextLine()
    {
        if (index < lines.Length - 1) // perbaiki lines.Length di sini
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false); // perbaiki GameObject menjadi gameObject
            SceneManager.LoadScene("HubAfterSchool");
        }
    }
}
