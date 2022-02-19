using System.Collections;
using UnityEngine;

namespace TimeEvaluationUI.Runtime
{
    public class TestExperimentManager : MonoBehaviour
    {
        [SerializeField] TimeEvaluationController timeEvaluation;


        void Start()
        {
            StartCoroutine(RunExperiment());
        }

        IEnumerator RunExperiment()
        {
            while (true)
            {
                timeEvaluation.Delay = Random.Range(100, 950);
                timeEvaluation.IsPractice = true;
                yield return StartCoroutine(timeEvaluation.TimeEvaluation());
            }
        }

    }
}
