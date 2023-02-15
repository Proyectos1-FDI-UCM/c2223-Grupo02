using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    #region Parameters
    [SerializeField]
    private int _life;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
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
        _life -= damage;
    }

}
