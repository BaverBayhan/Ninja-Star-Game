using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_management : MonoBehaviour
{
    public void LoadGameFromMenu()
    {
        SceneManager.LoadScene("game", LoadSceneMode.Single);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
