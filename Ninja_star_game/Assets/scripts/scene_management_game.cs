using UnityEngine.SceneManagement;
using UnityEngine;

public class scene_management_game : MonoBehaviour
{
    user_controller uc;
    private void Awake()
    {
        uc = FindObjectOfType<user_controller>();
    }
    private void Update()
    {
        IsGameEnded();
    }
    public void IsGameEnded()
    {
        if (uc.health<=0.06)
        {
            SceneManager.LoadScene("menu", LoadSceneMode.Single);
        }
    }
}
