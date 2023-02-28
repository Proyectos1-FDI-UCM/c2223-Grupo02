using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovementComponent : MonoBehaviour
{
    /*
     *_distanceToPlayer: Comparar distancia en X, simplemente, pq a lo mejor Raycast no necesario
     * Animator;
     * Para flip direcction, cambiar scale.x entre 1 y -1 Transform.localScale = new Vector3 (-1,1,0) en la derecha del todo y new V3 (1,1,0) en la izqda del todo
     * Vector 3 right, left, zero
     */

    #region References

    //Moveremos al enemigo por tranform
    private Transform _myTransform;

    //Referencia al MovementComponent
    private MovementComponent _movementComponent;

    #endregion

    #region Properties

    //Distancia hasta el jugador
    private Vector3 _distanceToTarget;

    //Controla si está persiguiendo o no al jugador
    private bool _isAttacking;

    //Dirección del movimiento (True a derecha, false a izquierda)
    private bool _rightDirection = true;

    //Reloj
    private float _time;

    //Dirección de movimiento
    private Vector3 _direction;

    private bool _moving;


    #endregion

    #region Parameters

    [Tooltip("Jugador al que perseguir")]
    [SerializeField] private GameObject _target;

    [Tooltip("Velocidad de patrulla")]
    [SerializeField] private float _speed;

    [Tooltip("Velocidad de persecución")]
    [SerializeField] private float _maxSpeed;

    [Tooltip("Tiempo de cada patrullaje")]
    [SerializeField] private float _routineTime;

    [Tooltip("Tiempo de parada entre cada patrullaje")]
    [SerializeField] private float _stopTime;

    #endregion

    #region Methods

    private void DetectPLayer()
    {
        //if (_target.transform.position <= _distanceToTarget)
        //{

        //}

    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el componente del movimiento para acceder a la dirección
        _movementComponent = GetComponent<MovementComponent>();

        _myTransform = transform;

        _time = _routineTime;

        _direction = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        //Va a derecha
        _movementComponent.SetDirection(_direction);
          
        //Un tiempo determinado
        _time -= Time.deltaTime;

        //Se podría añadir otra condición para cambiar de dirección
        if (_time < 0) 
        {
            _direction = Vector3.right * Random.Range(-1, 2);

            if(_direction == Vector3.zero)
            {
                _time = _stopTime;
            }
            else
            {
                _time = _routineTime;
            }

        }  
    }
}
