using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyAttack : State
{

    #region References

    private Transform _playerTransform;
    private Transform _myTransform;
    private MovementComponent _myMovementComponent;
    private CombatController _myCombatController;
    private AtackComponent _myAttackComponent;
    private Animator _myAnimator;

    #endregion

    #region Parameters
    [SerializeField]
    private int _strongAttack;

    [SerializeField]
    private int _softAttack;

    [SerializeField]
    private float _attackTime; //Tiempo de espera entre ataques
    #endregion

    #region Properties

    private float _currentAttackTime;

    private int _attackType;

    #endregion

    public void OnEnter()
    {
        //Hacemos que se reinicie el tiempo de ataque al principio
        _currentAttackTime = _attackTime;

        //Fijarse en que ese sea el nombre del bool en la animación
        _myAnimator.SetBool("AttackState", true);

        //Hacemos que se quede quieto mientras pega
        _myMovementComponent.SetMaxSpeed(0);
    }

    public void Tick()
    {
        //Disminuimos el tiempo que queda para atacar
        _currentAttackTime -= Time.deltaTime;
        if (_currentAttackTime < 0)
        {
            //El tipo de ataque se elige de manera aleatoria con un random

            _attackTime = Random.Range(0, 1);
            //Ataque en área
            if (_attackType == 1)  
            {
                //Daño del ataque en área
                _myAttackComponent.SetDamage(_softAttack);

                //Hacemos que mire al lado contrario al jugador
                _myMovementComponent.SetDirection(GameManager.Instance._directionComponent.X_Directions(_playerTransform.position - _myTransform.position, 2));

                //Atacamos
                _myCombatController.Atack(GameManager.Instance._directionComponent.X_Directions(_playerTransform.position - _myTransform.position, 4));

            }
            //Ataque fuerte
            else { _myAttackComponent.SetDamage(_strongAttack); }

            //Que mire al jugador para atacar (¿Al final esto sobra?)
            _myMovementComponent.SetDirection(GameManager.Instance._directionComponent.X_Directions(_playerTransform.position - _myTransform.position, 2));

            //Ataca
            _myCombatController.Atack(GameManager.Instance._directionComponent.X_Directions(_playerTransform.position - _myTransform.position, 4));

        }
        
    }

    public void OnExit()
    {

    }

    //Constructora de la clase
    public BullyAttack (BullyMachine mymachine)
    {
       
    }
}
