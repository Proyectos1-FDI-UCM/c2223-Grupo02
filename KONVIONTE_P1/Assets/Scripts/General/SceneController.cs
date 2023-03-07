using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region Methods
    //Se llama al número de escena asignado en Build Settings
    public void ChangeScene(string sceneName)
    {
        Debug.Log("TuVieja");
        SceneManager.LoadScene(sceneName);

    }
    public void Quit()
    {
        Application.Quit();
    }
    #endregion
}
