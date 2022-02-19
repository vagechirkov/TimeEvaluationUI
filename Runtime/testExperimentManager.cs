using System;
using System.Collections;
using UnityEngine;

namespace TimeEvaluationUI.Runtime
{
    public class TestExperimentManager : MonoBehaviour
    {
        [SerializeField] DragDrop dragDrop;
        
        
        void Update()
        {
            if (Math.Abs(dragDrop.Response - -1f) > 0.1)
            {
                StartCoroutine(WaitForSeconds(2f));
            }
            
        }
        
        IEnumerator WaitForSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            dragDrop.ResetSlider();
        }

    }
}
