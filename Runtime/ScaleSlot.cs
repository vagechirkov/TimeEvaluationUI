using UnityEngine;
using UnityEngine.EventSystems;

namespace TimeEvaluationUI.Runtime
{
    public class ScaleSlot : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        { 
            if (eventData.pointerDrag != null)
            {
                Debug.Log("OnDrop");
                // eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }
}
