using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GotoGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GotoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
