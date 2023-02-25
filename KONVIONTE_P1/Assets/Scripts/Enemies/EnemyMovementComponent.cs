using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementComponent : MonoBehaviour
{
    /* Parámetros: _speed, _detectionDistance, _timeOfRutine (con bool para cambio de dirección) 
     *Animaciones
     *_distanceToPlayer: Raycast de detección a _playerTransform  
     * Animator;
     * booleano _attacking
     * RigidBody (para que se caiga)
     * 
     * Para flip direcction, cambiar scale.x entre 1 y -1 Transform.localScale = new Vector3 (-1,1,0) en la derecha del todo y new V3 (1,1,0) en la izqda del todo
     */

    #region References

    //Moveremos al enemigo por tranform
    private Transform _myTransform;

    //Properties, tía, cambia esto
    //Distancia hasta el jugador, para hacer un raycast
    private Vector3 _distanceToTarget;

    #endregion


    #region Parameters

    [Tooltip("Se refiere al jugador a seguir")]
    [SerializeField] private GameObject _target;

    #endregion

    #region Methods

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
