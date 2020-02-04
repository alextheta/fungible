using Fungible.Movement;
using UnityEditor;
using UnityEngine;

namespace Fungible.Editor
{
    //[CustomEditor(typeof(Map))]
    public class MapEditor : UnityEditor.Editor
    {
        private GUIStyle buttonStyle;
        private bool enableToogle;

        /*public override void OnInspectorGUI()
        {
            buttonStyle = new GUIStyle(GUI.skin.button);
            enableToogle = GUILayout.Toggle(enableToogle, "Toogle Me", buttonStyle);

            if(enableToogle)
            {
                // your stuff here
            }
        }*/
    }
}
