using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//ESTE SCRIPT DEBE IR ATACHADO AL JUGADOR
//NOTA: es posible que haya que migrar la rotacion al movementComponent y tambien a lo mejor es util hacer un script solo para el input
public class PlayerAnimationManager : MonoBehaviour
{
    
    private Transform _myTransform;
    private Animator _myAnimator;
    private PlayerInputActions _playerInputActions;
    private CombatController _playerCombatController;

    private Vector2 _horizontalVector;
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myAnimator = GetComponent<Animator>();     
        _playerCombatController = GetComponent<CombatController>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {        
        _horizontalVector = _playerInputActions.Player.HorizontalMove.ReadValue<Vector2>();

        //giramos al jugador segun hacia donde nos estemos moviendo
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
        if (callbackContext.performed)
        {
            _playerCombatController.Atack(_playerInputActions.Player.VerticalAtack.ReadValue<Vector2>());
        }
    }
    public void SetIsAttakingFalse()
    {
        _myAnimator.SetBool("IsAttaking", false);
    }
}
