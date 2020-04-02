using System;
using Fungible.Environment;
using UnityEngine;

namespace Fungible.Storytelling
{
    [RequireComponent(typeof(ClickableObject))]
    public class StoryLabelObject : StoryLabelBaseObject
    {
        private void OnClick()
        {
            Show();
        }
        
        private void Awake()
        {
            GetComponent<ClickableObject>().ClickEvent += OnClick;
        }

        private void OnDestroy()
        {
            GetComponent<ClickableObject>().ClickEvent -= OnClick;
        }
    }
}