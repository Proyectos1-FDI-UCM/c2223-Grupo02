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

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //PARA QUE ESTO FUNCIONE, EL OBJETO DE ATAQUE DEL JUGADOR DEBE SER EL PRIMER HIJO EN LA JERARQUÍA
        _playerAtackComponent = transform.GetChild(0).GetComponent<AtackComponent>();

        _directionComponent = GetComponent<DirectionComponent>();
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
            _playerAtackComponent.Atack();
            //Debug.Log("Atack");
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
