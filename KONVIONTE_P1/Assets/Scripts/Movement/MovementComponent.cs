using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
    private Vector2 _movementSpeedVector;
    #endregion
    #region Parameters
    private float _speed = 0f;
    [SerializeField] private float _maxMovementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speedStabilizer;
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

    // Update is called once per frame
    void Update()
    {
        _movementSpeedVector = _playerInputActions.Player.HorizontalMove.ReadValue<Vector2>();
        _myCharacterController.Move(_movementSpeedVector * Time.deltaTime * (_speed / _speedStabilizer));
        Debug.Log(_speed);
        // Si mantenemos la tecla (si el vector velocidad no es cero)
        if (_movementSpeedVector != Vector2.zero && _speed < _maxMovementSpeed)
        {
            _speed++; // Aumentamos la velocidad por frame para provocar sensacion de aceleracion hasta llegar a la máxima velocidad
        }
        // Si soltamos la tecla (esto hay que cambiarlo porque solo funciona para teclado)
        if (Keyboard.current.dKey.wasReleasedThisFrame || Keyboard.current.aKey.wasReleasedThisFrame)
        {
            _speed = 0f; // La velocidad vuelve a cero para poder volver a acelerar
        }
    }
}
