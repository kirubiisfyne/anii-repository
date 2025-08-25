using UnityEngine;
using UnityEngine.SceneManagement; 

public class TitleScreen : MonoBehaviour
{
    public void StartGame()
    {

        Debug.Log("Start button was clicked!"); 
        SceneManager.LoadSceneAsync(1);
    }
}