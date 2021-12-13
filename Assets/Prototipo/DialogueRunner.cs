using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class DialogueRunner
{
    public float time = 5;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] ImageFollower image;

    float timer;

    public void Check()
    {
        if (timer <= 0) return;

        timer -= Time.deltaTime;
        if (timer <= 0) 
            image.expand = false;
        else if (!image.expand) 
            image.expand = true;
    }

    public void SetText(string newText)
    {
        text.text = newText;
        timer = time;
    }

    public void Stop()
    {
        timer = 0;
        image.expand = false;
    }
}
