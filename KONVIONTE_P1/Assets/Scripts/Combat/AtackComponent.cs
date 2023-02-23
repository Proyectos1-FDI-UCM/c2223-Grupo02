using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//ESTE SCRIPT VA ATACHADO AL OBJETO CONTENEDOR DEL TRIGGER DE ATAQUE, VALIDO PARA JUGADOR Y PARA ENEMIGOS
//Nota: Attack va con 2 't's
public class AtackComponent : MonoBehaviour
{
    #region Parameters
    [SerializeField]
    private int _damage;

    #endregion

    #region Properties  
    private Animator _animator;
    
    private bool _impacted = false;
    LifeComponent _collisionLifeComponent;
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
    public void Atack()
    {
        //Al comienzo de la pulsación de la tecla q hayamos escogido, se llama al animator para lanzar un trigger(una acción)
        //dicho trigger, activa la animacion de ataque(esta luego se desactiva sola por exit time). 
        //En dicha animación hay colocadas en ciertos frames acciones, que activan el collider al principio de la animacion
        //y lo desactivan al final. De esta manera el ataque se detecta solo en ese lapso de las animaciones
        

        //NOTA PARA EL PLAYER NO HACE FALTA
        //CUIDADO, QUIZAS HAGA FALTA MIGRAR SISTEMAS
        //_animator.SetTrigger("IsAtacking");
        
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si nuestro collider está activo(está en los frames de la animacion) y entra en contacto con algo que tenga un lifeComponent y que es de distinta layer
        //le inflinge daño a dicha entidad

        if(_collisionLifeComponent == null)_collisionLifeComponent = collision.GetComponent<LifeComponent>();

        if (_collisionLifeComponent != null && collision.gameObject.layer != gameObject.layer)
        {
            //_collisionLifeComponent.ReciveDamage(_damage);
            _impacted = true;
        }
    }
    /// <summary>
    /// Se llama al acabar la animacion, chequea si se ha impactado con algo para saber si se puede hacer daño o no
    /// </summary>
    public void TryAtack()
    {
        //Debug.Log("0");

        if (_impacted)
        {
            //Debug.Log("1");
           
            if (gameObject.layer == LayerMask.NameToLayer("Player"))//si es el jugador, aplica el daño directamente al enemigo
            {
                _collisionLifeComponent.ReciveDamage(_damage);
                //Debug.Log("2");

            }
            else//si es el enemigo
            {
                //Debug.Log("3");

                //primero chequeamos si ha habido parry
                _collisionLifeComponent.ReciveDamage(_damage);
            }
            _impacted =false;
            _collisionLifeComponent=null;
        }
    }
}
