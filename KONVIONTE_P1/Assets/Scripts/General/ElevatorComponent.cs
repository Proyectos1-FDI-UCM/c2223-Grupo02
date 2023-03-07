using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ParryComponent>() != null)
        {
            SceneManager.LoadScene("HUDmainMenu");
        }
    }
}
