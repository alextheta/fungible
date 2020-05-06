using UnityEngine;

namespace Fungible.Environment
{
    [RequireComponent(typeof(ObjectActivator))]
    public class ItemObjectActivator : ItemDrivenObject
    {
        protected override void OnClick()
        {
            GetComponent<ObjectActivator>().Invoke();
        }
    }
}