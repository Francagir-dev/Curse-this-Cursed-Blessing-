using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class ImageFollower : MonoBehaviour
{
    [Header("Position")]
    public Transform target;
    public Vector3 offset;
    public float speed = .3f;

    [Header("Scale")]
    public bool expand;
    public Vector3 expandedSize = Vector3.one;
    public Vector3 reducedSize = Vector3.zero;
    public bool overrided;
    public Vector3 overridedScale;

    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    void Update()
    {
        Vector3 newScale;
        if (!overrided) newScale = expand ? expandedSize : reducedSize;
        else newScale = overridedScale;
        Vector3 newPos = Camera.main.WorldToScreenPoint(target.position);
        newPos += offset;
        newPos = new Vector3(Mathf.Clamp(newPos.x, 0, Screen.width - rect.rect.width), Mathf.Clamp(newPos.y, 0, Screen.height - rect.rect.height));

        transform.position = Vector3.Lerp(transform.position, newPos, speed);
        transform.localScale = Vector3.Lerp(transform.localScale, newScale, speed);
    }

    public void SetOverride(Vector3 newOver)
    {
        overridedScale = newOver;
        overrided = true;
    }
}
