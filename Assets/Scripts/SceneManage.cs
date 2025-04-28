using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void StartScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
