using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _myTargetTransform;
    [SerializeField] private float _zOffset;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _xOffset;
    private float _interpolation;
    private Transform _myTransform;
    private float _horizontalMovement;
    private PlayerInputActions _actions;

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _interpolation = 0f;
        _actions = new PlayerInputActions();
        _actions.Enable();
        _myTransform.position = new Vector3(_myTargetTransform.position.x, _myTargetTransform.position.y + _yOffset, _zOffset);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //_myTransform.position = new Vector3(_myTargetTransform.position.x, _myTransform.position.y, _zOffset);
        if (_actions.Player.HorizontalMove.ReadValue<Vector2>() != Vector2.zero)
        {
            _horizontalMovement = Mathf.Lerp(_myTargetTransform.position.x, _myTargetTransform.position.x + _xOffset, _interpolation);
            _myTransform.position = new Vector3(_myTargetTransform.position.x + _horizontalMovement, _myTransform.position.y, _zOffset);
            _interpolation += Time.deltaTime;
        }
        else
        {
            _horizontalMovement = Mathf.Lerp(_myTransform.position.x, _myTargetTransform.position.x, Time.deltaTime);
            _myTransform.position = new Vector3(_myTargetTransform.position.x, _myTransform.position.y, _zOffset);
        }
    }

    //public void CameraMovement(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        _horizontalMovement = Mathf.Lerp(_myTargetTransform.position.x, _myTargetTransform.position.x + _xOffset, Time.deltaTime);
    //        _myTransform.position = new Vector3(_myTargetTransform.position.x + _horizontalMovement, _myTransform.position.y, _zOffset);
    //    }
    //    else if (context.canceled)
    //    {
    //        _horizontalMovement = Mathf.Lerp(_myTransform.position.x, _myTargetTransform.position.x, Time.deltaTime);
    //        _myTransform.position = new Vector3(_myTargetTransform.position.x, _myTransform.position.y, _zOffset);
    //    }
    //}   
}
