using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TimeEvaluationUI.Runtime
{
    public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] Canvas canvas;
        
        RectTransform _rectTransform;
        CanvasGroup _canvasGroup;
        
        // Radius of the protractor
        const float ScaleRadius = 228f;
        
        Vector2 StartPosition { get; set; }
        bool IsOnScale { get; set; }

        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            StartPosition = transform.localPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = .6f;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 1f;
            
            var circlePosition = (Vector2) transform.localPosition;
            var diff = circlePosition - StartPosition;
            var angle = Vector2.Angle(diff.normalized, Vector2.down);
            
            if (Mathf.Abs(diff.magnitude - ScaleRadius) < 10f && angle < 90f)
            {
                var circleImage = gameObject.GetComponent<Image>();
                circleImage.color = Color.green;
            }
            else
            {
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
                
            Debug.Log("Angle: " + angle);
            Debug.Log("Dist: " + diff.magnitude);
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

    }
}
