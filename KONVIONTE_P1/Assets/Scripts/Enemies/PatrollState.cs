using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollState : State
{
    Transform myTransform;
    public void OnEnter()
    {

    }
    public void Tick()
    {

    }
    public void OnExit()
    {

    }

    //Constructor de la clase
    public PatrollState(Transform myTransform)
    {
        this.myTransform = myTransform;
    }


}
