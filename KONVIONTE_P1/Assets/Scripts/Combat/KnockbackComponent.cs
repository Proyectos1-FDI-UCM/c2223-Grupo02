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

    #endregion

    #region Properties

    //Dirección en la que aplicamos la fuerza (derecha o izquierda)
    //Mirar objeto Collision
    [SerializeField] private float _backHeigth;

    //Punto donde recibimos el golpe ("ContactPoint2D")
    private Vector3 _hitPoint;
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
        _myTransform.position -= new Vector3(GameManager.Instance._directionComponent.X_Directions( new Vector2(xDirection,0),2).x, -_backHeigth).normalized * _knockbackForce;
        Debug.Log(new Vector3(xDirection, _backHeigth).normalized * _knockbackForce);
        GameManager.Instance.InputOff();
        GameManager.Instance.InmortalityPlayer();
    }


    #endregion



    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myAnimator = GetComponent<Animator>();

    }
    public void EndKnockBack()
    {
        GameManager.Instance.InputOn();
        GameManager.Instance.InmortalityPlayer();
    }
}
