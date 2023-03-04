using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleComponent : MonoBehaviour
{
    #region References
    private LifeComponent _myLifeComponent;
    #endregion

    #region Parameters
    [Tooltip("Daño al tocar")]
    [SerializeField] private int _damage;
    #endregion

    #region Methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _myLifeComponent.ReciveDamage(_damage);
    }


    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _myLifeComponent = GetComponent<LifeComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
