using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class DashComponent : MonoBehaviour
{
    private float _dashDistance;

    private Transform _myTransform;
    private bool _putoDasheo;
    [SerializeField] private float _maxDashDistance;

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Si puedes dashear
        if (_putoDasheo)
        {
            //Dash
            PerformDash();

            // Cundo recorras la distancia para
            if(_dashDistance >= _maxDashDistance)
            {
                _putoDasheo = false;
                _dashDistance = 0;
            }

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
        _dashDistance += _maxDashDistance * Time.fixedDeltaTime;

        _myTransform.position += _dashDistance * Time.fixedDeltaTime * (Vector3)Vector2.right;

        Debug.Log("tuvieja");
    }
}
