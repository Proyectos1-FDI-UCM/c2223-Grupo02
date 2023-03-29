using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

//ESTE SCRIPT VA ATTACHADO TANTO AL JUGADOR COMO A LOS ENEMIGOS
public class LifeComponent : MonoBehaviour
{
    #region Parameters
    [SerializeField] private int _maxLife;
    Animator _myAnimator;

    [SerializeField] private ParticleSystem SuperHit;
    #endregion

    #region Properties

    private int _life;
    /* Relacionado a KnockbackComponent.
    * Si false, todo ocurre normal.
    * Si true, la vida no baja
    */
    private bool _immortal = false;

    #endregion

    #region Accesor

    public int CurrentLife { get { return _life; }}
    public int MaxLife { get { return _maxLife; }}
    public bool Immortal { get { return _immortal; }}

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myAnimator = GetComponent<Animator>();
        _life = _maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Le resta a <paramref name="_life"/> el <paramref name="damage"/> correspondiente
    /// </summary>
    /// <param name="damage"></param>
    public void ReciveDamage(int damage)
    {
        if (GetComponent<ParryComponent>() != null)
        {
            if(GameManager.Instance.AudioManager != null) {
                GameManager.Instance.AudioManager.Play("PlayerHit"); }  
            
            
        }
        //Si moñeco no inmortal, recibe daño
        if (!_immortal)
        {
            _life -= damage;
            if (SuperHit != null)
            {
                SuperHit.Play();
            }
            

            //Si recibe daño mortal muere, logicamente
            if (_life <= 0)
            {
                Death();
            }
        }
    }
    /// <summary>
    /// Se encarga de curar la vida al jugador cuando coge una jeringa
    /// </summary>
    /// <param name="healing"></param>
    public void HealLife(int healing)
    {
        // Si la cura le suma por encima del limite
        if (_life + healing > _maxLife)
        {
            _life = _maxLife; // La vida al maximo
        }
        else
        {
            _life += healing; // Si no le curamos lo estipulado
        }
    }
    /// <summary>
    /// Cambia el valor del booleano de inmortal
    /// </summary>
    public void SetInmortal(bool On)
    {
        _immortal = On;
    }
    /// <summary>
    /// Destruye el <paramref name="gameObject"/> y activa la animacion
    /// </summary>
    private void Death()
    {
        //No se activa la animación de muerte
        _myAnimator.SetTrigger("Die");
        if(GetComponent<ParryComponent>() != null)
        {
            GameManager.Instance.ResetLevel();
        }
        gameObject.SetActive(false);
    }
}
