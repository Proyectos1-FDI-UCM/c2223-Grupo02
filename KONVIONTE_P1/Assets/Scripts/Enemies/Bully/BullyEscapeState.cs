using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyEscapeState : State
{
    #region References

    private Transform _playerTransform;
    private Transform _myTransform;
    private MovementComponent _myMovementComponent;

    #endregion

    #region Parameters

    private float _speed;

    #endregion

    #region Properties

    private float _currentEscapeTime;

    #endregion

    public void OnEnter()
    {
        _currentEscapeTime = 0;
    }
    public void Tick()
    {
            //Menos de cuarto de vida huir
            //Disminuir el tiempo de escape
            _currentEscapeTime -= Time.deltaTime;

            if (_currentEscapeTime < 0)
            {
               //Seteo del time
               _currentEscapeTime = _speed;

               //¿Aumenta la velocidad en la huída?


               //Seteo de la dirección de movimiento
               //DUDA. ¿QUÉ SIGNIFICA EL 2 DEL FINAL?
               _myMovementComponent.SetDirection(GameManager.DirectionComponent.X_Directions(_myTransform.position - _playerTransform.position, 2));
            }
            
    }
    public void OnExit()
    {

    }
}
