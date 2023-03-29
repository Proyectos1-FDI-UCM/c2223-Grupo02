using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCamera : MonoBehaviour
{
    #region Referencias
    [SerializeField] private Transform _myTargetTransform;
    private Transform _myTransform;
    #endregion
    #region Parametros
    [SerializeField] private float _interpolationSpeed;
    [SerializeField] private float _returnSpeed;
    [SerializeField] private float _zOffset;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _leftBoundarie;
    [SerializeField] private float _rightBoundarie;
    #endregion
    #region Propiedades
    private float _interpolation;
    private float _direction;
    private float _horizontalMovement;
    private float _targetPosition;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _interpolation = 0f;
        _myTransform.position = new Vector3(_myTargetTransform.position.x, _myTargetTransform.position.y + _yOffset, _zOffset);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowLogic();
    }

    /// <summary>
    /// Realiza toda la logica y movimiento de la camara
    /// </summary>
    private void FollowLogic()
    {
        // Si la camara puede seguir al jugador
        if (CanFollow())
        {
            _targetPosition = _myTargetTransform.position.x + (_xOffset * _direction);
            HorizontalFollow(_targetPosition, _interpolationSpeed);
        }
        // Si no le puede seguir, centra al jugador con el mismo método que antes
        else
        {
            _targetPosition = _myTargetTransform.position.x;
            HorizontalFollow(_targetPosition, _returnSpeed);
        }

        // Segumiento vertical
        VerticalFollow();
    }

    /// <summary>
    /// Habilita a la camara a seguir al jugador si este se mueve
    /// </summary>
    /// <param name="context"></param>
    private bool CanFollow()
    {
        // La camara se descentra si el jugador alcanza su velocidad maxima
        if(GameManager.Player.GetComponent<MovementComponent>().Speed 
            == GameManager.Player.GetComponent<MovementComponent>().MaxMovementSpeed)
        {
            // Reset de la interpolacion
            _interpolation = 0;

            // Direccion
            if((Vector2)GameManager.Player.GetComponent<MovementComponent>().Direction == Vector2.right)
            {
                _direction = 1;
            }
            else if ((Vector2)GameManager.Player.GetComponent<MovementComponent>().Direction == Vector2.left)
            {
                _direction = -1;
            }

            return true;
        }
        else
        {
            _interpolation = 0;

            return false;
        }
    }

    private void HorizontalFollow(float targetPosition, float speed)
    {
        // Se hace un lerpeo entre la pos del player y la pos objetivo
        // _direction cambia la direccion de movimiento de la camara segun la del jugador
        _horizontalMovement = Mathf.Lerp(_myTransform.position.x, targetPosition, speed * Time.deltaTime);
        // Se modifica la pos de la camara
        _myTransform.position = new Vector3(_horizontalMovement, _myTransform.position.y, _zOffset);
        // Aumenta la interpolacion
        _interpolation += speed * Time.deltaTime;
    }

    private void VerticalFollow()
    {
        if (_myTargetTransform.position.y >= _myTransform.position.y)
        {
            _myTransform.position = new Vector3(_myTransform.position.x, _myTargetTransform.position.y, _zOffset);
        }
        else if (_myTargetTransform.position.y < _myTransform.position.y - _yOffset)
        {
            _myTransform.position = new Vector3(_myTransform.position.x, _myTargetTransform.position.y + _yOffset, _zOffset);
        }
    }
}
