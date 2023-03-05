using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACHADO AL PLAYER 
public class KnockbackComponent : MonoBehaviour
{
    /* Consiste en recibir un impulso en dirección contraria a la del ataque. Acción-Reacción, Newton 3
     * 
     * Se desactiva InputComponent (? AYUDA!!
     * 
     * Se necesita una referencia al player (probar por Rb o por Transform)
     * 
     * Mirar objeto colisión, para buscar el punto de colisión
     */

    #region References

    private Transform _myTransform;
    private Animator _myAnimator;
    #endregion

    #region Parameters     

    [Tooltip("Fuerza del retroceso")]
    [SerializeField] private float _knockbackForce;
    [SerializeField]
    [Tooltip("Margen al que se teletransporta el jugadror si el knockback se hace hacia una pared")]
    float _marginTpKnockBack = 0.5f;

    #endregion

    #region Properties

    //Dirección en la que aplicamos la fuerza (derecha o izquierda)
    //Mirar objeto Collision
    [SerializeField] private float _backHeigth;

    //Punto donde recibimos el golpe ("ContactPoint2D")
    private Vector3 _hitPoint;

    private LayerMask _floorLayerMask;

    private float _distance;
    #endregion


    #region Methods
    /// <summary>
    /// Mueve al jugador en <paramref name="xDirection"/>
    /// <para>Desacitiva el input</para>
    /// <para>Confiere Inmortalidad</para>
    /// <param name="xDirection"></param>
    /// </summary>
    public void Pushed(float xDirection)
    {
        //_myRigidbody2D.AddForce(_backDirection * _knockbackForce);
        _myAnimator.SetTrigger("KnockBack");

        //desplazamiento del knockback
        //la coordenada X debe ser 1 o -1 , asi que la obtenemos del X_directions
        //la coordenada Y debe ser negativa, porque estamos restando a la posicion
        //el knockback force multiplica el vector entero

        _distance = Physics2D.Raycast(_myTransform.position,
                                      new Vector3(-GameManager.Instance._directionComponent.X_Directions(new Vector2(xDirection, 0), 2).x, _backHeigth).normalized,_knockbackForce,_floorLayerMask
                                      ).distance;

        Debug.Log(_distance);
        //no choca con nada
        if (_distance == 0)
        {
            _myTransform.position -= new Vector3(GameManager.Instance._directionComponent.X_Directions( new Vector2(xDirection,0),2).x,
            -_backHeigth).normalized * _knockbackForce;      
        }
        else
        {          
            _myTransform.position -= new Vector3(GameManager.Instance._directionComponent.X_Directions(new Vector2(xDirection, 0), 2).x,
            -_backHeigth).normalized * (_distance -_marginTpKnockBack);
        }
        
        GameManager.Instance.InputOff();
        GameManager.Instance.InmortalityPlayer();
    }


    #endregion



    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myAnimator = GetComponent<Animator>();
        _floorLayerMask = LayerMask.GetMask("Floor");

    }
    public void EndKnockBack()
    {
        GameManager.Instance.InputOn();
        GameManager.Instance.InmortalityPlayer();
    }
}
