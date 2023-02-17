using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AtackComponent : MonoBehaviour
{
    #region Parameters
    [SerializeField]
    private int _damage;

    #endregion

    #region Properties  
    private Animator _animator;

    #endregion

    // Start is called before the first frame update
    void Start()
    {      
        _animator =  transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Este metodo se llama desde el input nuevo de Unity,
    /// concretamente desde el controlador del input del Player, cuando se pulsa la tecla espacio
    /// Para mas informacion ir al input en el editor
    /// </summary>
    /// <param name="context"></param>
    public void Atack(InputAction.CallbackContext context)
    {
        //Al comienzo de la pulsación de la tecla q hayamos escogido, se llama al animator para lanzar un trigger(una acción)
        //dicho trigger, activa la animacion de ataque(esta luego se desactiva sola por exit time). 
        //En dicha animación hay colocadas en ciertos frames acciones, que activan el collider al principio de la animacion
        //y lo desactivan al final. De esta manera el ataque se detecta solo en ese lapso de las animaciones
        if(context.started) _animator.SetTrigger("New Trigger");
        
        //if(context.canceled) _myTrigger.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si nuestro collider está activo(está en los frames de la animacion) y entra en contacto con algo que tenga un lifeComponent y que es de distinto tag
        //le inflinge daño a dicha entidad

        LifeComponent lc = collision.GetComponent<LifeComponent>();

        if (lc != null && collision.tag != tag)
        {
            lc.ReciveDamage(_damage);
        }
    }
}
