using System;
using UnityEngine;
using UnityEngine.UI;

namespace TimeEvaluationUI.Runtime
{
    public class Feedback : MonoBehaviour
    {
        [SerializeField] RectTransform subjectResponse;
        
        [SerializeField] RectTransform correctResponse;
        
        [SerializeField] Button nextButton;

        public bool Continue { get; set; }
        
        Vector2 StartPosition { get; set; }

        void Awake()
        {
            StartPosition = subjectResponse.gameObject.transform.localPosition;
            nextButton.onClick.AddListener(continueButtonOnClick);
        }
        
        public void ShowFeedback(Vector2 correct, Vector2 subject)
        {
            correctResponse.anchoredPosition = StartPosition + correct;
            subjectResponse.anchoredPosition = StartPosition + subject;
            Continue = false;
        }
        
        public void ResetFeedback()
        {
            correctResponse.anchoredPosition = StartPosition;
            subjectResponse.anchoredPosition = StartPosition;
        }
        
        void continueButtonOnClick()
        {
            Continue = true;
        }
        
    }
}
