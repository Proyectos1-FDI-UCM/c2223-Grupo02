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
    [Tooltip("Altura del Salto")]
    private float _heightToPeak;
    [SerializeField]
    [Range(0f, 1f)]
    private float _speedDivider;

    [Header("Tiempos de subida y bajada")]
    [SerializeField]
    private float _ascensionTime;
    [SerializeField]
    private float _descensionTime;

    #endregion
    #region Properties
    private bool _canceled;
    private float _velocity;
    private float _position;
    private float _gravity;
    private float _fallSpeed;
    private float _upIniSpeed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _actions = new PlayerInputActions();
        _actions.Player.Enable();
        _gravity = - (2 * _heightToPeak) / Mathf.Pow(_ascensionTime, 2);

        // La velocidad de bajada depende del tiempo y la altura a la que queramos llegar
        _fallSpeed = _heightToPeak / _descensionTime;
        _upIniSpeed = (2 * _heightToPeak) / _ascensionTime;
    }

    //fixed update para regular la gravedad
    private void FixedUpdate()
    {
        // IMPORTANTE Para testear en ejecucion
        // _upIniSpeed = (2 * _heightToPeak) / _ascensionTime;
        // Gravedad Magia de la física de la ESO
        if (_velocity < 0 ||
            (_canceled && _velocity < (_upIniSpeed * _speedDivider)))
        {
            Gravity();
        }
        else
        {          
            _velocity -= Time.fixedDeltaTime * _upIniSpeed / _ascensionTime;
            _position = _velocity * Time.fixedDeltaTime + 0.5f * _gravity * Mathf.Pow(Time.fixedDeltaTime, 2);
            _myTransform.position += _position * (Vector3)Vector2.up;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _gravity = - (2 * _heightToPeak) / Mathf.Pow(_ascensionTime, 2);
            _velocity = _upIniSpeed;
            _canceled = false;
        }
        if (context.canceled)
        {
            _canceled = true;
        }
    }

    public void Gravity()
    {
        Debug.Log("tuviejaGravity");
        _myTransform.position -= Vector3.up * _fallSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        Debug.Log("TuviejaTrigger");
        _velocity = 0;
        _gravity = 0;
        _position = 0;
    }
}
