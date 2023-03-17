using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class DashComponent : MonoBehaviour
{
    #region References
    private MovementComponent _movement;
    private DirectionComponent _direction;
    private Mouse _mouse;
    private Gamepad _gamepad;
    #endregion
    #region Parameters
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashTime;
    #endregion
    #region Properties
    private float _dashSpeed;
    private Transform _myTransform;
    private bool _putoDasheo;
    private float _time;
    private Vector3 _dashDirection;
    private float _rayDistance;
    private LayerMask _floorMask;
    private float _maxDashDistance;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _mouse = Mouse.current;
        _gamepad = Gamepad.current;
        _myTransform = transform;
        _movement = GetComponent<MovementComponent>();
        _direction = GetComponent<DirectionComponent>();
        _floorMask = LayerMask.GetMask("Floor");
        _maxDashDistance = _dashDistance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Si puedes dashear
        if (_putoDasheo)
        {
            // Dash
            TryDash();

            Debug.Log(_putoDasheo);
        }
    }

    #region Methods
    /// <summary>
    /// Habilita poder dashear desde el Game Manager
    /// </summary>
    /// <param name="canDash"></param>
    /// <returns></returns>
    public void CanDash(bool canDash)
    {
        _putoDasheo = canDash; 
    }

    /// <summary>
    /// Realiza el cambio del transform para el dash
    /// </summary>
    private void PerformDash(float distance)
    {
        // Immortalidad
        // GameManager.Instance.InmortalityPlayer(_putoDasheo);

        // Aumento puntual de velocidad
        _dashSpeed = (_movement.Speed + (distance / _dashTime));

        // Modificaciónd de la posición
        _myTransform.position += _dashSpeed * Time.fixedDeltaTime * _dashDirection;

        // Timer
        DashTimer(); 

        Debug.Log("tuvieja");
    }

    private void DashTimer()
    {
        // Cuando pase el tiempo del dash paras
        if (_time >= _dashTime)
        {
            // Reset de propiedades e immortalidad
            _putoDasheo = false;
            _time = 0;
            _dashDistance = _maxDashDistance;
            // GameManager.Instance.InmortalityPlayer(_putoDasheo);
        }
        else
        {
            _time += Time.fixedDeltaTime; // Contador
        }
    }

    /// <summary>
    /// Setea la dirección del dash en funcion de la posición del ratón o el joystick
    /// </summary>
    private void DashDirection()
    {
        // Si hay gamepad leemos el joystick
        if (_gamepad != null)
        {
            _dashDirection = _direction.X_Directions(_gamepad.rightStick.ReadValue(), 8);
        }
        else // Si hay ratón cogemos su posición
        {
            _dashDirection = _direction.X_Directions(Camera.main.ScreenToWorldPoint(_mouse.position.ReadValue()) - _myTransform.position, 8);
        }
    }

    private void TryDash()
    {
        DashDirection();

        _rayDistance = Physics2D.Raycast(_myTransform.position, (Vector2)_dashDirection, _dashDistance, _floorMask).distance;

        if(_rayDistance == 0)
        {
            PerformDash(_dashDistance);
        }
        else
        {
            _dashDistance -= _rayDistance;
            PerformDash(_dashDistance);
        }
    }
    #endregion
}
