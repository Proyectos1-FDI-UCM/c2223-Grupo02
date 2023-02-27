using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementComponent : MonoBehaviour
{
    /*
     *_distanceToPlayer: Comparar distancia en X, simplemente, pq a lo mejor Raycast no necesario
     * Animator;
     * Para flip direcction, cambiar scale.x entre 1 y -1 Transform.localScale = new Vector3 (-1,1,0) en la derecha del todo y new V3 (1,1,0) en la izqda del todo
     * Vector 3 right, left, zero
     */

    #region References

    //Moveremos al enemigo por tranform
    private Transform _myTransform;

    #endregion

    #region Properties

    //Distancia hasta el jugador
    private Vector3 _distanceToTarget;

    //Controla si está persiguiendo o no al jugador
    private bool _isAttacking;

    #endregion

    #region Parameters

    [Tooltip("Jugador al que perseguir")]
    [SerializeField] private GameObject _target;

    [Tooltip("Velocidad de patrulla")]
    [SerializeField] private float _speed;

    [Tooltip("Velocidad de persecución")]
    [SerializeField] private float _maxSpeed;

    [Tooltip("Tiempo de cada patrullaje")]
    [SerializeField] private float _routineTime;

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
