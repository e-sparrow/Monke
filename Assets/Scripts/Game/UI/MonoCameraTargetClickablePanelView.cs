using System;
using Game.Camera.Enums;
using Game.UI.Enums;
using Game.UI.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.UI
{
    public class MonoCameraTargetClickablePanelView : MonoBehaviour, IClickablePanelView
    {
        public event Action OnClick = () => { };

        [SerializeField] private EventTrigger eventTrigger;

        private EventTrigger.Entry _entry;

        private IClickablePanelSystem _clickablePanelSystem;

        [Inject]
        private void Construct(IClickablePanelSystem clickablePanelSystem)
        {
            _clickablePanelSystem = clickablePanelSystem;
        }
        
        private void Awake()
        {
            _entry = new EventTrigger.Entry();
            
            _entry.eventID = EventTriggerType.PointerClick;
            _entry.callback.AddListener(Trigger);
        }

        private void OnEnable()
        {
            eventTrigger.triggers.Add(_entry);
            _clickablePanelSystem.Register(this);
        }

        private void OnDisable()
        {
            eventTrigger.triggers.Remove(_entry);
            _clickablePanelSystem.Unregister(this);
        }

        private void Trigger(BaseEventData data)
        {
            OnClick.Invoke();
        }

        public Vector3 Position => transform.position;

        [field: SerializeField]
        public EClickablePanelType Type
        {
            get;
            private set;
        }

        [field: SerializeField]
        public ECameraTargetType CameraTargetType
        {
            get;
            private set;
        }
    }
}