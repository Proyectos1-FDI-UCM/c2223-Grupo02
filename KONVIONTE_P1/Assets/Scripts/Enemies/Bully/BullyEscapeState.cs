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

    private float _escapeTime;
    private float _stopEscapeTime;

    #endregion

    #region Properties

    private float _currentEscapeTime;
    private float _currentStopTime;

    #endregion

    public void OnEnter()
    {
        _currentEscapeTime = 0;
        _currentStopTime = _stopEscapeTime;
    }
    public void Tick()
    {
       //Disminuir el tiempo de escape
       _currentEscapeTime -= Time.deltaTime;

       if (_currentEscapeTime < 0)
       {
            //Seteo del time
            _currentEscapeTime = _escapeTime;

            //Seteo de la dirección del movimiento y aumento de velocidad
            _myMovementComponent.SetDirection(GameManager.DirectionComponent.X_Directions(2*(_myTransform.position - _playerTransform.position), 2));

            //Es necesario disminuirlo de nuevo? no se puede hacer debajo otro if en vez de dentro?
            //Disminuimos el tiempo hasta la próxima parada
            _currentEscapeTime -= Time.deltaTime;

            if (_currentStopTime < 0)
            {
                _currentStopTime = _stopEscapeTime; 
                //No se moverá durante ese tiempo
                _myMovementComponent.SetDirection(GameManager.DirectionComponent.X_Directions(0 * (_myTransform.position - _playerTransform.position), 2))
            }

       }
            
    }
    public void OnExit()
    {

    }

    public BullyEscapeState(BullyMachine myMachine)
    {
        _playerTransform = myMachine.PlayerTransform;
        _myTransform = myMachine.MyTransform;
        _myMovementComponent = myMachine.MyMovementComponent;


    }
}
