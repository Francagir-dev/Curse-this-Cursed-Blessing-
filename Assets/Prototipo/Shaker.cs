using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public float time;
    public float magnitude;

    public bool lockX = false;
    public bool lockY = false;
    public bool lockZ = false;

    float timeLeft;
    Vector3 origPos;

    public void BeginShake()
    {
        timeLeft = time;
        origPos = transform.position;
    }

    private void Update()
    {
        if (timeLeft <= 0) return;

        Vector3 newPos = new Vector3(origPos.x, origPos.y, origPos.z);

        if (!lockX) newPos += Vector3.right * Mathf.Lerp(0, Random.Range(magnitude, -magnitude), timeLeft / time);
        if (!lockY) newPos += Vector3.up * Mathf.Lerp(0, Random.Range(magnitude, -magnitude), timeLeft / time);
        if (!lockZ) newPos += Vector3.forward * Mathf.Lerp(0, Random.Range(magnitude, -magnitude), timeLeft / time);

        transform.position = newPos;

        timeLeft -= Time.deltaTime;
    }
}
