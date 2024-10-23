using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    [SerializeField]
    GameObject DialoguePanel;
    [SerializeField]
    GameObject Scene1;
    [SerializeField]
    GameObject Scene2;
    private int index;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue(); // tambahkan titik koma di sini
    }

    // Update is called once per frame
    void Update()
    {
        if(index == 4)
        {
            var sayDialog = DialoguePanel.GetComponent<RectTransform>();
            var pos = sayDialog.anchoredPosition;
            sayDialog.anchoredPosition = new Vector3(pos.x, -327);
            Scene1.SetActive(true);
        }
        if (index == 7)
        {
            Scene1.SetActive(false);
            Scene2.SetActive(true);
        }
        if (index == 10)
        {
            var sayDialog = DialoguePanel.GetComponent<RectTransform>();
            var pos = sayDialog.anchoredPosition;
            sayDialog.anchoredPosition = new Vector3(pos.x, 0);
            Scene2.SetActive(false);
        }
        if (Input.GetButtonDown("Attack"))
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

    IEnumerator TypeLine()
    {
        // Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
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
            SceneManager.LoadScene(sceneName); ;
        }
    }
}
