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

        if (_collisionLifeComponent != null && collision.gameObject.layer != gameObject.layer)//si son de distinta layer para que los enemigos no se hagan daño
        {
            Debug.Log("Golpeado por tu vieja");
            _impacted = true;
        }
    }
    /// <summary>
    /// Se llama al acabar la animacion(es un evento de animacion), chequea si se ha impactado con algo para saber si se puede hacer daño o no
    /// </summary>
    public void TryAplyDamage()
    {
        //si no ha impactado, no hacemos nada
        if (!_impacted) return;
        
        if (transform.parent.GetComponent<ParryComponent>() != null)//si es el jugador(el atacante), aplica el daño directamente al enemigo
        {
            _collisionLifeComponent.ReciveDamage(_damage);
            _myTransform.parent.GetComponent<ParryComponent>().ResetDamage();
        }
        else if(_collisionLifeComponent.GetComponent<ParryComponent>() != null)//si es el enemigo(el atacante)
        {
            //si no ha habido parry, se le aplica el daño
            if (!(_collisionLifeComponent.GetComponent<ParryComponent>().Parried ||
                _collisionLifeComponent.Immortal))
            {
                _collisionLifeComponent.ReciveDamage(_damage);

                //KNOCKBACK

                //dependiendo de la rotacion del padre(en el caso de los enemigos),
                //hay que sumar o restar el offset del trigger de ataque(este objeto).
                //Se hace una resta entre la posicion del player y la del trigger de ataque
                //para obtener la posicion relativa en x, que se pasara por valor al método del knockback

                float posicionRelativa = _myTransform.parent.localEulerAngles.y == 0 ?
                    (_myTransform.position.x - _myTransform.localPosition.x) - _collisionLifeComponent.transform.position.x :
                    (_myTransform.position.x + _myTransform.localPosition.x) - _collisionLifeComponent.transform.position.x;

                _collisionLifeComponent.GetComponent<KnockbackComponent>().Pushed(posicionRelativa);            
            }
        }
        //actualizar las variables de control
        _impacted =false;
        _collisionLifeComponent=null;      
    }

    public void LaserDamage()
    {
        if (_collisionLifeComponent == null) return;
        //si el jugador no ha parreado
        if (!(_collisionLifeComponent.GetComponent<ParryComponent>().Parried ||
               _collisionLifeComponent.Immortal))
        {
            //daño y knockback
            _collisionLifeComponent.ReciveDamage(_damage);

            float aux = GameManager.Instance.Player.GetComponent<MovementComponent>()._lastDirection.x;
            _collisionLifeComponent.GetComponent<KnockbackComponent>().Pushed(aux);


            //actualizar las variables de control
            _impacted = false;
            _collisionLifeComponent = null;
        }
    }
    public void SetDamage(int value)
    {
        _damage = value;
    }
    #endregion


}
