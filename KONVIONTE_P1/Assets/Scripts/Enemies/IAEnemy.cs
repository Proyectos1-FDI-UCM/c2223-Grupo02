using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnemy : MonoBehaviour
{

    #region References

    private Transform _player;

    private Transform _myTransform;
    private CombatController _myCombatController;
    private MovementComponent _myMovementComponent;
    #endregion

    #region Parameters
    

    [SerializeField]
    Vector3 _detectionBoxSize;
    [SerializeField]
    Vector3 _detectionBoxOffset;

    [SerializeField]
    Vector3 _attackBoxSize;
    [SerializeField]
    Vector3 _attackBoxOffset;


    [Tooltip("Tiempo de cada patrullaje")]
    [SerializeField] private float _routineTime;

    [Tooltip("Tiempo de parada entre cada patrullaje")]
    [SerializeField] private float _stopTime;

    [Tooltip("Tiempo en el que se actualiza la posicion del jugador para el follow")]
    [SerializeField] private float _followTime;

    [Tooltip("Tiempo entre ataques")]
    [SerializeField] private float _attackTime;

    [Tooltip("Distancia del rayo que detecta la colision con las paredes")]
    [SerializeField]
    private float _raycastDistance;

    #endregion

    #region Properties

    /*
    0 = patrulla aleatoria
    1 = perseguir al jugador
    2 = atacar     
     */

    [SerializeField]
    private int _estadoActual;


    private float _currentPatrollTime;

    private float _currentFollowTime;

    private float _currentAttackTime;

    private LayerMask _playerLayerMask;

    private LayerMask _floorLayerMask;

    private Vector3 _movementDirection;
    
    private RaycastHit2D _raycastInfo;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        
        _player = GameManager.Instance.Player.transform;
        
        _myCombatController = GetComponent<CombatController>();
        
        _myMovementComponent = GetComponent<MovementComponent>();

        _playerLayerMask = LayerMask.GetMask("Player");

        _floorLayerMask = LayerMask.GetMask("Floor");

        _estadoActual = 0;

        _currentPatrollTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //para ver la caja
        OurNamespace.Box.ShowBox(_attackBoxSize, _attackBoxOffset, _myTransform);
        if (_estadoActual == 0)//patrulla aleatoria
        {
            //Debug.Log("1");
            
            //Restamos el tiempo
            _currentPatrollTime -= Time.deltaTime;

            //Si el tiempo llega a 0 (o es menor)
            if (_currentPatrollTime < 0)
            {
                //calculamos aleatoriamente la siguiente direccion
                _movementDirection = Vector3.right * Random.Range(-1, 2);//devuelve un aleatorio -1,0,1 

                //si es una parada, asignamos el tiempo de parada
                if (_movementDirection == Vector3.zero)
                {
                    _currentPatrollTime = _stopTime;
                }
                else //si es un  movimiento asignamos el tiempo de movimiento
                {
                    _currentPatrollTime = _routineTime;
                }

                //actualizamos la direccion en el movement
                _myMovementComponent.SetDirection(_movementDirection);
                
                //Debug.Log("2");
            }

            //prueba de rayo

           
            //casteo del rayo
            _raycastInfo = Physics2D.Raycast(_myTransform.position, _myTransform.right, _raycastDistance, _floorLayerMask);

            //si he chocado con una pared
            if (_raycastInfo.transform != null)
            {
                Debug.Log("tu vieja ha chocado con una pared"); 
                //cambiamos la direccion
                _movementDirection *= -1;

                //actualizamos el tiempo
                _currentPatrollTime = _routineTime;

                //actualizamos la direccion en el movement
                _myMovementComponent.SetDirection(_movementDirection);
            }

            //para ver la caja
            OurNamespace.Box.ShowBox(_detectionBoxSize, _detectionBoxOffset, _myTransform);
            
            //si el enemigo detecta al jugador
            if(OurNamespace.Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask))
            {
                //cambiamos el estado a perseguir
                _estadoActual = 1;
                
                //para que entre a la actualizacion en el estado 1
                _currentFollowTime = 0;

                //settear el tiempo entre ataques
                _currentAttackTime = 0;

                //cambiar velocidad del bicho?
            }

        }
        else if(_estadoActual == 1)//perseguir al jugador
        {
            //para que haya un delay en la actualizacion de la rutina de perseguir al jugador

            //disminuir el tiempo de follow
            _currentFollowTime -= Time.deltaTime;

            //disminuir el tiempo de ataque 
            _currentAttackTime -= Time.deltaTime;

            if(_currentFollowTime < 0)
            {
                //seteo del time
                _currentFollowTime = _followTime;
                //seteo de la direccion de movimiento
                _myMovementComponent.SetDirection(GameManager.Instance._directionComponent.X_Directions(_player.position - _myTransform.position,2));
            }

            //para ver la caja
            OurNamespace.Box.ShowBox(_attackBoxSize, _attackBoxOffset, _myTransform);


            //si el enemigo deja de detectar al jugador, volvemos al estado 1
            if (!OurNamespace.Box.DetectSomethingBox(_detectionBoxSize, _detectionBoxOffset, _myTransform, _playerLayerMask))
            {
                _estadoActual = 0;
                _currentPatrollTime = 0;
            }

            //si el jugador está en la caja de ataque y ha pasado el tiempo entre ataques, atacar
            if (OurNamespace.Box.DetectSomethingBox(_attackBoxSize, _attackBoxOffset, _myTransform, _playerLayerMask) &&
                _currentAttackTime <0)
            {
                _myCombatController.Atack(_player.position - _myTransform.position);
                _estadoActual = 2;
            }
        }
        else if(_estadoActual == 2)//atacar
        {
            //vacio porque no hace nada, solo espera a volver a otro estado segun la animacion
        }
        
    }


    /// <summary>
    /// Se llama desde un animation event al final de la animacion de ataque
    /// </summary>
    private void VolverAlEstadoFollow()
    {
        _estadoActual = 1;
        _currentFollowTime = 0;
        _currentAttackTime = _attackTime;
    }
}



namespace OurNamespace
{
    /*Comentar bien los metodos*/
    public class Box
    {
        /// <summary>
        /// Muestra la caja con los parametros que se le dan
        /// </summary>
        public static void ShowBox(Vector3 _boxSize, Vector3 _boxOffSet, Transform _spawnTransform)
        {
            //la caja cambia segun la rotacion del objeto(para mas info buscar el operador ?)
            int _direction = _spawnTransform.rotation.y == 0 ? 1:-1;
                      
            //pintado de la caja
            Debug.DrawRay(_spawnTransform.position - _boxSize + new Vector3(_boxOffSet.x * _direction, _boxOffSet.y), Vector2.right);
            Debug.DrawRay(_spawnTransform.position + _boxSize + new Vector3(_boxOffSet.x * _direction, _boxOffSet.y), Vector2.left);
            Debug.DrawRay(_spawnTransform.position - _boxSize + new Vector3(_boxOffSet.x * _direction, _boxOffSet.y), Vector2.up);
            Debug.DrawRay(_spawnTransform.position + _boxSize + new Vector3(_boxOffSet.x * _direction, _boxOffSet.y), Vector2.down);           
        }

        /// <summary>
        /// Devuelve si detecta lo que se le ha pedido o no
        /// </summary>     
        public static bool DetectSomethingBox(Vector3 _boxSize, Vector3 _boxOffSet, Transform _spawnTransform, LayerMask _layerToFliter)
        {
            //la caja cambia segun la rotacion del objeto(para mas info buscar el operador ?)
            int _direction = _spawnTransform.rotation.y == 0 ? 1 : -1;

            Collider2D _colliderResult = Physics2D.OverlapArea(_spawnTransform.position - _boxSize + new Vector3(_boxOffSet.x * _direction, _boxOffSet.y) //punto 1 de la caja
                                          , _spawnTransform.position + _boxSize + new Vector3(_boxOffSet.x * _direction, _boxOffSet.y), //punto 2 de la caja
                                           _layerToFliter);//capa que filtra la deteccion
            return _colliderResult != null;            
        }
    }




}