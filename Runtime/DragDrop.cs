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
        const float ScaleRadius = 640f;
        const float PositionPrecisionError = 20f;

        Vector2 StartPosition { get; set; }

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

            if (Mathf.Abs(magnitude - ScaleRadius) < PositionPrecisionError && angle < 180f && angle > 0)
                _image.color = Color.blue;
            else
                _image.color = Color.red;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 1f;
            var (magnitude, angle) = EstimateAngleAndMagnitude();
            if (Mathf.Abs(magnitude - ScaleRadius) < PositionPrecisionError && angle < 180f && angle > 0)
            {
                _image.color = Color.green;
                Response = angle / 180f * 1000f;
                // add 180 degrees because 0 is to the left in EstimateAngleAndMagnitude
                _rectTransform.anchoredPosition = StartPosition + GetPositionOnCircle(angle + 180f);
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

        public static Vector2 GetPositionOnCircle(float degrees)
        {
            var radians = degrees * Mathf.Deg2Rad;
            var x = Mathf.Cos(radians);
            var y = Mathf.Sin(radians);
            return new Vector2(x, y) * ScaleRadius;
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