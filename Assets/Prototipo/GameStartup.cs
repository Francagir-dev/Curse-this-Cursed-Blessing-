using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStartup : MonoBehaviour
{
    public static GameStartup instance;

    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;

    PlayerInput input;
    bool pressed;
    void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
    }

    public void Pressed(InputAction.CallbackContext cont)
    {
        pressed = cont.performed;
    }

    public void EndGame(bool win)
    {
        Time.timeScale = 0;

        if (win) this.win.SetActive(true);
        else lose.SetActive(true);
        StartCoroutine(WaitInput());
        IEnumerator WaitInput()
        {
            while (!pressed)
            {
                yield return null;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}
