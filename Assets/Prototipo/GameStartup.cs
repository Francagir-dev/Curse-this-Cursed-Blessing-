using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    public static GameStartup instance;

    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;
    void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
    }

    public void EndGame(bool win)
    {
        Time.timeScale = 0;

        if (win) this.win.SetActive(true);
        else lose.SetActive(true);
        StartCoroutine(WaitInput());
        IEnumerator WaitInput()
        {
            while (!Input.GetKeyDown(KeyCode.Space))
            {
                yield return null;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}
