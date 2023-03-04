using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

//ESTE SCRIPT VA ATACHADO AL OBJETO CONTENEDOR DEL TRIGGER DE ATAQUE, VALIDO PARA JUGADOR Y PARA ENEMIGOS
//Nota: Attack va con 2 't's
//IMPORTANTE FALTA HACER QUE SIRVA PARA ATACAR A VARIOS ENEMIGOS A LA VEZ
public class AtackComponent : MonoBehaviour
{
    #region References
    private Transform _myTransform;
    #endregion
    #region Parameters
    [SerializeField]
    private int _damage;
    #endregion

    #region Properties  
       
    private bool _impacted = false;
    LifeComponent _collisionLifeComponent;
    private int _realDamage;
    #endregion
    private void Start()
    {
        _myTransform = transform;
    }
    #region Methods
    private void OnTriggerStay2D(Collider2D collision)//cuidado con si es stay o enter
    {
        //si nuestro collider está activo(está en los frames de la animacion) y entra en contacto con algo que tenga un lifeComponent y que es de distinta layer
        //le inflinge daño a dicha entidad

        if(_collisionLifeComponent == null)_collisionLifeComponent = collision.GetComponent<LifeComponent>();

        if (_collisionLifeComponent != null && collision.gameObject.layer != gameObject.layer)
        {           
            _impacted = true;
        }
    }
    /// <summary>
    /// Se llama al acabar la animacion(es un evento de animacion), chequea si se ha impactado con algo para saber si se puede hacer daño o no
    /// </summary>
    public void TryAplyDamage()
    {
        //Debug.Log("0");

        if (_impacted)
        {
            //Debug.Log("1");
           
            if (transform.parent.GetComponent<ParryComponent>())//si es el jugador(el atacante), aplica el daño directamente al enemigo
            {
                _collisionLifeComponent.ReciveDamage(_damage);
                transform.parent.GetComponent<ParryComponent>().ResetDamage();
                //Debug.Log("2");

            }
            else//si es el enemigo(el atacante)
            {
                //Debug.Log("3");

                //primero chequeamos si ha habido parry
                if (!_collisionLifeComponent.GetComponent<ParryComponent>()._parried)//si no ha parreado se le aplica el daño
                {
                    _collisionLifeComponent.ReciveDamage(_damage);
                    //Debug.Log("He hecho daño al jugador");

                    //Knockback
                    if(_myTransform.parent.localEulerAngles.y == 0)
                    {
                        _collisionLifeComponent.GetComponent<KnockbackComponent>().Pushed((_myTransform.position.x -_myTransform.localPosition.x)- _collisionLifeComponent.transform.position.x);
                    }
                    else
                    {
                        _collisionLifeComponent.GetComponent<KnockbackComponent>().Pushed((_myTransform.position.x + _myTransform.localPosition.x) - _collisionLifeComponent.transform.position.x);

                    }


                }
                //Debug.Log("el jugador parreó");

            }
            _impacted =false;
            _collisionLifeComponent=null;
        }
    }
    public void SetDamage(int value)
    {
        _damage = value;
    }
    #endregion


}
