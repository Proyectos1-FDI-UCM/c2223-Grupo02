using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BecarioEscapeState : State
{
    #region References

    private Transform _playerTransform;
    private Transform _myTransform;
    private MovementComponent _myMovementComponent;

    #endregion

    #region Parameters
  
    private float _escapeTime;

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
        //Disminuir el tiempo de escape
        _currentEscapeTime -= Time.deltaTime;

        if (_currentEscapeTime < 0)
        {
            //Seteo del time
            _currentEscapeTime = _escapeTime;

            //¿Aumenta la velocidad en la huída?


            //Seteo de la dirección de movimiento
            //DUDA. ¿QUÉ SIGNIFICA EL 2 DEL FINAL?
            _myMovementComponent.SetDirection(GameManager.Instance._directionComponent.X_Directions(_myTransform.position - _playerTransform.position, 2));
        }

    }
    public void OnExit()
    { 
    
    }

    //Constructor de la clase
    public BecarioEscapeState(BecarioMachine myMachine)
    {
        _myTransform = myMachine.MyTransform;
        _myMovementComponent = myMachine.MyMovementComponent;
        _playerTransform = myMachine.PlayerTransform;

        _escapeTime = myMachine.EscapeTime;

        _currentEscapeTime = 0;
    }

}
