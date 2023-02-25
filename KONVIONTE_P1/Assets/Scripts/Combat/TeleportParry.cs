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
    private Transform _predictionTransform;
    private DirectionComponent _myDirectionComponent;
    private Mouse _mouse;
    private Transform _myTransform;
    private ParryComponent _parryComponent;
    #endregion
    #region Parameters
    [SerializeField]
    float _teleportDistance;
    [SerializeField]
    float _limitTime;
    #endregion
    #region Properties
    Vector3 _moveToVector;
    float _currentTime;
    bool _telepotDone;
    LayerMask _floorMask;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myDirectionComponent = GetComponent<DirectionComponent>();
        _parryComponent= GetComponent<ParryComponent>();
        _myTransform = transform;
        _mouse = Mouse.current;
        _currentTime = 0;
        _telepotDone = true;
        _floorMask = LayerMask.GetMask("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        //calculo de la posición futura
        _moveToVector = _myDirectionComponent.EightAxis(Camera.main.ScreenToWorldPoint(_mouse.position.ReadValue()) - _myTransform.position);
        if(_myTransform.localEulerAngles.y == 0)
        {
            Debug.Log("TuviejaNormal");
            _predictionTransform.localPosition = _moveToVector * _teleportDistance;
        }
        else if(_myTransform.localEulerAngles.y == 180)
        {
            Debug.Log("TuviejaInvertida");
            _predictionTransform.localPosition = new Vector3(-_moveToVector.x, _moveToVector.y) * _teleportDistance;
        }
        //_predictionTransform.localPosition = _moveToVector * _teleportDistance;
        if (_currentTime > _limitTime && !_telepotDone)
        {
            Teleport();
        }
        if(Physics2D.OverlapArea(_myTransform.position - new Vector3(1,1),new Vector2(1,1),_floorMask))
        {
            Debug.Log("Tu vieja");
        }
    }
    public void TriggerTeleport()
    {
        _predictionTransform.gameObject.SetActive(true);
        _telepotDone = false;
        _currentTime = 0;                    
    }
    
    //mover al GM
    public void PerfomTeleport(InputAction.CallbackContext context)
    {
        if (context.performed && !_telepotDone)
        {
            Teleport();
        }
    }
    private void Teleport()
    {
        _predictionTransform.gameObject.SetActive(false);
        _myTransform.localPosition += _moveToVector * _teleportDistance;
        _telepotDone = true;
        //cambiar por metodo
        _parryComponent._parried = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_predictionTransform.position, new Vector3(1, 1, 0));
    }
}
