using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Director : MonoBehaviour
{
    private Vector2 directionGizmo;
    public Vector2 DirectionGizmo;
    [SerializeField]
    private Vector2 directionInput;

    private Vector2[] direcciones = { Vector2.right, new Vector2(1, 1), Vector2.up, new Vector2(-1, 1), Vector2.left, new Vector2(-1, -1), Vector2.down, new Vector2(1, -1) }; 

    public float rotation;
    private float rotationOffSet = 22.5f;
    private Mouse mouse;
    private Gamepad mando;

    // Start is called before the first frame update
    void Start()
    {
        mando = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {
        mouse = Mouse.current;
        Debug.Log("HolaMundo");
            //detects and proccess input logic
        directionGizmo = EightAxis(mando.leftStick.ReadValue());

    }
    /// <summary>
    /// return the corresponding normalized vector in a 8 axis sistem form a given directional vector (<paramref name="dir"/>)
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public Vector2 EightAxis(Vector2 dir)
    {
        rotation = Vector2.SignedAngle(Vector2.right, mando.leftStick.ReadValue()) + rotationOffSet;

        rotation = (rotation + 360) % 360;

        Vector2 directionGizmo;

        int index = (int) rotation / 45;

        directionGizmo = direcciones[index];
        return directionGizmo.normalized;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, directionGizmo*5.0f);
    }

}
