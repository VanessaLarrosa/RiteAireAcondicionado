using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HideShowButton : MonoBehaviour, IPointerDownHandler
{

    public bool isHidden;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private Vector3 endPosition;


    // When clicking, it lerps the position to show and hide the task list
    public void OnPointerDown(PointerEventData eventData)
    {
        endPosition.x *= -1;
        gameObject.SetActive(!isHidden);

        Rect uvRect = transform.Find("HideShowArrow").GetComponentInChildren<RawImage>().uvRect;
        uvRect.width *= -1;
        transform.Find("HideShowArrow").GetComponentInChildren<RawImage>().uvRect = uvRect;

        StartCoroutine(LerpPosition(transform.parent.position, transform.parent.position + endPosition, 0.2f));
    }

    // Coroutine for lerping the positions
    IEnumerator LerpPosition(Vector3 start, Vector3 target, float lerpDuration)
    {
        float timeElapsed = 0f;

        while(timeElapsed < lerpDuration)
        {
            transform.parent.position = Vector3.Lerp(start, target, curve.Evaluate(timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.parent.position = target;

        
    }

}
