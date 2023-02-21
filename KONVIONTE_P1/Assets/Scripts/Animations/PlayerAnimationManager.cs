using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//ESTE SCRIPT DEBE IR ATACHADO AL JUGADOR
//En lugar de gestionar las animaciones en cada script(salto,movimiento,combate...) lo gestionamos todo desde otro a parte
//para tenerlo mas organizado.
//"susceptible a cambios"
public class PlayerAnimationManager : MonoBehaviour
{
    
    private Transform _myTransform;
    private Animator _myAnimator;
    private SpriteRenderer _mySpriteRenderer;
    private PlayerInputActions _playerInputActions;

    private Vector2 _horizontalVector;
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myAnimator = GetComponent<Animator>();
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
        _horizontalVector = _playerInputActions.Player.HorizontalMove.ReadValue<Vector2>();
        if (_horizontalVector != Vector2.zero)
        {
            _myAnimator.SetBool("IsMoving", true);
            if(_horizontalVector.x > 0 &&  _myTransform.localEulerAngles.y != 0)
            {
                _myTransform.localEulerAngles = new Vector3(_myTransform.localRotation.x, 0, _myTransform.localRotation.z);
            }
            else if (_horizontalVector.x < 0 && _myTransform.localEulerAngles.y != 180)
            {
                _myTransform.localEulerAngles = new Vector3(_myTransform.localRotation.x, 180, _myTransform.localRotation.z);
            }
        }
        else
        {
            _myAnimator.SetBool("IsMoving", false);
        }
        


        
    }
    public void AtackAnim(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && !_myAnimator.GetBool("IsAttaking"))
        {
            _myAnimator.SetBool("IsAttaking", true);
        }
    }
    public void SetIsAttakingFalse()
    {
        _myAnimator.SetBool("IsAttaking", false);

    }
}
