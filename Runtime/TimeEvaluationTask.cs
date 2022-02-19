using System;
using System.Collections;
using UnityEngine;

namespace TimeEvaluationUI.Runtime
{
    public class TimeEvaluationTask : MonoBehaviour
    {
        [SerializeField] DragDrop dragDrop;
        
        [SerializeField] Feedback feedback;
        
        [SerializeField] GameObject timeEvaluationCanvas;
        
        [SerializeField] GameObject feedbackCanvas;

        public float Delay { get; set; }
        
        public float Response { get; set; }
        
        public bool IsPractice { get; set; }
        
        const float ProtractorRadius = 640f;

        void Awake()
        {
            dragDrop.ScaleRadius = ProtractorRadius;
        }


        public IEnumerator TimeEvaluation()
        {
            timeEvaluationCanvas.SetActive(true);

            yield return new WaitUntil(() => dragDrop.Finished);
            Response = dragDrop.Response;

            yield return new WaitForSeconds(1f);
            dragDrop.ResetSlider();
            timeEvaluationCanvas.SetActive(false);

            if (!IsPractice) yield break;
            
            feedbackCanvas.SetActive(true);
            var correctPosition = Utils.GetPositionOnCircle((Delay / 1000f) * 180f + 180f, ProtractorRadius);
            var subjectPosition = Utils.GetPositionOnCircle((Response / 1000f) * 180f + 180f, ProtractorRadius);
            feedback.ShowFeedback(correctPosition, subjectPosition);
            yield return new WaitUntil(() => feedback.Continue);
            yield return new WaitForSeconds(0.5f);
            feedbackCanvas.SetActive(false);
            feedback.ResetFeedback();
        }

    }
}
