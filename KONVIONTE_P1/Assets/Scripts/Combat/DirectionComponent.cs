using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//ESTE SCRIPT VA ATACHADO AL OBJETO PLAYER
public class DirectionComponent : MonoBehaviour
{

    #region Properties
    private Vector2 _directionGizmo;
    
    private float rotation;
    
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
        if (mando != null)
        {           
            _directionGizmo = X_Directions(mando.rightStick.ReadValue(), 8);

            //Debug.Log(" mando"); 
        }
        else
        {
            _directionGizmo = X_Directions(Camera.main.ScreenToWorldPoint(mouse.position.ReadValue()) - transform.position,8);
            
            //Debug.Log("no mando"); 
        }

    }
    /// <summary>
    /// return the corresponding normalized vector in a 8 axis sistem form a given directional vector (<paramref name="dir"/>)
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>   
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, _directionGizmo * 5.0f);
    }
    public Vector3 X_Directions(Vector2 dir,int n)
    {
        float offset = 360 / (2 * n);

        rotation = Vector2.SignedAngle(Vector2.right, dir) + offset;        
        //para quitar los angulos negativos
        rotation = (rotation + 360) % 360;

        //indice del tramo en el que estamos
        int indice = (int)rotation / (360 / n);

        //pasamos el indice a grados
        rotation = indice * (360 / n);

        //pasamos los grados a radianes
        rotation = rotation * Mathf.Deg2Rad;
        
        return new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation)).normalized;                             
    }
}
