using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> _sentences;

    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
    }

}
