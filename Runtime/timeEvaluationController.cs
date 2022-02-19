using System;
using System.Collections;
using UnityEngine;

namespace TimeEvaluationUI.Runtime
{
    public class TimeEvaluationController : MonoBehaviour
    {
        [SerializeField] DragDrop dragDrop;
        
        [SerializeField] GameObject timeEvaluationCanvas;
        
        // [SerializeField] GameObject Feedback;
        
        public int Delay { get; set; }
        
        public int Response { get; set; }
        
        public bool IsPractice { get; set; }
        
        
        IEnumerator timeEvaluation()
        {
            dragDrop.ResetSlider();
            timeEvaluationCanvas.SetActive(true);

            yield return new WaitUntil(() => Math.Abs(dragDrop.Response - -1f) > 0.1);

            if (IsPractice)
                // int rr = 4;
            else
                timeEvaluationCanvas.SetActive(false);
           
        }

    }
}
