using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollState : State
{
    Transform _myTransform;
    MovementComponent _myMovement;
    Transform _playerTransform;


    public void OnEnter()
    {

    }
    public void Tick()
    {

    }
    public void OnExit()
    {

    }

    public PatrollState(Transform myTransform, MovementComponent myMovement, Transform playerTransform)
    {
        _myTransform = myTransform;
        _myMovement = myMovement;
        _playerTransform = playerTransform;
    }

    //Constructor de la clase


}
