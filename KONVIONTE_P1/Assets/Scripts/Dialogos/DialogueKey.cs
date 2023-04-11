using UnityEngine;
using UnityEngine.InputSystem;
public class DialogueKey : MonoBehaviour
{
    [SerializeField] private Dialogue[] _dialogues;

    [SerializeField] private GameObject _image;

    [SerializeField]
    private bool _increaseTutorialState = false;

    private bool _inZone = false;

    private void Start()
    {
        DialogueManager.Instance.AddCartel(this);
        _image.SetActive(false);
    }

    public void TriggerDialogue()
    {
        if (_inZone) 
        {
            DialogueManager.Instance.StartDialogue(_dialogues);            
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

            if (_increaseTutorialState)
            {
                TutorialManager.Instance.IncreaseState();
            }

        }
    }    
}

