using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class AnswerWheel : MonoBehaviour
{
    [System.Serializable]
    struct WheelParts
    {
        public TextMeshProUGUI text;
        public Slider slider;
        public Image image;
    }

    [SerializeField] WheelParts[] wheelParts;

    Coroutine coroutine;
    Enemy answerTo;
    PlayerInput input;

    bool isPressed = false;
    Vector2 selectedOption;

    private void Awake()
    {
        //input = new PlayerInput();
        //input.Enable();
        //input.Player.SelectOption.performed += Option; 
    }

    public void Option(InputAction.CallbackContext cont)
    {
        selectedOption = cont.ReadValue<Vector2>();
    }

    public void OnTrigger(InputAction.CallbackContext cont)
    {
        isPressed = cont.performed;
    }

    public void SetAnswers(string[] answers, float time, Enemy target)
    {
        answerTo = target;
        for (int i = 0; i < wheelParts.Length; i++)
        {
            wheelParts[i].image.fillAmount = 1;
            wheelParts[i].text.text = answers[i];
            wheelParts[i].slider.GoToDestination();
            wheelParts[i].slider.Slide();
        }
        selectedOption = Vector2.zero;

        coroutine = StartCoroutine(Timer());

        IEnumerator Timer()
        {
            ImageFollower playerFollow = Player.instance.playerChoice;
            float remainTime = time;
            //yield return new WaitForSeconds(1);
            while (remainTime > 0)
            {
                for (int i = 0; i < wheelParts.Length; i++)
                {
                    wheelParts[i].image.fillAmount = remainTime / time;
                }

                bool trigger = isPressed;

                if (!trigger) playerFollow.overrided = false;

                if (trigger)
                {
                    playerFollow.SetOverride(Vector3.one);

                    if (selectedOption.y > 0) SelectedAnsw(0);
                    if (selectedOption.x > 0) SelectedAnsw(1);
                    if (selectedOption.y < 0) SelectedAnsw(2);
                    if (selectedOption.x < 0) SelectedAnsw(3);
                }

                remainTime -= Time.deltaTime;
                yield return null;
            }

            for (int i = 0; i < wheelParts.Length; i++)
            {
                wheelParts[i].image.fillAmount = 0;
                wheelParts[i].slider.Slide();
            }
            Player.instance.playerChoice.overrided = false;
            Player.instance.playerChoice.expand = false;
        }

        void SelectedAnsw(int answ)
        {
            selectedOption = Vector2.zero;
            StopCoroutine(coroutine);
            for (int i = 0; i < wheelParts.Length; i++)
            {
                if (i == answ) continue;
                wheelParts[i].slider.Slide();
            }
            StartCoroutine(Wait());

            IEnumerator Wait()
            {
                float time = 1;
                while (time > 0)
                {
                    time -= Time.deltaTime;
                    yield return null;
                }
                answerTo.GetAnswer(wheelParts[answ].text.text);
                Player.instance.playerChoice.overrided = false;
                Player.instance.playerChoice.expand = false;
            }
        }


    }
}
