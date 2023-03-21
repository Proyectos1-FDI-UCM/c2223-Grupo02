using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyAttack : State
{

    #region References

    private Transform _playerTransform;
    private Transform _myTransform;
    private CombatController _myCombatController;
    private MovementComponent _myMovementComponent;
    private AtackComponent _myAttackComponent;

    #endregion

    #region Parameters
    private int _attackType;

    [SerializeField]
    private int _strongAttack;

    [SerializeField]
    private int _softAttack;

    private bool _attack;
    #endregion

    #region Properties

    #endregion

    public void OnEnter()
    {
        
    }

    public void Tick()
    {
        //Definir cuando va a atacar
        if (_attack)
        {
            //El tipo de ataque se elige de manera aleatoria con un random
            //Ataque fuerte
            if (_attackType == 1)  
            {
                //Daño del ataque más potente
                _myAttackComponent.SetDamage(_strongAttack);

                //Que mire al jugador para atacar (¿Al final esto sobra?)
                _myMovementComponent.SetDirection(GameManager.Instance._directionComponent.X_Directions(_playerTransform.position - _myTransform.position, 2));

                //Ataca
                _myCombatController.Atack(GameManager.Instance._directionComponent.X_Directions(_playerTransform.position - _myTransform.position, 4));
            }
            //Ataque área
            else
            {
                //Daño del ataque en área
                _myAttackComponent.SetDamage(_softAttack);

                //Que mire al jugador para atacar (¿Al final esto sobra?)
                _myMovementComponent.SetDirection(GameManager.Instance._directionComponent.X_Directions(_playerTransform.position - _myTransform.position, 2));

                //Ataca
                _myCombatController.Atack(GameManager.Instance._directionComponent.X_Directions(_playerTransform.position - _myTransform.position, 4));

                //Hacemos que mire al lado contrario
                _myMovementComponent.SetDirection(GameManager.Instance._directionComponent.X_Directions(_playerTransform.position - _myTransform.position, 2));

                //Volvemos a atacar
                _myCombatController.Atack(GameManager.Instance._directionComponent.X_Directions(_playerTransform.position - _myTransform.position, 4));
            }
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
