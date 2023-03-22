using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// SCRIPT PERTENECIENTE AL DIALOGUE MANAGER EN ESCENA

public class DialogueManager : MonoBehaviour
{
    #region References
    [SerializeField] private Text _dialogueName;
    [SerializeField] private Text _dialogueText;
    #endregion
    #region Properties
    private Queue<string> _sentences;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
    }

    /// <summary>
    /// Para empezar el dialogo se llama desde el DialogueTriggger
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartDialogue(Dialogue dialogue)
    {
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

    /// <summary>
    /// Este metodo lo llama el trigger en pantalla (un boton p.ej)
    /// </summary>
    public void NextSentence()
    {
        // Si no quedan frases
        if(_sentences.Count == 0)
        {
            // Se termina el dialogo
            EndDialogue();
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

    }
}
