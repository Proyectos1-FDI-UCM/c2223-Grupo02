using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TeleportParry : MonoBehaviour
{
    #region References
    [Header("References")]
    [SerializeField]
    private Transform _predictionTransform;
    private DirectionComponent _myDirectionComponent;
    private Mouse _mouse;
    private Gamepad _gamepad;
    private Transform _myTransform;
    private ParryComponent _parryComponent;
    private Animator _animator;
    #endregion

    #region Parameters

    [Header("Distancias")]

    [SerializeField]
    float _teleportDistance;


    [SerializeField]
    [Tooltip("Margen al que se teletransporta el jugadror si el telepot se hace hacia una pared")]
    float _marginTeleport = 0.5f;

    [Header("Tiempos")]

    [SerializeField]
    float _limitTime;
    #endregion

    #region Properties
    Vector3 _moveToVector;
    float _currentTime;
    [SerializeField]
    public bool _telepotDone;
    LayerMask _floorMask;
    float _distance;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _mouse = Mouse.current;
        _gamepad = Gamepad.current;
        _myDirectionComponent = GameManager.DirectionComponent;
        _parryComponent= GetComponent<ParryComponent>();
        _myTransform = transform;
        //set parameters
        _currentTime = 0;
        _telepotDone = true;
        _floorMask = LayerMask.GetMask("Floor");
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //calculo de parámetros
        _currentTime += Time.unscaledDeltaTime;
        _distance = Physics2D.Raycast(_myTransform.position, _moveToVector, _teleportDistance, _floorMask).distance;

        if (_gamepad != null)
        {
            _moveToVector = _myDirectionComponent.X_Directions(_gamepad.rightStick.ReadValue(),8);
        }
        else
        {
            _moveToVector = _myDirectionComponent.X_Directions(Camera.main.ScreenToWorldPoint(_mouse.position.ReadValue()) - _myTransform.position,8);
        }
        //reposicionamiento del trasform de predicción
        if(_distance == 0)
        {
            _predictionTransform.position = _myTransform.position + _moveToVector * _teleportDistance;
        }
        else
        {
            _predictionTransform.position = _myTransform.position + _moveToVector * (_distance - _marginTeleport);
        }
        //Teletransporte forzado
        if (_currentTime > _limitTime && !_telepotDone)
        {
            Teleport();
        }
    }
    /// <summary>
    /// Desactiva la gravedad y permite ver la posición donde se ubicará el jugador
    /// </summary>
    public void TriggerTeleport()
    {
        //Debug.Log("TUvieja");
        AudioManager.Instance.Play("Freeze");
        _animator.SetBool("IsFreeze",true);
        _predictionTransform.gameObject.SetActive(true);
        _telepotDone = false;
        _currentTime = 0;
    }
    /// <summary>
    /// Método para realizar el teleport antes del tiempo establecido
    /// Llamada desde el input
    /// </summary>    
    public void PerfomTeleport()
    {
        if (!_telepotDone)
        {
            Teleport();
        }
    }
    /// <summary>
    /// Teletransporta al jugador comprobando si es posible realizar la acción
    /// </summary>
    private void Teleport()
    {
        _animator.SetBool("IsFreeze", false);
        _animator.SetBool("Teleport",true);
        _predictionTransform.gameObject.SetActive(false);
    }
    /// <summary>
    /// Teletrasnporte propiamente dicho
    /// Se le llama desde el evento del teleport
    /// </summary>
    private void TeleportEvent()
    {
        if (_distance == 0)
        {
            _myTransform.localPosition += _moveToVector * _teleportDistance;
        }
        else
        {
            _myTransform.localPosition += _moveToVector * (_distance - _marginTeleport);
        }
        _telepotDone = true;
        AudioManager.Instance.Play("Teleport");
        _animator.SetBool("Teleport", true);

        //GameManager.Instance.InmortalityPlayer();
    }
    /// <summary>
    /// Pone en true _teleportDone, esto permite que el jugador pueda volver a parrear  
    /// </summary>
    private void TeleportDone()
    {
        _telepotDone = true;
        _parryComponent.SetParryOff();
    }
}
