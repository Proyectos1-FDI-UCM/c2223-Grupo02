using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinterAttackState : State
{
    #region References
    private Transform _playerTransform;
    private Transform _myTransform;
    MovementComponent _myMovementComponent;
    CombatController _myCombatController;
    AtackComponent _myAttackComponent;
    Animator _myAnimator;
    #endregion
    #region Parameters
    private float _attackTime;

    #endregion
    #region Properties
    private float _currentAttackTime;

    private int _attackType;

    private float _originalMaxSpeed;
    #endregion

    public void OnEnter()
    {
        _currentAttackTime = 0;

        //Fijarse en que ese sea el nombre del bool en la animación
        _myAnimator.SetBool("AttackState", true);

        _originalMaxSpeed = _myMovementComponent.MaxMovementSpeed;
        //Hacemos que se quede quieto mientras pega
        _myMovementComponent.SetMaxSpeed(0);
    }

    public void Tick()
    {
        _currentAttackTime -= Time.deltaTime;
        if (_currentAttackTime < 0)
        {
            
            //Que mire al jugador para atacar (¿Al final esto sobra?)
            _myMovementComponent.SetDirection(GameManager.DirectionComponent.X_Directions(_playerTransform.position - _myTransform.position, 2));

            //Ataca
            _myCombatController.Atack(GameManager.DirectionComponent.X_Directions(_playerTransform.position - _myTransform.position, 4));
            _currentAttackTime = _attackTime;
            Debug.Log("AtaqueSprinter");
        }
    }

    public void OnExit()
    {
        _myMovementComponent.SetMaxSpeed(_originalMaxSpeed);
        _myAnimator.SetBool("AttackState", false);
        _myAnimator.SetBool("IsAttacking", false);
    }
    public SprinterAttackState(SprinterMachine myMachine)
    {
        _playerTransform = myMachine.PlayerTransform;
        _myTransform = myMachine.MyTransform;
        _myMovementComponent = myMachine.MyMovementComponent;
        _myCombatController = myMachine.MyCombatController;
        _myAttackComponent = myMachine.MyAttackComponent;
        _myAnimator = myMachine.MyAnimator;

        _attackTime = myMachine.AttackTime;
    }
}
