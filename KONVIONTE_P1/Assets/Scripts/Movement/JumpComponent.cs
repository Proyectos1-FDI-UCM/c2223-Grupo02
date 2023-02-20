using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

/*
-tiempo subida
-tiempo bajada
-salto controlable(segun tiempo que pulses el boton)
-altura ajustable
-subida mrua 
-bajada mru
*/
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

    [Header("Tiempos de subida y bajada")]
    [SerializeField]
    private float _ascensionTime;
    [SerializeField]
    private float _descensionTime;

    #endregion
    #region Properties
    [Space(15)]
    [SerializeField]
    private float _velocity;
    [SerializeField]
    private float _position;
    [SerializeField]
    private float _gravity;


    private float _fallSpeed;

    [SerializeField]
    private float _upIniSpeed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _actions = new PlayerInputActions();
        _actions.Player.Enable();
        _gravity = - (2 * _heightToPeak * Mathf.Pow(_footSpeed,2)) / Mathf.Pow(_distanceToPeak, 2);

        //la velocidad de bajada depende del tiempo y la altura a la que queramos llegar
        _fallSpeed = _heightToPeak / _descensionTime;
    }
    //fixed update para regular la gravedad
    private void FixedUpdate()
    {
        //gravedad Magia de la física de la ESO
        
        _velocity += _gravity * Time.fixedDeltaTime;
        _position = _velocity * Time.fixedDeltaTime + 0.5f * _gravity * Mathf.Pow(Time.fixedDeltaTime, 2);
        _myTransform.position += _position * (Vector3)Vector2.up;
        
        if (_velocity< 0)
        {
            Gravity();
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _gravity = - (2 * _heightToPeak * Mathf.Pow(_footSpeed, 2)) / Mathf.Pow(_distanceToPeak, 2);
            _velocity = _jumpForceVal + _gravity * Time.deltaTime;
        }
    }

    public void Gravity()
    {
        _myTransform.position -= Vector3.up * _fallSpeed*Time.fixedDeltaTime;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Tuvieja");
        _velocity = 0;
        _gravity = 0;
        _position = 0;
    }
}
