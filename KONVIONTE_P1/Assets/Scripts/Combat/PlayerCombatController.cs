using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//ESTE SCRIPT VA ATACHADO AL OBJETO PLAYER
public class PlayerCombatController : MonoBehaviour
{
    #region Properties

    AtackComponent _playerAtackComponent;
    DirectionComponent _directionComponent;
    Transform _playerTransform;

    PlayerInputActions _playerInputActions;

    private Vector2 _verticalAtack;
    [SerializeField]
    private float _atackTriggerOffset = 1.2f;

    private Transform _atackTriggerTransform;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //PARA QUE ESTO FUNCIONE, EL OBJETO DE ATAQUE DEL JUGADOR DEBE SER EL PRIMER HIJO EN LA JERARQUÍA
        _playerAtackComponent = transform.GetChild(0).GetComponent<AtackComponent>();

        _directionComponent = GetComponent<DirectionComponent>();

        _playerTransform = transform;
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        _atackTriggerTransform = _playerTransform.GetChild(0).transform;//solo funciona si se cumple bien la jerarquía
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Llama a la funcion Atack del AtackComponent del jugador
    /// </summary>
    /// <param name="context"></param>
    public void Atack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ColocarCollider();
            _playerAtackComponent.Atack();
            //Debug.Log("Atack");
        } 
    }
    /// <summary>
    /// Coloca el collider del ataque en el sitio correcto segun el input
    /// </summary>
    public void ColocarCollider()
    {
        _verticalAtack = _playerInputActions.Player.VerticalAtack.ReadValue<Vector2>();
        if (_verticalAtack != Vector2.zero)
        {
            if(_verticalAtack.y > 0)
            {
                _atackTriggerTransform.localPosition = new Vector3(0,_atackTriggerOffset,0);
            }
            else // _verticalAtack.y <0
            {
                _atackTriggerTransform.localPosition = new Vector3(0,-_atackTriggerOffset,0);
            }
        }
        else
        {
            _atackTriggerTransform.localPosition = new Vector3(  _atackTriggerOffset,0, 0);
            
        }
    }

    /// <summary>
    /// Llama a la funcion TryAtack del AtackComponent del jugador
    /// </summary>
    /// <param name="context"></param>
    public void TryAtack()
    {
        //Debug.Log("aaa");

        _playerAtackComponent.TryAtack();
    }
}
