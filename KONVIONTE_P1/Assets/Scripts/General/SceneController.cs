using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region Properties
    //Escena actual
    private Scene _currentScene;

    //�ltimo CheckPoint
    private Transform _checkPointTransform;



    #endregion

    #region Methods
    //Se llama al n�mero de escena asignado en Build Settings
    public void ChangeScene(string sceneName)
    {
        Debug.Log("TuVieja");
        SceneManager.LoadScene(sceneName);

    }
    public void Quit()
    {
        Application.Quit();
    }

    //SetLastCheckpoint()
    //Le pasamos la escena, para que nos lleve al �ltimo checkpoint guardado. 
    #endregion
}
