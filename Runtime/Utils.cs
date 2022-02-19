using UnityEngine;

namespace TimeEvaluationUI.Runtime
{
    public class Utils
    {
        
        public static Vector2 GetPositionOnCircle(float degrees, float scaleRadius)
        {
            var radians = degrees * Mathf.Deg2Rad;
            var x = Mathf.Cos(radians);
            var y = Mathf.Sin(radians);
            return new Vector2(x, y) * scaleRadius;
        }
        
    }
}