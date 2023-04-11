using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ESTE SCRIPT ES EL IMPORTANTE
 * Lo tiene el objeto que triggerea el dialogo
 * Llama al metodo que empieza el dialogo en el Dialogue Manager
 */

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue[] _dialogues;

    [SerializeField]
    private bool _increaseTutorialState = false;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(_dialogues);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == GameManager.Player)
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.Player)
        {
            if(_increaseTutorialState)
            {
                TutorialManager.Instance.IncreaseState();
            }

            Destroy(gameObject);
        }
    }
}
