using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TimeEvaluationUI.Runtime
{
    public class TimeEvaluationTask : MonoBehaviour
    {
        [SerializeField] DragDrop dragDrop;

        [SerializeField] Feedback feedback;

        [SerializeField] GameObject timeEvaluationCanvas;

        [SerializeField] GameObject feedbackCanvas;

        // Serialize field for the skip button
        [SerializeField] Button skipButton;

        public float Delay { get; set; }

        public float Response { get; set; }

        public bool IsPractice { get; set; }

        // Skip button is pressed
        bool IsSkip { get; set; }

        const float ProtractorRadius = 640f;

        void Awake()
        {
            dragDrop.ScaleRadius = ProtractorRadius;
            skipButton.gameObject.SetActive(false);
            skipButton.onClick.AddListener(() => IsSkip = true);
        }

        public IEnumerator TimeEvaluation()
        {
            timeEvaluationCanvas.SetActive(true);

            // Add skip button if not practice
            if (!IsPractice) skipButton.gameObject.SetActive(true);

            // Wait until dragDrop is done or skip button is pressed
            while (!dragDrop.Finished && !IsSkip) yield return null;

            // -1 means skip
            Response = IsSkip ? -1f : dragDrop.Response;
            IsSkip = false;

            yield return new WaitForSeconds(0.5f);
            dragDrop.ResetSlider();
            timeEvaluationCanvas.SetActive(false);
            skipButton.gameObject.SetActive(false);

            if (!IsPractice) yield break;

            feedbackCanvas.SetActive(true);
            var correctPosition = Utils.GetPositionOnCircle(Delay / 1000f * 180f + 180f, ProtractorRadius);
            var subjectPosition = Utils.GetPositionOnCircle(Response / 1000f * 180f + 180f, ProtractorRadius);
            feedback.ShowFeedback(correctPosition, subjectPosition);
            yield return new WaitUntil(() => feedback.Continue);
            yield return new WaitForSeconds(0.5f);
            feedbackCanvas.SetActive(false);
            feedback.ResetFeedback();
        }
    }
}