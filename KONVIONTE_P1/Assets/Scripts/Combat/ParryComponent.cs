using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryComponent : MonoBehaviour
{
    #region Parameters

    [SerializeField]
    private float _radius;


    #endregion

    #region Properties

    private LayerMask _enemyAtackLayer;

    private Vector2 _origin;

    private Collider2D[] _colisions;



    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _enemyAtackLayer = LayerMask.GetMask("EnemyAtack");
        _origin = transform.position;        
    }

    // Update is called once per frame
    void Update()
    {
        //metodo de prueba
        //hacemos un overlap constantemente con la layer del collider de los enemigos y si detectamos algo, hemos parreado
        //mover posteriormente a la funcion parry
        _colisions = Physics2D.OverlapCircleAll(_origin, _radius, _enemyAtackLayer);
        
        if(_colisions.Length > 0)
        {
            Debug.Log("he parreado");
        }
    }
    
    public void Parry()
    {


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_origin, _radius);
    }
}
