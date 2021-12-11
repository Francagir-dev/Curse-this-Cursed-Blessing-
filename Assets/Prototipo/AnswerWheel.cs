using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

                float trigger = Input.GetAxis("Trigger");
                if (trigger <= 0) playerFollow.overrided = false;

                if (trigger >= 1)
                {
                    playerFollow.SetOverride(Vector3.one);

                    if (Input.GetButtonDown("Y")) SelectedAnsw(0);
                    if (Input.GetButtonDown("B")) SelectedAnsw(1);
                    if (Input.GetButtonDown("A")) SelectedAnsw(2);
                    if (Input.GetButtonDown("X")) SelectedAnsw(3);
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
