using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ESTE SCRIPT VA ATACHADO AL PLAYER
//Falta comentar el codigo porque no esta terminado ni es definitivo
//NOTA: Collision es con 2 L
public class ParryComponent : MonoBehaviour
{
    #region Parameters
    [SerializeField]
    private int _baseDamage;
    
    private int _boostDamage;

    [SerializeField]
    private int _boostMultiplier;

    [SerializeField]
    private float _radius;

    [SerializeField]
    private float _parryTime;
    [SerializeField]
    private float _cooldownParryTime;
    [SerializeField]
    private float _boostDamageTime;

    #endregion

    #region Properties
    [SerializeField]
    Transform _myTransform;

    AtackComponent _playerAtackComponent;

    TeleportParry _playerTeleportComponent;

    private LayerMask _enemyAtackLayer;
    
    private Collider2D[] _colisions;

    private float _parryCurrentTime;

    private float _parryCooldownCurrentTime;
    [HideInInspector]
    public float _boostDamageCurrentTime;

    public bool _parried;

    private bool _canParry;

    public bool _damageBoosted;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _enemyAtackLayer = LayerMask.GetMask("EnemyAtack");       
        //CUIDADO ESTO SOLO FUNCIONA SEGUN LA JERARQUIA
        _playerAtackComponent = transform.GetChild(0).GetComponent<AtackComponent>();
        _playerTeleportComponent = GetComponent<TeleportParry>();
        _myTransform = transform;
        _parried = false;
        _canParry = true;
        _parryCurrentTime = _parryTime;

        _boostDamage = _baseDamage * _boostMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        //para la deteccion del parry seg�n cierto tiempo
        if(_parryCurrentTime < _parryTime && !_parried)
        {
            _parryCurrentTime += Time.deltaTime;
            //hacemos un overlap con la layer del collider de los enemigos y si detectamos algo, hemos parreado
            _colisions = Physics2D.OverlapCircleAll(_myTransform.position, _radius, _enemyAtackLayer);
            
            if (_colisions.Length > 0)
            {
                ParryEfects();                
            }
            else if(_parryCurrentTime >= _parryTime)
            {
                _parryCooldownCurrentTime = 0;
                _canParry = false;
            }
        }
        else if(_parryCooldownCurrentTime < _cooldownParryTime)//para el cooldown del parry
        {
            _parryCooldownCurrentTime += Time.deltaTime;
            if(_parryCooldownCurrentTime >= _cooldownParryTime)
            {
                _canParry = true;
            }
        }

        //quita el efecto del smite cuando pase el umbral de tiempo
        if (_damageBoosted)
        {
            _boostDamageCurrentTime += Time.deltaTime;
            if(_boostDamageCurrentTime > _boostDamageTime)
            {
                ResetDamage();                
            }
        }
    }
    public void PerformParry()
    {
        if (_canParry)
        {
            _parryCurrentTime = 0;
            _canParry = false;
        }
    }
    private void ParryEfects()
    {
        _parried = true;
        _canParry = true;
        _parryCurrentTime += _parryTime;
        _playerAtackComponent.SetDamage(_boostDamage);
        _damageBoosted = true;
        _playerTeleportComponent.TriggerTeleport();
        _boostDamageCurrentTime = 0;
    }
    //Llamar un frame despues del tryAtack para resetear?
    /// <summary>
    /// Resetea solo si es necesario
    /// </summary>
    public void ResetDamage()
    {
        if (_damageBoosted)
        {
            _playerAtackComponent.SetDamage(_baseDamage);
            _damageBoosted= false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_myTransform.position, _radius);
    }
}
