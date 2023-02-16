using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{

    #region Refences
    private CharacterController _myCharacterController;
    private PlayerInputActions _playerInputActions;
    private PlayerInput _playerInput;
    #endregion
    #region Properties
    private Vector2 _movementVector;
    #endregion
    #region Parameters
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    #endregion

    /*public void Jump()  
    {
        _myCharacterController.Move(Vector2.up * Time.deltaTime * _jumpForce);
    }*/

    // Start is called before the first frame update
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInput = GetComponent<PlayerInput>();
    }
    private void FixedUpdate()
    {
        //Movido al FixedUpdate para 
        _movementVector = _playerInputActions.Player.HorizontalMove.ReadValue<Vector2>();
        _myCharacterController.Move(_movementVector * Time.deltaTime * _movementSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
