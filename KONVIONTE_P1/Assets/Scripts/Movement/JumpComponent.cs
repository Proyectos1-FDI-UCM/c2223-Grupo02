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
    private Collider2D _myCollider;
    private Animator _myAnimator;
    [SerializeField] private LayerMask _floorMask;
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
    private bool _isGrounded;
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
        _myAnimator = GetComponent<Animator>();
        _myCollider = GetComponent<Collider2D>();
        _gravity = - (2 * _heightToPeak) / Mathf.Pow(_ascensionTime, 2);

        // La velocidad de bajada depende del tiempo y la altura a la que queramos llegar
        _fallSpeed = _heightToPeak / _descensionTime;
        _upIniSpeed = (2 * _heightToPeak) / _ascensionTime;
        _isGrounded = true;
    }

    //fixed update para regular la gravedad
    private void FixedUpdate()
    {
        // IMPORTANTE Para testear en ejecucion
        // _upIniSpeed = (2 * _heightToPeak) / _ascensionTime;
        // Gravedad Magia de la física de la ESO
        if (_velocity <= 0 ||
            (_canceled && _velocity < (_upIniSpeed * _speedDivider)) || 
             (DetectRoof() && _canceled))
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

    private void Update()
    {
        DetectFloor();
    }

    /// <summary>
    /// Si el jugador da el input realiza el salto
    /// </summary>
    /// <param name="performed"></param>
    /// <param name="canceled"></param>
    public void Jump(bool performed,bool canceled)
    {
        Debug.Log(_isGrounded);
        if (performed && _isGrounded)
        {
            _myAnimator.SetTrigger("Jump");
            _gravity = - (2 * _heightToPeak) / Mathf.Pow(_ascensionTime, 2);
            _velocity = _upIniSpeed;
            _canceled = false;
            _isGrounded = false;
        }
        if (canceled)
        {
            _canceled = true;
            _isGrounded = false;
        }
    }    
    public void Gravity()
    {
        //Debug.Log("tuviejaGravity");
        _myTransform.position -= Vector3.up * _fallSpeed * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Casteamos una caja que si colisiona con el <paramref name="_layerMask"/> del suelo restea la gravedad (solo si está bajando) y deja saltar al jugador
    /// </summary>
    private void DetectFloor()
    {
        if (Physics2D.BoxCast(_myCollider.bounds.center, _myCollider.bounds.size - (Vector3)new Vector2(0, .1f),
            0f, Vector2.down, .2f, _floorMask))
        {
            _isGrounded = true;
            //para evitar que se pare justo nada mas saltar
            if(_velocity < 0f)
            {
                _velocity = 0;
                _gravity = 0;
                _position = 0;
            }
        }
    }

    private bool DetectRoof()
    {
        bool detected = false;
        if(Physics2D.BoxCast(_myCollider.bounds.center, _myCollider.bounds.size - (Vector3)new Vector2(0, .1f), 
            0f, Vector2.up, .2f, _floorMask))
        {
            _canceled = true;
            detected = true;
        }

        return detected;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(_myCollider.bounds.center, _myCollider.bounds.size - (Vector3)new Vector2(0, .1f));
    }
}
