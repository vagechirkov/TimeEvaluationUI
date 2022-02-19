using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TimeEvaluationUI.Runtime
{
    public class TestExperimentManager : MonoBehaviour
    {
        [SerializeField] TimeEvaluationTask timeEvaluation;

        readonly GUIStyle _guiStyle = new GUIStyle();

        void Start()
        {
            _guiStyle.fontSize = 30;
            var color = Color.black;
            color.a = 0.5f;
            _guiStyle.normal.textColor = color;
            StartCoroutine(RunExperiment());
        }

        IEnumerator RunExperiment()
        {
            while (true)
            {
                timeEvaluation.Delay = Random.Range(100, 950);
                timeEvaluation.IsPractice = true;
                yield return StartCoroutine(timeEvaluation.TimeEvaluation());
                timeEvaluation.Response = 0;
            }
        }

        void OnGUI()
        {
            GUI.Label(
                new Rect(50, 25, 300, 20), "Debug info:", _guiStyle);

            GUI.Label(
                new Rect(50, 75, 300, 20),
                "Response: " + (int) timeEvaluation.Response + " ms",
                _guiStyle);

            GUI.Label(
                new Rect(50, 125, 300, 20),
                "Delay: " + (int) timeEvaluation.Delay + " ms",
                _guiStyle);
        }
    }
}
