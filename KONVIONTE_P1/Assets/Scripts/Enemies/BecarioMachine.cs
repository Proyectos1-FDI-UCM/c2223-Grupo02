using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BecarioMachine : StateMachine
{
    Transform _myTransform;
    MovementComponent _myMovement;
    Transform _playerTransform;






    PatrollState patrollState;


    Transition detectaEnemigo;
    Func<bool> _detectaEnemigo;


    // Start is called before the first frame update
    void Start()
    {
        _detectaEnemigo = () => DetectaEnemigo(_myTransform);

        patrollState = new PatrollState(_myTransform, _myMovement, _playerTransform);



        //InicializaTransicion(patrollState,,);
    }

    // Update is called once per frame
    void Update()
    {
        Tick();
    }


    public bool DetectaEnemigo(Transform a)
    {
        return true;
    }
}
