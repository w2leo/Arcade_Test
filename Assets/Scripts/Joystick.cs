using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private float handleRange = 1;
    [SerializeField] private float deadZone = 0;
    [SerializeField] protected RectTransform background = null;
    [SerializeField] private RectTransform handle = null;

    private Canvas canvas;
    private Camera cam;
    private Vector2 input;

    public float Horizontal => input.x;
    public float Vertical => input.y;
    public Vector2 Direction2D => new Vector2(Horizontal, Vertical);
    public Vector3 Direction3d => new Vector3(Horizontal, 0, Vertical);


    private void Awake()
    {
        input = Vector2.zero;
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
            Debug.LogError("The Joystick is not placed inside a canvas");
        
        cam = canvas.worldCamera;
        Vector2 center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        Vector2 radius = background.sizeDelta / 2;
        input = (eventData.position - position) / (radius * canvas.scaleFactor);
        HandleInput(input.magnitude, input.normalized, radius);
        handle.anchoredPosition = input * radius * handleRange;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    private void HandleInput(float magnitude, Vector2 normalised, Vector2 radius)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
                input = normalised;
        }
        else
            input = Vector2.zero;
    }
}
