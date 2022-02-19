using System;
using UnityEngine;

namespace TimeEvaluationUI.Runtime
{
    public class Feedback : MonoBehaviour
    {
        [SerializeField] RectTransform spriteSubjectResponse;
        
        [SerializeField] RectTransform spriteCorrectResponse;
        
        Vector2 StartPosition { get; set; }

        void Awake()
        {
            StartPosition = spriteSubjectResponse.gameObject.transform.localPosition;
        }
        
        public void ShowFeedback(Vector2 correct, Vector2 subject)
        {
            spriteCorrectResponse.anchoredPosition = StartPosition + correct;
            spriteSubjectResponse.anchoredPosition = StartPosition + subject;
        }
        
        public void ResetFeedback()
        {
            spriteCorrectResponse.anchoredPosition = StartPosition;
            spriteSubjectResponse.anchoredPosition = StartPosition;
        }
    }
}
