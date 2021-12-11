using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    [SerializeField] bool goToOrigin;
    public float speed = .4f;
    public bool inOrigin;
    [SerializeField] bool setOrigin;
    public Vector3 origin;
    [SerializeField] bool setDestination;
    public Vector3 destination;

    Vector3 target;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.localPosition, target) <= .01f) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, speed);
    }
    
    public void Slide()
    {
        inOrigin = !inOrigin;
        if (inOrigin) target = origin;
        else target = destination;
    }

    public void GoToOrigin()
    {
        transform.localPosition = origin;
        inOrigin = true;
    }

    public void GoToDestination()
    {
        transform.localPosition = destination;
        inOrigin = false;
    }

    private void OnValidate()
    {

        if (setOrigin)
        { origin = transform.localPosition; setOrigin = false; }
        if (setDestination)
        { destination = transform.localPosition; setDestination = false; }
        if (goToOrigin)
        { transform.localPosition = origin; goToOrigin = false; inOrigin = true; }
    }
}
