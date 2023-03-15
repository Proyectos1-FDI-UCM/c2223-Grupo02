using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class DashComponent : MonoBehaviour
{
    #region References
    private MovementComponent _movement;
    #endregion
    #region Parameters
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashTime;
    #endregion
    #region Properties
    private float _dashSpeed;
    private Transform _myTransform;
    private bool _putoDasheo;
    private float _time;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _movement = GetComponent<MovementComponent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Si puedes dashear
        if (_putoDasheo)
        {
            //Dash
            PerformDash();

            // Cuando pase el tiempo del dash paras
            if(_time >= _dashTime)
            {
                _putoDasheo = false;
                _time = 0;
            }
            _time += Time.fixedDeltaTime;

            Debug.Log(_putoDasheo);
        }
    }

    /// <summary>
    /// Habilita poder dashear desde el Game Manager
    /// </summary>
    /// <param name="canDash"></param>
    /// <returns></returns>
    public bool CanDash(bool canDash)
    {
        _putoDasheo = canDash; 
        
        return _putoDasheo;
    }

    /// <summary>
    /// Realiza el cambio del transform para el dash
    /// </summary>
    private void PerformDash()
    {
        _dashSpeed = (_movement.Speed + (_dashDistance / _dashTime));

        _myTransform.position += _dashSpeed * Time.fixedDeltaTime * _movement._lastDirection;

        Debug.Log("tuvieja");
    }
}
