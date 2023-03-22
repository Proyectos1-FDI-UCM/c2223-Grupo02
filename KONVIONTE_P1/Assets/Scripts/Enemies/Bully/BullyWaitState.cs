using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyWaitState : State
{
    #region References

    private Transform _playerTransform;
    private Transform _myTransform;
    private MovementComponent _myMovementComponent;
    

    #endregion

    #region Parameters

    #endregion

    #region Properties

    #endregion

    public void OnEnter()
    {
       
    }
    public void Tick()
    {
       //Mitad/Cuarto de vida -> Espera mirando al jugador
            
       _myTransform.LookAt(_playerTransform);
    }
    public void OnExit()
    {

    }
}
