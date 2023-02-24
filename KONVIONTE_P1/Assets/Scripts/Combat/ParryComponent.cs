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
    [SerializeField]
    private int _boostDamage;


    [SerializeField]
    private float _radius;

    [SerializeField]
    private float _parryTime;
    [SerializeField]
    private float _cooldownParryTime;

    #endregion

    #region Properties

    Transform _myTransform;

    AtackComponent _playerAtackComponent;

    private LayerMask _enemyAtackLayer;
    
    private Collider2D[] _colisions;

    private float _parryCurrentTime;

    private float _parryCooldownCurrentTime;

    public bool _parried;

    private bool _canParry;

    public bool _damageBoosted;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _enemyAtackLayer = LayerMask.GetMask("EnemyAtack");       
        _playerAtackComponent = GetComponent<AtackComponent>();
        _myTransform = transform;
        _parried = false;
        _canParry = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(_parryCurrentTime < _parryTime && !_parried)
        {
            _parryCurrentTime += Time.deltaTime;
            //hacemos un overlap con la layer del collider de los enemigos y si detectamos algo, hemos parreado
            _colisions = Physics2D.OverlapCircleAll(_myTransform.position, _radius, _enemyAtackLayer);
            
            if (_colisions.Length > 0)
            {
                _parried = true;
                _parryCurrentTime += _parryTime;
                _playerAtackComponent.SetDamage(_boostDamage);
                _damageBoosted = true;
                //_canParry=true;
            }
            else if(_parryCurrentTime >= _parryTime)
            {
                _parryCooldownCurrentTime = 0;
                _canParry = false;
            }
        }
        else if(_parryCooldownCurrentTime < _cooldownParryTime)
        {
            _parryCooldownCurrentTime += Time.deltaTime;
            if(_parryCooldownCurrentTime >= _cooldownParryTime)
            {
                _canParry = true;
            }
        }        
    }
    public void Parry()
    {
        if (_canParry)
        {
            _parryCurrentTime = 0;
            _canParry = false;
        }
    }

    //Llamar un frame despues del tryAtack para resetear?
    public void ResetDamage()
    {
        _playerAtackComponent.SetDamage(_baseDamage);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_myTransform.position, _radius);
    }
}
