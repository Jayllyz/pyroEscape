using System.Collections;
using UnityEditor;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEngine;
#endif

public class MenuScript : MonoBehaviour
{
    public GameObject mainMenuRoot;
    public Button startGameButton;
    public Button quitGameButton;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
        startGameButton.onClick.AddListener(StartGame);
        quitGameButton.onClick.AddListener(QuitGame);
    }

    private void StartGame()
    {
        mainMenuRoot.SetActive(false);
        StartCoroutine(DelayStartGame());
        Cursor.visible = false;
    }

    private IEnumerator DelayStartGame()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1.0f;
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}