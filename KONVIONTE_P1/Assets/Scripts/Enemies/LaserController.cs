using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    private AtackComponent _myAtackComponent;

    // Start is called before the first frame update
    void Start()
    {
        _myAtackComponent = GetComponent<AtackComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _myAtackComponent.TryAplyDamage();
    }
}
