using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> _sentences;

    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        NextSentence();
    }

    public void NextSentence()
    {
        if(_sentences.Count == 0)
        {
            EndDialogue();
        }
        else
        {
            _sentences.Dequeue();

            string sentence = _sentences.Dequeue();
        }
    }

    private void EndDialogue()
    {

    }
}
