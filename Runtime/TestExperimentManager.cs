using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TimeEvaluationUI.Runtime
{
    public class TestExperimentManager : MonoBehaviour
    {
        [SerializeField] TimeEvaluationTask timeEvaluation;
        
        bool _isPractice;

        void Start()
        {
            StartCoroutine(RunExperiment());
        }

        IEnumerator RunExperiment()
        {
            while (true)
            {
                timeEvaluation.Delay = Random.Range(100, 950);
                yield return StartCoroutine(timeEvaluation.TimeEvaluation());
                timeEvaluation.Response = 0;
            }
        }

        void OnGUI()
        {
            var guiStyle = new GUIStyle();
            var toggleStyle = new GUIStyle(GUI.skin.toggle);
            
            guiStyle.fontSize = 30;
            toggleStyle.fontSize = 30;
            var color = Color.black;
            color.a = 0.5f;
            guiStyle.normal.textColor = color;


            GUI.Label(
                new Rect(50, 25, 300, 20), "Debug info", guiStyle);

            GUI.Label(
                new Rect(50, 75, 300, 20),
                "Response: " + (int) timeEvaluation.Response + " ms",
                guiStyle);

            GUI.Label(
                new Rect(50, 125, 300, 20),
                "Delay: " + (int) timeEvaluation.Delay + " ms",
                guiStyle);
            
            _isPractice = GUI.Toggle(
                new Rect(50, 175, 300, 40), 
                _isPractice, "Practice", toggleStyle);
            timeEvaluation.IsPractice = _isPractice;
        }
        
    }
}
