using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DirectionComponent : MonoBehaviour
{

    #region Properties

    private Vector2 directionGizmo;
    private Vector2[] direcciones = { Vector2.right,   new Vector2(1, 1),   Vector2.up, new Vector2(-1, 1),
                                      Vector2.left , new Vector2(-1, -1), Vector2.down, new Vector2(1, -1)};

    private float rotation;
    [SerializeField]
    private float rotationOffSet = 22.5f;

    private Mouse mouse;
    private Gamepad mando;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        mando = Gamepad.current;
        mouse = Mouse.current;
    }

    // Update is called once per frame
    void Update()
    {

        //detects and proccess input logic
        if (mando != null){ directionGizmo = EightAxis(mando.rightStick.ReadValue()); Debug.Log(" mando"); }
        else { directionGizmo = EightAxis(Camera.main.ScreenToWorldPoint( mouse.position.ReadValue())- transform.position); Debug.Log("no mando"); }

    }
    /// <summary>
    /// return the corresponding normalized vector in a 8 axis sistem form a given directional vector (<paramref name="dir"/>)
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public Vector2 EightAxis(Vector2 dir)
    {
        if(mando != null) rotation = Vector2.SignedAngle(Vector2.right,dir) + rotationOffSet;
        else rotation = Vector2.SignedAngle(Vector2.right, dir) + rotationOffSet;

        rotation = (rotation + 360) % 360;

        Vector2 directionGizmo;

        int index = (int)rotation / 45;

        directionGizmo = direcciones[index];
        return directionGizmo.normalized;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, directionGizmo * 5.0f);
    }
}
