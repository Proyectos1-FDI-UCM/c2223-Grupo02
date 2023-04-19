using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ESTE SCRIPT VA ATACHADO AL PLAYER
//Falta comentar el codigo porque no esta terminado ni es definitivo
//NOTA: Collision es con 2 L
public class ParryComponent : MonoBehaviour
{

    #region Parameters

    //Daños con y sin smite
    [Header("Daño y bufo de daño")]
    [SerializeField]
    private int _baseDamage;
    
    [Tooltip("Multiplicador del daño tras el parry")]
    [SerializeField]
    private int _boostMultiplier;

    [Tooltip("Duración del smite")]
    [SerializeField]
    private float _boostDamageTime;

    [Space(10)]
    [Header("Estadísticas Parry")]
    [Tooltip("Radio del área de parry")]
    [SerializeField]
    private float _radius;

    [Tooltip("Tiempo que dura la detección del parry")]
    [SerializeField]
    private float _parryTime;
    
    [Tooltip("Enfriamiento desde que se usa un parry hasta que puedes volver a usarlo s lo fallaste")]
    [SerializeField]
    private float _cooldownParryTime;
    

    #endregion

    #region Properties
    [SerializeField]
    Transform _myTransform;

    AtackComponent _playerAtackComponent;

    TeleportParry _playerTeleportComponent;

    Animator _playerAnimator;

    private LayerMask _enemyAtackLayer;
    
    private Collider2D[] _colisions;

    private float _parryCurrentTime;

    private float _parryCooldownCurrentTime;
    [HideInInspector]
    public float _boostDamageCurrentTime;
    //boolenao que indica si nos encontramos en la ventana de parry
    [SerializeField]
    private bool _parried;
    //Booleano que te permite hacer el parry
    private bool _canParry;

    public bool _damageBoosted;
    private int _boostDamage;

    private bool _encontrao;
    #endregion

    #region Accesors
    public bool CanParry
    {
        get { return _canParry; }
    }
    public bool Parried
    {
        get { return _parried; }
    }
    public bool Encontrao
    {
        get { return _encontrao; }
    }
    #endregion
  

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ParryComponent" + gameObject.name);
        _enemyAtackLayer = LayerMask.GetMask("EnemyAtack");       
        _myTransform = transform;
        //CUIDADO ESTO SOLO FUNCIONA SEGUN LA JERARQUIA
        _playerAtackComponent = _myTransform.GetChild(0).GetComponent<AtackComponent>();
        _playerTeleportComponent = GetComponent<TeleportParry>();
        _playerAnimator = GetComponent<Animator>();

        _parried = false;
        _canParry = true;
        _encontrao = false;

        _parryCurrentTime = _parryTime;

        _boostDamage = _baseDamage * _boostMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        //si estoy en el tiempo del parry y todavía no he parreado
        if(_parryCurrentTime < _parryTime && !_parried)
        {
            //incrementa el contador de tiempo
            _parryCurrentTime += Time.deltaTime;

            //hacemos un overlap con la layer del collider del componente de ataque de los enemigos 
            //(_colisions es un array en el que se guardan las colisiones)
            _colisions = Physics2D.OverlapCircleAll(_myTransform.position, _radius, _enemyAtackLayer);
            
            //si hemos detectado alguna colision, hemos parreado
            if (_colisions.Length > 0)
            {
                ParryEfects();                
            }
            //si no hemos parreado y este era nuestro ultimo update de parry,
            //desactivamos _canParry y empezamos el timer del cooldown
            else if (_parryCurrentTime >= _parryTime)
            {
                _playerAnimator.SetBool("IsParring", false); //revisar de cara a las nueva animaciones
                _parryCooldownCurrentTime = 0;
                _canParry = false;
                //Debug.Log("ISP, FALSE");
            }
        }
        else if(_parryCooldownCurrentTime < _cooldownParryTime)//si tenemos que actualizar el cooldown del parry
        {
            //actualiza el contador del tiempo
            _parryCooldownCurrentTime += Time.deltaTime;
            //si el contador supera el tiempo de cooldown, ya se puede parrear otra vez
            if(_parryCooldownCurrentTime >= _cooldownParryTime)
            {
                _playerAnimator.SetBool("IsParring", false);
                _canParry = true;
            }
        }

        //si tenemos el boost de daño activo
        if (_damageBoosted)
        {
            //incrementamos el contador de tiempo 
            _boostDamageCurrentTime += Time.deltaTime;
            //si el contador supera el tiempo al que tenia que llegar, resetea el daño
            if(_boostDamageCurrentTime > _boostDamageTime)
            {
                ResetDamage();                
            }
        }
    }
    /// <summary>
    /// Realiza la accion del parry, solo si es posible
    /// </summary>
    public void PerformParry()
    {
        AudioManager.Instance.Play("ParrySound");
        if (_canParry)
        {
            _playerAnimator.SetBool("IsParring", true);
            _parryCurrentTime = 0;
            _canParry = false;
        }
    }
    /// <summary>
    /// Todos los efectos y actualizaciones de variables correspontiendes que suceden cuando se realiza un parry
    /// </summary>
    private void ParryEfects()
    {
        _parried = true;
        _canParry = true;
        _damageBoosted = true;

        _parryCurrentTime += _parryTime;
        _boostDamageCurrentTime = 0;

        AudioManager.Instance.Play("CorrectParry");
        _playerAtackComponent.SetDamage(_boostDamage);
        _playerAnimator.SetFloat("Smite",1);
        _playerTeleportComponent.TriggerTeleport();
        _playerAnimator.SetBool("IsFreeze", true);
        _playerAnimator.SetBool("IsParring", false);
        //Debug.Log("is freeze");
        //Time.timeScale = 0;

        _encontrao = SearchCollision();
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
            _damageBoosted = false;
            _playerAnimator.SetFloat("Smite", 0);
        }
    }
    public void SetParryOff()
    {
        _parried = false;
    }

    private bool SearchCollision()
    {
        Debug.Log("tuvieja1");
        int i = 0;
        bool found = false;
        while (i < _colisions.Length && !found)
        {
            if (_colisions[i].transform.parent.gameObject.GetComponent<DashComponent>() != null)
            {
                found = true;
                Debug.Log("tuvieja2");
            }
            else
            {
                i++;
                Debug.Log("tuvieja3");
            }
        }

        return found;
    }

    public void SetEncontrao(bool On)
    {
        _encontrao = On;
    }

    //para verlo to guapo en la escena manin
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_myTransform.position, _radius);
    }
}
