using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
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
    #endregion
    #region Propiedades
    private bool _canFollow;
    private float _interpolation;
    private float _direction;
    private float _horizontalMovement;
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
        // Si la camara puede seguir al jugador
        if (_canFollow)
        {
            // Se hace un lerpeo entre la pos del player y la pos objetivo
            // _direction cambia la direccion de movimiento de la camara segun la del jugador
            _horizontalMovement = Mathf.Lerp(_myTransform.position.x, _myTargetTransform.position.x + (_xOffset * _direction), _interpolation);
            // Se modifica la pos de la camara
            _myTransform.position = new Vector3(_horizontalMovement, _myTransform.position.y, _zOffset);
            // Aumenta la interpolacion
            _interpolation += _interpolationSpeed * Time.deltaTime;
        }
        // Si no le puede seguir, centra al jugador con el mismo método que antes
        else
        {
            _horizontalMovement = Mathf.Lerp(_myTransform.position.x, _myTargetTransform.position.x, _interpolation);
            _myTransform.position = new Vector3(_horizontalMovement, _myTransform.position.y, _zOffset);
            _interpolation += _returnSpeed * Time.deltaTime;
        }

        if (_myTargetTransform.position.y >= _myTransform.position.y)
        {
            _myTransform.position = new Vector3(_myTransform.position.x, _myTargetTransform.position.y, _zOffset);
        }
        else if(_myTargetTransform.position.y < _myTransform.position.y - _yOffset)
        {
            _myTransform.position = new Vector3(_myTransform.position.x, _myTargetTransform.position.y + _yOffset, _zOffset);
        }
    }

    /// <summary>
    /// Habilita a la camara a seguir al jugador si este se mueve
    /// </summary>
    /// <param name="context"></param>
    public void CanFollow(bool performed, bool canceled,Vector2 directon)
    {
        // Si el jugador se mueve
        if (performed)
        {
            // Habilitamos el movimiento de la camara
            _canFollow = true;
            // Resetamos la interpolacion
            _interpolation = 0;
            // Según la direccion de movimiento la camara se mueve hacia derecha o izquierda
            if(directon == Vector2.left)
            {
                _direction = -1f;
            }
            else
            {
                _direction = 1f;
            }
        }
        // Si el jugador para
        else if (canceled)
        {
            // La camara deja de seguirle
            _canFollow = false;
            // Reseteamos la interpolacion
            _interpolation = 0;
        }
    }
}
