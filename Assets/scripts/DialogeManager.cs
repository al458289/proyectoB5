using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

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
        sentences = lines;
        index = 0;
        isDialogueActive = true;
        textBoxCanvas.SetActive(true);

        // 1. CONGELAMOS EL JUEGO AQUÍ
        Time.timeScale = 0f;

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textDisplay.text = "";
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;

            // ¡CAMBIO CLAVE! Usamos Realtime para que escriba aunque el juego esté en pausa
            yield return new WaitForSecondsRealtime(typingSpeed);
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

            // 2. REANUDAMOS EL JUEGO AQUÍ (Cuando ya no quedan más frases)
            Time.timeScale = 1f;
        }
    }
}