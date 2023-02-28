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
    private Gamepad _gamepad;
    private Transform _myTransform;
    private ParryComponent _parryComponent;
    private Animator _animator;
    #endregion
    #region Parameters
    [SerializeField]
    float _teleportDistance;
    [SerializeField]
    float _limitTime;
    [SerializeField]
    float _predictionAreaRadius=0.4f;
    #endregion
    #region Properties
    Vector3 _moveToVector;
    float _currentTime;
    public bool _telepotDone { get; private set; }
    LayerMask _floorMask;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _mouse = Mouse.current;
        _gamepad = Gamepad.current;
        _myDirectionComponent = GetComponent<DirectionComponent>();
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
        _currentTime += Time.unscaledDeltaTime;
        //calculo de la posición futura

        if (_gamepad != null)
        {
            _moveToVector = _myDirectionComponent.X_Directions(_gamepad.rightStick.ReadValue(),8);
            //Debug.Log(" mando"); 
        }
        else
        {
            //Debug.Log("no mando"); 
            _moveToVector = _myDirectionComponent.X_Directions(Camera.main.ScreenToWorldPoint(_mouse.position.ReadValue()) - _myTransform.position,8);
        }
        //Correcióin de el eje x debido a la orientación del jugador
        if(_myTransform.localEulerAngles.y == 0)
        {
            //Debug.Log("TuviejaNormal");
            _predictionTransform.localPosition = _moveToVector * _teleportDistance;
        }
        else if(_myTransform.localEulerAngles.y == 180)
        {
            //Debug.Log("TuviejaInvertida");
            _predictionTransform.localPosition = new Vector3(-_moveToVector.x, _moveToVector.y) * _teleportDistance;
        }
        //Teletransporte forzado
        if (_currentTime > _limitTime && !_telepotDone)
        {
            Teleport();
        }
    }
    public void TriggerTeleport()
    {
        Debug.Log("TUvieja");
        GameManager.Instance.SetPhysics(false);
        _predictionTransform.gameObject.SetActive(true);
        _telepotDone = false;
        _currentTime = 0;
    }
       
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
        _telepotDone = true;
        _parryComponent._parried = false;
        _predictionTransform.gameObject.SetActive(false);
        _animator.SetBool("IsFreeze", false);
        _animator.SetTrigger("Teleport");
    }
    private void TeleportEvent()
    {
        if (!Physics2D.OverlapCircle(_predictionTransform.position, _predictionAreaRadius, _floorMask))
        {
            _myTransform.localPosition += _moveToVector * _teleportDistance;
        }
    }
    private void SetPhysics()
    {
        GameManager.Instance.SetPhysics(true);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_predictionTransform.position, _predictionAreaRadius);
    }
}
