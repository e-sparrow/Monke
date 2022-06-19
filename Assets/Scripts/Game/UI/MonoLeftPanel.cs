using System;
using System.Collections.Generic;
using ESparrow.Utils.Extensions;
using ESparrow.Utils.Generic.Pairs;
using ESparrow.Utils.Generic.Pairs.Interfaces;
using Game.Counting.Interfaces;
using Game.Counting.Interfaces.Enums;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class MonoLeftPanel : MonoBehaviour
    {
        [SerializeField] private List<SerializablePair<ECountType, TextInfo>> texts;

        private ICounterContainer _counterContainer;
        
        [Inject]
        private void Construct(ICounterContainer counterContainer)
        {
            _counterContainer = counterContainer;
        }

        private void OnEnable()
        {
            var dictionary = texts.SelectBase<SerializablePair<ECountType, TextInfo>, IPair<ECountType, TextInfo>>();
            foreach (var pair in dictionary)
            {
                var counter = _counterContainer.GetCounter(pair.Key);
                counter.OnValueChanged += ChangeText;
                
                ChangeText(counter.Value);

                void ChangeText(int value)
                {
                    pair.Value.Text.text = pair.Value.StartString + value;
                }
            }
        }
        
        [Serializable]
        private struct TextInfo
        {
            [field: SerializeField]
            public TMP_Text Text
            {
                get;
                private set;
            }

            [field: SerializeField]
            public string StartString
            {
                get;
                private set;
            }
        }
    }
}