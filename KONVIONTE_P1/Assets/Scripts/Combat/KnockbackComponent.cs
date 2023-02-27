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

    private Rigidbody2D _myRigidbody2D;

    private Transform _myTransform;

    #endregion

    #region Parameters     

    [Tooltip("Fuerza del retroceso")]
    [SerializeField] private float _knockbackForce;

    #endregion

    #region Properties

    //Dirección en la que aplicamos la fuerza (derecha o izquierda)
    //Mirar objeto Collision
    [SerializeField] private Vector3 _backDirection;

    //Punto donde recibimos el golpe ("ContactPoint2D")
    private Vector3 _hitPoint;

    public bool _push;

    #endregion


    #region Methods

    public void Pushed()
    {
        //_myRigidbody2D.AddForce(_backDirection * _knockbackForce);

        _myTransform.position += _backDirection * _knockbackForce;
        _push = false;

    }


    #endregion



    // Start is called before the first frame update
    void Start()
    {
        //_myRigidbody2D = GetComponent<Rigidbody2D>();

        ////No existe una fuerza en la normal que haga que la fricción con el suelo haga efecto
        ////Así que, Manolo se va a la puta, porque no para nunca. 
        ///

        _myTransform = transform;


    }

    // Update is called once per frame
    void Update()
    {
        if (_push)
        {
            Pushed();
        }
    }
}
