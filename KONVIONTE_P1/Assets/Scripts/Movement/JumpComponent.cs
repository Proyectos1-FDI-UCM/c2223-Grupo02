using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class JumpComponent : MonoBehaviour
{
    #region References
    private Transform _myTransform;
    private PlayerInputActions _actions;
    #endregion
    #region Prarameters
    [SerializeField]
    [Tooltip("Velocidad horizontal del salto")]
    private float _footSpeed;
    [SerializeField]
    [Tooltip("Altura del Salto")]
    private float _heightToPeak;
    [SerializeField]
    [Tooltip("Distancia a la que está la abside de la parábola")]
    private float _distanceToPeak;

    [Tooltip("Impulso de salto inicial")]
    [SerializeField]
    private float _jumpForceVal;
    #endregion
    #region Properties
    private float _velocity;
    private float _position;
    private float _gravity;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = GetComponent<Transform>();
        _actions = new PlayerInputActions();
        _actions.Player.Enable();
        _gravity = - (2 * _heightToPeak * Mathf.Pow(_footSpeed,2)) / Mathf.Pow(_distanceToPeak, 2);

    }
    //fixed update para regular la gravedad
    private void FixedUpdate()
    {
        //gravedad Magia de la física de la ESO
        _velocity += _gravity * Time.fixedDeltaTime;
        _position += _velocity * Time.fixedDeltaTime + 0.5f * _gravity * Mathf.Pow(Time.fixedDeltaTime, 2);
        _myTransform.position += _position * (Vector3)Vector2.up;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _gravity = - (2 * _heightToPeak * Mathf.Pow(_footSpeed, 2)) / Mathf.Pow(_distanceToPeak, 2);
            _velocity = _jumpForceVal + _gravity * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Tuvieja");
        _velocity = 0;
        _gravity = 0;
        _position = 0;
    }
}
