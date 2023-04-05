using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class DashComponent : MonoBehaviour
{
    #region References
    private Transform _myTransform;
    private MovementComponent _movement;
    private DirectionComponent _direction;
    private Mouse _mouse;
    private Gamepad _gamepad;
    private AtackComponent _attack;
    #endregion
    #region Parameters
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashTime;
    #endregion
    #region Properties
    private float _dashSpeed;
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
        _direction = GameManager.DirectionComponent;
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
            PerformDash(_dashDistance);

            // Si eres el enemigo y has terminado el dash haces el daño de la estela
            if (_time <= 0 && gameObject.GetComponent<ParryComponent>() == null)
            {
                DashDamage();
            }

            Debug.Log(_putoDasheo);
        }
    }

    #region Methods
    /// <summary>
    /// Habilita poder dashear
    /// </summary>
    /// <param name="canDash"></param>
    /// <returns></returns>
    public void Dashing(bool canDash)
    {
        _putoDasheo = canDash;
        TryDash();
    }

    /// <summary>
    /// Ejecuta el cambio del transform para el dash y el timer
    /// </summary>
    private void PerformDash(float distance)
    {
        // Immortalidad
        // GameManager.Instance.InmortalityPlayer(_putoDasheo);

        // Aumento puntual de velocidad
        _dashSpeed = _movement.Speed + (distance / _dashTime);

        // Modificaciónd de la posición
        _myTransform.position += _dashSpeed * Time.fixedDeltaTime * _dashDirection;

        // Timer
        DashTimer(); 

        Debug.Log("tuvieja");
    }
   
    /// <summary>
    /// Cuenta el tiempo del dash y resetea todo
    /// </summary>
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
        if (gameObject.GetComponent<ParryComponent>() != null)
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
        else
        {
            _dashDirection = (GameManager.Player.transform.position - _myTransform.position).normalized;
        }  
    }

    /// <summary>
    /// Ejecuta toda la logica del dash
    /// </summary>
    private void TryDash()
    {
        // Coge la direccion
        DashDirection();

        // Comprueba que no se choca
        _rayDistance = Physics2D.Raycast(_myTransform.position, (Vector2)_dashDirection, _dashDistance, _floorMask).distance;

        // Realiza el dash
        if(_rayDistance != 0)
        {
            _dashDistance -= _rayDistance;
        }
    }

    /// <summary>
    /// Llama al daño 
    /// </summary>
    private void DashDamage()
    {
        //_attack.TryAplyDamage();
    }
    #endregion
}
