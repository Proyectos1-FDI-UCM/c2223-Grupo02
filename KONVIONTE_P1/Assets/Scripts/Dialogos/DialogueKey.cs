using UnityEngine;
using UnityEngine.InputSystem;
public class DialogueKey : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;

    [SerializeField] private GameObject _image;

    private bool _inZone = false;

    public void TriggerDialogue(InputAction.CallbackContext context)
    {
        if (context.started && _inZone) 
        {
            DialogueManager.Instance.StartDialogue(_dialogue);            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.Player)
        {
            _inZone = true;
            _image.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.Player)
        {
            _inZone = false;
            _image.SetActive(false);
        }
    }    
}

