using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bdetectsplayer : State
{

    #region References

    private Transform _playerTransform;
    private Transform _myTransform;
    private MovementComponent _myMovementComponent;
    private LifeComponent _myLifeComponent;

    #endregion

    #region Parameters

    [SerializeField] private float _range;
    [SerializeField] private LayerMask _playerMask;

    private float _speed;
    #endregion

    #region Properties

    private bool _alert;
    private float _currentEscapeTime;
    #endregion

    public void OnEnter()
    {
        _currentEscapeTime = 0;
    }
    public void Tick()
    {
        //Rango del enemigo y detección del jugador
        _alert = Physics.CheckSphere(_myTransform.position, _range, _playerMask);

        //Más de media vida y _alert = true -> perseguir jugador
        if ((_myLifeComponent.CurrentLife > (_myLifeComponent.MaxLife / 2)) && _alert)
        {
            //Si se gira raro poner (new Vector3(_playerTransform.position.x, _myTransform.position.y, _playerTransform.position.z))
            _myTransform.LookAt(_playerTransform);
            //Movimiento de persecución
            _myMovementComponent.SetDirection(GameManager.Instance._directionComponent.X_Directions(_playerTransform.position - _myTransform.position, 2));

            /* Tambien podría hacerse con:
             * _myTransform.position = Vector3.MoveTowards(_myTransform.position, _playerTransform, _speed * Time.deltaTime);
             */
        }
        

        //Mitad/Cuarto de vida -> Espera mirando al jugador
        if ((_myLifeComponent.MaxLife / 4) < _myLifeComponent.CurrentLife && _myLifeComponent.CurrentLife < (_myLifeComponent.MaxLife / 2) && _alert)
        {
            _myTransform.LookAt(_playerTransform);
        }
  
        //Menos de cuarto de vida huir
        if ((_myLifeComponent.CurrentLife < _myLifeComponent.MaxLife / 4) && _alert)
        {
            //Disminuir el tiempo de escape
            _currentEscapeTime -= Time.deltaTime;

            if (_currentEscapeTime < 0)
            {
                //Seteo del time
                _currentEscapeTime = _speed;

                //¿Aumenta la velocidad en la huída?


                //Seteo de la dirección de movimiento
                //DUDA. ¿QUÉ SIGNIFICA EL 2 DEL FINAL?
                _myMovementComponent.SetDirection(GameManager.Instance._directionComponent.X_Directions(_myTransform.position - _playerTransform.position, 2));
            }
        }

    }
    public void OnExit()
    {

    }

}
