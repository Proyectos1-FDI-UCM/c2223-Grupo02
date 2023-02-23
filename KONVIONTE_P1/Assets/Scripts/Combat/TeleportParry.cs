using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TeleportParry : MonoBehaviour
{
    #region References
    [SerializeField]
    Transform _predictionTransform;
    DirectionComponent _myDirectionComponent;
    Mouse _mouse;
    Transform _myTransform;
    #endregion
    #region Parameters
    [SerializeField]
    float _teleportDistance;
    [SerializeField]
    float _timer;
    #endregion
    #region Properties
    Vector3 _moveToVector;
    float _time;
    bool _telepotDone;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myDirectionComponent = GetComponent<DirectionComponent>();
        _myTransform = transform;
        _mouse = Mouse.current;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        _moveToVector = _myDirectionComponent.EightAxis(Camera.main.ScreenToWorldPoint(_mouse.position.ReadValue()) - _myTransform.position);
        _predictionTransform.localPosition = _moveToVector * _teleportDistance;
        //_predictionTransform.localPosition = _moveToVector * _teleportDistance;
        if (_time > _timer)
        {
            
        }
    }
    public void Teleport(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _predictionTransform.gameObject.SetActive(true);
            _telepotDone = false;
        }
        if (context.performed || (context.canceled && !_telepotDone))
        {
            _predictionTransform.gameObject.SetActive(false);
            _myTransform.localPosition += _moveToVector * _teleportDistance;
            _telepotDone = true;
        }
    }
}
