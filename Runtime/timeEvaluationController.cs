using System;
using System.Collections;
using UnityEngine;

namespace TimeEvaluationUI.Runtime
{
    public class TimeEvaluationController : MonoBehaviour
    {
        [SerializeField] DragDrop dragDrop;
        
        [SerializeField] Feedback feedback;
        
        [SerializeField] GameObject timeEvaluationCanvas;
        
        [SerializeField] GameObject feedbackCanvas;
        
        public int Delay { get; set; }
        
        public int Response { get; set; }
        
        public bool IsPractice { get; set; }
        
        
        public IEnumerator TimeEvaluation()
        {
            dragDrop.ResetSlider();
            timeEvaluationCanvas.SetActive(true);

            yield return new WaitUntil(() => dragDrop.Finished);

            yield return new WaitForSeconds(0.5f);
            
            if (IsPractice)
            {
                feedbackCanvas.SetActive(true);
                var correctPosition = DragDrop.GetPositionOnCircle(Delay / 1000f * 180f);
                var subjectPosition = DragDrop.GetPositionOnCircle(Response / 1000f * 180f);
                feedback.ShowFeedback(correctPosition, subjectPosition);
                
                yield return new WaitForSeconds(2f);
            }
            else
            {
                timeEvaluationCanvas.SetActive(false);
            }
            
            timeEvaluationCanvas.SetActive(false);
            feedbackCanvas.SetActive(false);
        }

    }
}
