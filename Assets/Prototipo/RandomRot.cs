using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRot : MonoBehaviour
{
    float ind = 0;
    int mult = 1;
    float origVec;
    private void Awake()
    {
        origVec = transform.localEulerAngles.y;
    }
    private void Update()
    {
        transform.localEulerAngles = transform.up * Mathf.Lerp(origVec - 10, origVec + 10, ind);
        ind += Time.deltaTime * mult;

        if (ind > 1) mult = -1;
        else if (ind < 0) mult = 1;
    }
}
