using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MovementComponent : MonoBehaviour
{      
    #region Properties
    private Transform _myTransform;

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
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {        
        //Llamamos al move solo cuando es necesario para optimizar costes
        if(_directionVector != Vector3.zero || _speed != 0) Move();
    }
    /// <summary>
    /// Mueve al jugador en el fixed update
    /// </summary>
    private void Move()
    {
        //si nos estamos moviemdo
        if (_directionVector.magnitude != 0)
        {
            //ACELERACION

            //Si no hemos alcanzado la velocidad maxima, le aplicamos la aceleracion correspondiente
            if (_speed < _maxMovementSpeed)
            {
                //En el caso de que el tiempo de aceleracion sea 0, para evitar error, se setea la velocidad a la maxima instantaneamente
                if(_accelerationTime != 0)_speed += Time.fixedDeltaTime * _maxMovementSpeed / _accelerationTime;
                else _speed = _maxMovementSpeed;
            }
            //actualizamos la ultima direccion a la que nos hemos movido
            _lastDirection = _directionVector;
        }
        else//si hemos dejado de movernos
        {
            //DECELERACION

            //Si no hemos parado del todo, le aplicamos la deceleracion correspondiente
            if (_speed > 0)
            {
                //En el caso de que el tiempo de deceleracion sea 0, para evitar error, se setea la velocidad a 0 instantaneamente
                if (_deccelerationTime != 0) _speed -= Time.fixedDeltaTime * _maxMovementSpeed / _deccelerationTime;
                else _speed = 0;
            }
        }

        //forzamos que la velocidad esté en el intervalo [0,_maxMovementSpeed] y despues nos movemos
        _speed = Mathf.Clamp(_speed, 0f, _maxMovementSpeed);
        _myTransform.position += _speed * _lastDirection * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Setea el _directionVector, que es el vector que indica la direccion del movimiento
    /// </summary>
    /// <param name="direction"></param>
    public void SetDirection(Vector3 direction)
    {
        _directionVector = direction;
    }
}
