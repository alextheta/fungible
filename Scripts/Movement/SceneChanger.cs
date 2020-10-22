using Fungible.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungible.Movement
{
    public class SceneChanger : BaseEventListener
    {
        [SerializeField] private string sceneMoveTo;

        public override void Event()
        {
            SceneManager.LoadScene(sceneMoveTo);
        }
    }
}