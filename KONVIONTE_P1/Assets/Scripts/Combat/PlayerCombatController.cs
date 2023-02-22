using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//ESTE SCRIPT VA ATACHADO AL OBJETO PLAYER
public class PlayerCombatController : MonoBehaviour
{
    #region Properties

    private AtackComponent _myAtackComponent;
    private Transform _myTransform;
    private PlayerInputActions _playerInputActions;


    private Transform _atackTriggerTransform;
    [SerializeField]
    private float _atackTriggerOffset;

    private Vector2 _verticalAtack;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //PARA QUE ESTO FUNCIONE, EL OBJETO DE ATAQUE DE LA ENTIDAD DEBE SER EL PRIMER HIJO EN LA JERARQUÍA
        _myAtackComponent = transform.GetChild(0).GetComponent<AtackComponent>();
        

        _myTransform = transform;
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        _atackTriggerTransform = _myTransform.GetChild(0).transform;//solo funciona si se cumple bien la jerarquía
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_playerInputActions.Player.VerticalAtack.ReadValue<Vector2>());
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
            _myAtackComponent.Atack();
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

        _myAtackComponent.TryAtack();
    }
}
