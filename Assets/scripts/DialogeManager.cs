using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem; // IMPORTANTE: Añade esta línea

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject textBoxCanvas;
    public TextMeshProUGUI textDisplay;
    public float typingSpeed = 0.02f;

    private string[] sentences;
    private int index;
    private bool isTyping = false;
    private bool isDialogueActive = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        textBoxCanvas.SetActive(false);
    }

    void Update()
    {
        // Detectamos la tecla Enter usando el nuevo Input System
        if (isDialogueActive && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (isTyping)
            {
                StopAllCoroutines();
                textDisplay.text = sentences[index];
                isTyping = false;
            }
            else
            {
                NextSentence();
            }
        }
    }

    public void ShowText(string[] lines)
    {
        Debug.Log("ha entrado2");
        sentences = lines;
        index = 0;
        isDialogueActive = true;
        textBoxCanvas.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textDisplay.text = "";
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            isDialogueActive = false;
            textBoxCanvas.SetActive(false);
        }
    }
}