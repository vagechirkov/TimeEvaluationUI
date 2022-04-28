using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TimeEvaluationUI.Runtime
{
    public class TestExperimentManager : MonoBehaviour
    {
        [SerializeField] TimeEvaluationTask timeEvaluation;

        bool _isPractice, _newToggle;

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
            var colorNormal = Color.black;
            var colorHover = Color.black;
            colorNormal.a = 0.6f;
            colorHover.a = 0.4f;
            guiStyle.normal.textColor = colorNormal;
            guiStyle.hover.textColor = colorHover;
            toggleStyle.onNormal.textColor = colorNormal;
            toggleStyle.normal.textColor = colorNormal;
            toggleStyle.onActive.textColor = colorNormal;
            toggleStyle.active.textColor = colorNormal;
            toggleStyle.onFocused.textColor = colorNormal;
            toggleStyle.focused.textColor = colorNormal;
            toggleStyle.onHover.textColor = colorHover;
            toggleStyle.hover.textColor = colorHover;

            GUI.Label(
                new Rect(20, 25, 300, 20), "Debug Info:", guiStyle);

            GUI.Label(
                new Rect(20, 75, 300, 20),
                "Response: " + (int) timeEvaluation.Response + " ms",
                guiStyle);

            GUI.Label(
                new Rect(20, 125, 300, 20),
                "Delay: " + (int) timeEvaluation.Delay + " ms",
                guiStyle);

            var txt = " practice (toggle " + (_isPractice ? " on)" : " off)");
            _newToggle = GUI.Toggle(
                new Rect(20, 175, 300, 40), _isPractice, txt, toggleStyle);

            //if newToggle != _isPractice restart experiment
            if (_newToggle != _isPractice)
            {
                StopAllCoroutines();
                _isPractice = _newToggle;
                timeEvaluation.IsPractice = _isPractice;
                timeEvaluation.IsSkip = false;
                timeEvaluation.Response = 0f;
                StartCoroutine(RunExperiment());
            }
        }
    }
}