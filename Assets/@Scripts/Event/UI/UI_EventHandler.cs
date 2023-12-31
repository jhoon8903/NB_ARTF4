using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Scripts.Event.UI.UIEventType;

namespace Scripts.Event.UI
{
    public enum UIEventType
    {
        Click, PointerEnter, PointerExit
    }
    public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private Dictionary<UIEventType, Action<PointerEventData>> EventHandlers = new();

        private void InvokeEventAction(UIEventType eventType, PointerEventData eventData)
        {
            if (EventHandlers.TryGetValue(eventType, out var action)) action?.Invoke(eventData);
        }

        public void BindEvent(UIEventType eventType, Action<PointerEventData> action)
        {
            EventHandlers[eventType] = action;
        }

        public void UnbindEvent(UIEventType eventType)
        {
            if (EventHandlers.ContainsKey(eventType))
            {
                EventHandlers.Remove(eventType);
            }
        }

        public void OnPointerClick(PointerEventData eventData) => InvokeEventAction(Click, eventData);
        public void OnPointerEnter(PointerEventData eventData) => InvokeEventAction(PointerEnter, eventData);
        public void OnPointerExit(PointerEventData eventData) => InvokeEventAction(PointerExit, eventData);

        private void OnDestroy()
        {
            EventHandlers.Clear();
        }
    }
}