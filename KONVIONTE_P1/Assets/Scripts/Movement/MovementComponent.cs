using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    private CharacterController _myCharacterController;
    private PlayerInputActions _playerInputActions;
    private PlayerInput _playerInput;
    private Vector2 _movementVector;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;

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

    // Update is called once per frame
    void Update()
    {
        _movementVector = _playerInputActions.Player.HorizontalMove.ReadValue<Vector2>();
        _myCharacterController.Move(_movementVector * Time.deltaTime * _movementSpeed);
    }
}
