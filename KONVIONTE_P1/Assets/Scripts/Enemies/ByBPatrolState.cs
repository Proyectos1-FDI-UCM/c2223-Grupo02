using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByBPatrolState : State
{
    #region References

    private Transform _myTransform;
    private MovementComponent _myMovementComponent;

    #endregion

    #region Parameters

    [Tooltip("Tiempo de cada patrullaje")]
    [SerializeField] private float _routineTime;

    [Tooltip("Tiempo de parada entre cada patrullaje")]
    [SerializeField] private float _stopTime;

    [Header("Otros")]
    [Tooltip("Distancia del rayo que detecta la colisión con las paredes")]
    [SerializeField] private float _raycastWallDistance;

    [Tooltip("Distancia del rayo que detecta la colisión con el suelo")]
    [SerializeField] private float _raycastFloorDistance;

    [Tooltip("Distancia máxima que puede haber bajo el enemigo, para que baje")]
    [SerializeField] private float _maxDistance;

    [Tooltip("Objeto detector de suelo")]
    [SerializeField] private GameObject _floorDetector;

    #endregion

    #region Properties

    private float _currentPatrollTime;
    private LayerMask _playerLayerMask;
    private LayerMask _floorLayerMask;
    private Vector3 _movementDirection;
    private RaycastHit2D _wallRaycastInfo;
    private RaycastHit2D _floorRaycastInfo;

    #endregion

    public void OnEnter()
    {
        _currentPatrollTime = 0;
    }
    public void Tick()
    {
        //Restamos el tiempo
        _currentPatrollTime -= Time.deltaTime;

        //Si el tiempo llega a 0 (o es menor)
        if (_currentPatrollTime < 0)
        {
            //calculamos aleatoriamente la siguiente dirección
            _movementDirection = Vector3.right * Random.Range(-1, 2);//devuelve un aleatorio -1,0,1 

            //si es una parada, asignamos el tiempo de parada, sino, asignamos el tiempo de movimiento                                             
            _currentPatrollTime = _movementDirection == Vector3.zero ? _stopTime : _routineTime;

            //actualizamos la dirección en el movement
            _myMovementComponent.SetDirection(_movementDirection);
        }

        //Casteo del rayo de choque contra paredes
        _wallRaycastInfo = Physics2D.Raycast(_myTransform.position, _myTransform.right, _raycastWallDistance, _floorLayerMask);

        //Casteo del rayo de choque contra suelo
        _floorRaycastInfo = Physics2D.Raycast(_floorDetector.transform.position, -_floorDetector.transform.up, _raycastFloorDistance, _floorLayerMask);

        //Si he chocado con una pared o la distancia debajo de mí
        if (_wallRaycastInfo.transform != null || _floorRaycastInfo.distance == 0)
        {
            //cambiamos la direccion
            _movementDirection *= -1;

            //actualizamos el tiempo
            _currentPatrollTime = _routineTime;

            //actualizamos la direccion en el movement
            _myMovementComponent.SetDirection(_movementDirection);
        }
    }
    public void OnExit()
    {

    }

    //Constructor de la clase
    public ByBPatrolState (Transform myTransform, MovementComponent myMovementComponent)
    {
        _myTransform = myTransform;
        _myMovementComponent = myMovementComponent;
    }




}
