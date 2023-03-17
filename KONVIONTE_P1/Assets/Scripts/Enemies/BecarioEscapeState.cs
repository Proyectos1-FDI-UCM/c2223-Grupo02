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

    //Caja de detecci�n del jugador
    [SerializeField] Vector3 _detectionBoxSize;
    [SerializeField] Vector3 _detectionBoxOffset;

    [Tooltip("Tiempo en el que se actualiza la posici�n del jugador para el escape")]
    [SerializeField] private float _escapeTime;

    #endregion

    #region Properties

    private float _currentEscapeTime;

    #endregion

    public void OnEnter()
    {

    }
    public void Tick()
    {
        //Caja de detecci�n
        OurNamespace.Box.ShowBox(_detectionBoxSize, _detectionBoxOffset, _myTransform);

        //Disminuir el tiempo de escape
        _currentEscapeTime -= Time.deltaTime;

        if (_currentEscapeTime < 0)
        {
            //Seteo del time
            _currentEscapeTime = _escapeTime;

            //�Aumenta la velocidad en la hu�da?


            //Seteo de la direcci�n de movimiento
            //DUDA. �QU� SIGNIFICA EL 2 DEL FINAL?
            _myMovementComponent.SetDirection(GameManager.Instance._directionComponent.X_Directions(_myTransform.position - _playerTransform.position, 2));
        }

    }
    public void OnExit()
    { 
    
    }

    //Constructor de la clase
    public BecarioEscapeState(Transform myTransform, MovementComponent myMovementComponent, Transform playerTransform)
    {
        _myTransform = myTransform;
        _myMovementComponent = myMovementComponent;
        _playerTransform = playerTransform;
    }

}
