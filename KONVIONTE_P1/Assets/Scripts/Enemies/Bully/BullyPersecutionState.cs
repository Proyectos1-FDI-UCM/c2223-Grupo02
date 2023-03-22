using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyPersecutionState : State
{
    #region References

    private Transform _playerTransform;
    private Transform _myTransform;
    private MovementComponent _myMovementComponent;
    private LifeComponent _myLifeComponent;

    #endregion

    #region Parameters

    #endregion

    #region Properties

    //Son parte de la transición
    private bool _alert;
    private float _distancePlayerEnemy;

    #endregion

    public void OnEnter()
    {
        
    }
    public void Tick()
    {
        //la detección y la vida se haría en la transición, no aquí

        //Rango del enemigo y detección del jugador (cambiar la forma de detectar al jugador) (si es esta habría que usar physics 2d) (Hacer por la diferencia entre transforms)
        //_alert = Physics.CheckSphere(_myTransform.position, _range, _playerMask);

        //Deteccta la distancia entre jugador y enemigo
        //_distancePlayerEnemy = (_myTransform.position - _playerTransform.position).magnitude;

        //if (_alert)
        {
            //Más de media vida -> perseguir jugador
            //if (_myLifeComponent.CurrentLife > (_myLifeComponent.MaxLife / 2))
            {
                //Si se gira raro poner (new Vector3(_playerTransform.position.x, _myTransform.position.y, _playerTransform.position.z))
                //_myTransform.LookAt(_playerTransform);
                //Movimiento de persecución
                _myMovementComponent.SetDirection(GameManager.DirectionComponent.X_Directions(_playerTransform.position - _myTransform.position, 2));

                /* Tambien podría hacerse con:
                 * _myTransform.position = Vector3.MoveTowards(_myTransform.position, _playerTransform, _speed * Time.deltaTime);
                 */
            }
        }


    }
    public void OnExit()
    {
        //¿Llamar al estado de ataque?
    }
}
