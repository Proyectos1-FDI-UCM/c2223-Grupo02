using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnemy : MonoBehaviour
{

    #region References

    CombatController _myCombatController;
    DirectionComponent _myDirectionComponent;
    [SerializeField]
    Transform _player;
    Transform _myTransform;

    #endregion

    #region Parameters

    [SerializeField]
    float _timeToAtack;
    #endregion

    #region Properties

    float _currentTime;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCombatController = GetComponent<CombatController>();
        
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _timeToAtack)
        {
            //Ataque al jugador            
            _myCombatController.Atack(GameManager.Instance._directionComponent.X_Directions(_player.localPosition - _myTransform.localPosition, 4));
            _currentTime = 0;
        }
    }
}
