using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

// SCRIPT PERTENECIENTE AL DIALOGUE MANAGER EN ESCENA

public class DialogueManager : MonoBehaviour
{
    #region References
    [SerializeField] private TMP_Text _dialogueName;
    [SerializeField] private TMP_Text _dialogueText;

    
    #endregion

    #region Properties
    private Queue<string> _sentences;
    private Queue<Dialogue> _dialogues;

    public static DialogueManager Instance { get; private set; }
    #endregion

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
        _dialogues = new Queue<Dialogue>();
    }

    /// <summary>
    /// Para empezar el dialogo se llama desde el DialogueTriggger
    /// </summary>
    /// <param name="dialogue"></param>
    private void StartDialogue(Dialogue dialogue)
    {
        //activa la UI
        GameManager.Instance.UIManager.SetDialogueUI(true);

        // Cambia el nombre de quien habla
        _dialogueName.text = dialogue.name;

        // Elimina todos los strings de la cola
        _sentences.Clear();

        // Agregamos todas las frases a la cola
        foreach(string sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        // Primera frase
        NextSentence();
    }

    public void StartDialogue(Dialogue[] dialogues)
    {
        //limpiamos la cola
        _dialogues.Clear();

        //añadimos los dialogos a la cola
        foreach (Dialogue dialog in dialogues)
        {
            _dialogues.Enqueue(dialog);
        }

        //empezamos el primer dialogo
        Dialogue dialogue = _dialogues.Dequeue();

        StartDialogue(dialogue);

        // Desactivar el input y parar el juego
        GameManager.Instance.InputOff();
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Este metodo lo llama el trigger en pantalla (un boton p.ej)
    /// </summary>
    public void NextSentence()
    {     
        // Si no quedan frases
        if (_sentences.Count == 0)
        {
            //si no quedan dialogos en la cola
            if(_dialogues.Count == 0)
            {
                // Se termina el dialogo
                EndDialogue();
            }
            else
            {
                //empezar el siguiente dialogo en cola
                StartDialogue(_dialogues.Dequeue());
            }            
        }
        else
        {
            // Dequeue devuelve el primer string y lo quita de la cola
            string sentence = _sentences.Dequeue();

            // Se pone en pantalla
            _dialogueText.text = sentence;
        }
    }

    private void EndDialogue()
    {
        //desactiva la ui
        GameManager.Instance.UIManager.SetDialogueUI(false);

        //activa el input y sigue el juego
        GameManager.Instance.InputOn();
        Time.timeScale = 1.0f;
    }

}
