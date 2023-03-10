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
        //si nuestro collider est? activo(est? en los frames de la animacion) y entra en contacto con algo que tenga un lifeComponent y que es de distinta layer
        //le inflinge da?o a dicha entidad

        if(_collisionLifeComponent == null)_collisionLifeComponent = collision.GetComponent<LifeComponent>();

        if (_collisionLifeComponent != null && collision.gameObject.layer != gameObject.layer)
        {           
            _impacted = true;
        }
    }
    /// <summary>
    /// Se llama al acabar la animacion(es un evento de animacion), chequea si se ha impactado con algo para saber si se puede hacer da?o o no
    /// </summary>
    public void TryAplyDamage()
    {
        //Debug.Log("0");

        if (_impacted)
        {
            //Debug.Log("1");
           
            if (transform.parent.GetComponent<ParryComponent>() != null)//si es el jugador(el atacante), aplica el da?o directamente al enemigo
            {
                _collisionLifeComponent.ReciveDamage(_damage);
                transform.parent.GetComponent<ParryComponent>().ResetDamage();
                //Debug.Log("2");

            }
            else if(_collisionLifeComponent.GetComponent<ParryComponent>() != null)//si es el enemigo(el atacante)
            {
                //Debug.Log("3");

                //primero chequeamos si ha habido parry
                if (!_collisionLifeComponent.GetComponent<ParryComponent>()._parried && 
                    !_collisionLifeComponent._immortal)//si no ha parreado se le aplica el da?o
                {
                    _collisionLifeComponent.ReciveDamage(_damage);
                    //Debug.Log("He hecho da?o al jugador");

                    //KNOCKBACK

                    //dependiendo de la rotacion del padre(en el caso de los enemigos),
                    //hay que sumar o restar el offset del trigger de ataque(este objeto).
                    //Se hace una resta entre la posicion del player y la del trigger de ataque
                    //para obtener la posicion relativa en x, que se pasara por valor al m?todo del knockback
                    if(_myTransform.parent.localEulerAngles.y == 0)
                    {
                        _collisionLifeComponent.GetComponent<KnockbackComponent>().Pushed(
                        (_myTransform.position.x -_myTransform.localPosition.x)- _collisionLifeComponent.transform.position.x);
                    }
                    else
                    {
                        _collisionLifeComponent.GetComponent<KnockbackComponent>().Pushed(
                        (_myTransform.position.x + _myTransform.localPosition.x) - _collisionLifeComponent.transform.position.x);
                    }


                }
                //Debug.Log("el jugador parre?");

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
