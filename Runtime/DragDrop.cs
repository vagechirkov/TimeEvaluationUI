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
        Image _image;

        // Radius of the protractor
        const float PositionPrecisionError = 60f;

        Vector2 StartPosition { get; set; }
        
        public float ScaleRadius { get; set; } = 640f;

        public float Response { get; private set; }
        
        public bool Finished { get; private set; }

        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            StartPosition = transform.localPosition;
            _image = gameObject.GetComponent<Image>();
            _image.color = Color.red;
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
            var (magnitude, angle) = EstimateAngleAndMagnitude();
            var diff = ScaleRadius - magnitude;
            if (diff < PositionPrecisionError && diff > 0 && angle < 180f && angle > 0)
                _image.color = Color.blue;
            else
                _image.color = Color.red;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 1f;
            var (magnitude, angle) = EstimateAngleAndMagnitude();
            var diff = ScaleRadius - magnitude;
            if (diff < PositionPrecisionError && diff > 0 && angle < 180f && angle > 0)
            {
                _image.color = Color.blue;
                Response = angle / 180f * 1000f;
                // add 180 degrees because 0 is to the left in EstimateAngleAndMagnitude
                _rectTransform.anchoredPosition = StartPosition + Utils.GetPositionOnCircle(angle + 180f, ScaleRadius);
                Finished = true;
            }
            else
            {
                _canvasGroup.blocksRaycasts = true;
                _rectTransform.anchoredPosition = StartPosition;
            }
        }

        // return angle and magnitude
        Tuple<float, float> EstimateAngleAndMagnitude()
        {
            var circlePosition = (Vector2) transform.localPosition;
            var diff = circlePosition - StartPosition;
            var angle = Vector2.SignedAngle(Vector2.left, diff.normalized);
            var magnitude = diff.magnitude;

            return new Tuple<float, float>(magnitude, angle);
        }
        

        public void ResetSlider()
        {
            transform.localPosition = StartPosition;
            _image.color = Color.red;
            _canvasGroup.blocksRaycasts = true;
            Response = 0;
            Finished = false;
        }
    }
}