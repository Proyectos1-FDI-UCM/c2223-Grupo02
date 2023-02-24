using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Smite : MonoBehaviour
{
    #region references
    private ParryComponent _parryComponent;

    #endregion

    #region Parameters

    [Tooltip("Duración")]
    [SerializeField] private float _time = 10;

    [Tooltip("Objeto que aparece")]
    [SerializeField] private Image _object;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (_parryComponent._damageBoosted == true)
        {
            _object = GameObject.Find("Smite").GetComponent<Image>();
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        //Si se hace el parry se muestra en el hud el smite
            
            _time -= Time.deltaTime;

            //Cuando termine el tiempo del smite quitamos el símbolo
            if (_time < 0)
            {
                Destroy(this.gameObject);
            }
       
       
    }
}
