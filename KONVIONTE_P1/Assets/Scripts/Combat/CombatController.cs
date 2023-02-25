using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//ESTE SCRIPT VA ATACHADO TANTO AL PLAYER COMO A LOS ENEMIGOS
public class CombatController : MonoBehaviour
{
    #region Properties

    private Animator _animator;
    private AtackComponent _myAtackComponent;
    private Transform _myTransform;  

    private Transform _atackTriggerTransform;
    [SerializeField]
    private float _atackTriggerOffset;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();  
        //PARA QUE ESTO FUNCIONE, EL OBJETO DE ATAQUE DE LA ENTIDAD DEBE SER EL PRIMER HIJO EN LA JERARQUÍA
        _myAtackComponent = transform.GetChild(0).GetComponent<AtackComponent>();
        
        _myTransform = transform;
        _atackTriggerTransform = _myTransform.GetChild(0).transform;//solo funciona si se cumple bien la jerarquía
    }

    /// <summary>
    /// Esta funcion se llamará desde el input en el caso del jugador y desde la IA en el caso de los enemigos
    /// Coloca el collider de la entidad en su lugar correspontiente 
    /// Activa la animacion de ataque, (la cual a su vez pondra en activo el collider(trigger) del AtackComponent)
    /// En dicha animación hay colocadas en ciertos frames acciones, que activan el collider al principio de la animacion
    /// y lo desactivan al final. De esta manera el ataque se detecta solo en ese lapso de las animaciones  
    /// </summary>
    /// <param name="direction"></param>
    public void Atack(Vector2 direction)
    {
        if (!_animator.GetBool("IsAttaking"))//si no está ya atacando
        {
            ColocarCollider(direction);            
            _animator.SetBool("IsAttaking", true);
        }
    }
    /// <summary>
    /// Coloca el collider del ataque en el sitio correcto segun el vector que se le pase
    /// Se observa la coordenada Y del vector, si es mayor que 0, se coloca el collider arriba, si es menor se coloca abajo
    /// Para el player se le pasara un vector del input y para los enemigos un vector del 4direcciones
    /// </summary>
    public void ColocarCollider(Vector2 direction)
    {        
        Vector3 aux;

        if (direction.y != 0)
        {
            if(direction.y > 0) aux = new Vector3(0,  _atackTriggerOffset, 0);
            else                aux = new Vector3(0, -_atackTriggerOffset, 0); // _verticalAtack.y <0            
        }
        else aux = new Vector3(_atackTriggerOffset, 0, 0);

        _atackTriggerTransform.localPosition = aux;
    }

    /// <summary>
    /// Llama a la funcion TryAtack del AtackComponent del jugador
    /// </summary>
    /// <param name="context"></param>
    public void TryAtack()
    {        
        _myAtackComponent.TryAtack();
    }
}
