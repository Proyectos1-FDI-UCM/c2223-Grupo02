using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ESE SCRIPT VA ATACHADO A LA CURA WAPA

public class HealingComponent : MonoBehaviour
{
    private LifeComponent _playerLifeComponent;
    [SerializeField] private int _lifeHeal;
    [SerializeField] private LayerMask _playerLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el objeto colisionado tiene el Parry (asi solo cura al jugador)
        if (collision.gameObject.GetComponent<ParryComponent>() != null)
        {
            // Cogemos el LifeComponent
            _playerLifeComponent = collision.gameObject.GetComponent<LifeComponent>();
            // Llamamos al metodo de curar
            _playerLifeComponent.HealLife(_lifeHeal);
            // Se destruye la cura
            Destroy(gameObject);
        }
    }
}
