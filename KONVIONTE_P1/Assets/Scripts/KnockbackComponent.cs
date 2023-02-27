using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACHADO AL PLAYER 
public class KnockbackComponent : MonoBehaviour
{
    /* Consiste en recibir un impulso en direcci�n contraria a la del ataque. Acci�n-Reacci�n, Newton 3
     * 
     * Se desactiva InputComponent
     * 
     * No se puede recibir da�o mientras. - bool _immortal. True: durante Knockback, False: el resto del tiempo 
     * Relacionado a LifeComponent
     * 
     * Se necesita una referencia al player, y a su RB
     * || Par�metro de fuerza 
     * 
     * Vector 3, para marcar la direcci�n. Mirar objeto colisi�n, para buscar el punto de colisi�n
     * 
     * bool "_damaged" - True: Knockback. False: Parry. No nos importa ahora
     * 
     * 
     * 
     */

    #region Parameters     

    [Tooltip("Fuerza del retroceso")]
    [SerializeField] private int _kbForce;

    #endregion

    #region Properties

    //Direcci�n en la que aplicamos la fuerza (derecha o izquierda)
    //Mirar objeto Collision
    private Vector3 _kbDirection;

    //Marca si el personaje ha sido herido. 
    private bool _damaged;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
