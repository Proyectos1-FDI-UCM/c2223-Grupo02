using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MovementComponent : MonoBehaviour
{
    
    #region Refences  
    private PlayerInputActions _playerInputActions;
    
    private Transform _myTransform;
    #endregion
    #region Properties
    private Vector3 _directionVector;
    private Vector3 _lastDirection;
    
    private float _speed = 0f;
    #endregion

    #region Parameters  
    [SerializeField] private float _maxMovementSpeed;   
    
    [Header("Tiempos")]
    [SerializeField] private float _accelerationTime;
    [SerializeField] private float _deccelerationTime;
    #endregion

    
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();            
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        _directionVector = _playerInputActions.Player.HorizontalMove.ReadValue<Vector2>();

        if(_directionVector.magnitude != 0)
        {
            //ACELERACION
            if(_speed < _maxMovementSpeed)
            {
                _speed += Time.fixedDeltaTime*_maxMovementSpeed / _accelerationTime;
            }
            
            _lastDirection = _directionVector;
        }
        else
        {
            //DECELERACION
            if (_speed > 0)
            {
                _speed -= Time.fixedDeltaTime * _maxMovementSpeed / _deccelerationTime;
            }
        }
         _speed = Mathf.Clamp(_speed, 0f, _maxMovementSpeed);
        _myTransform.position += _speed * _lastDirection * Time.fixedDeltaTime;
    }
}
