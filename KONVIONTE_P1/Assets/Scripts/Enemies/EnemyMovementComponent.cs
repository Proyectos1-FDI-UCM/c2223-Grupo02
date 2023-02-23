using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementComponent : MonoBehaviour
{
    /*Referencias: _enemyTransform, _playerTransform (o bien tomarlo como GO _target, para poder acceder a todo), 
     *Parámetros: _speed, _detectionDistance, _timeOfRutine (con bool para cambio de dirección) 
     *Animaciones
     *_distanceToPlayer: Raycast de detección a _playerTransform  
     * Animator;
     * booleano _attacking
     * RigidBody (para que se caiga)
     * 
     * Para flip direcction, cambiar scale.x entre 1 y -1 Transform.localScale = new Vector3 (-1,1,0) en la derecha del todo y new V3 (1,1,0) en la izqda del todo
     */


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
