using System.Collections.Generic;
using Fungible.Movement;
using UnityEditor;
using UnityEngine;

namespace Fungible.Editor
{
    [CustomEditor(typeof(Map))]
    public class MapEditor : UnityEditor.Editor
    {
        private Map map;
        private int firstRoomIndex;
        private int displayedRoomIndex;
        private bool showRoomInEditor;
        private readonly List<string> rooms = new List<string>();
        private Dictionary<string, Room> roomNameMap = new Dictionary<string, Room>();
        
        public override void OnInspectorGUI()
        {
            FirstRoomBlock();
            DisplayedRoomBlock();
        }

        private void OnEnable()
        {
            map = (Map) target;

            foreach (Transform roomTransform in map.transform)
            {
                rooms.Add(roomTransform.name);
                roomNameMap.Add(roomTransform.name, roomTransform.GetComponent<Room>());
            }
        }
        
        private void FirstRoomBlock()
        {
            GUILayout.BeginHorizontal("box");
                EditorGUILayout.PrefixLabel("First Room");
                int previousIndex = displayedRoomIndex;
                firstRoomIndex = EditorGUILayout.Popup(firstRoomIndex, rooms.ToArray());
            GUILayout.EndHorizontal();

            if (previousIndex != firstRoomIndex)
            {
                map.firstRoom = GetRoomByName(rooms[firstRoomIndex]);
            }
        }

        private void DisplayedRoomBlock()
        {
            GUILayout.BeginVertical("box");

                GUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Show Room In Editor");
                    showRoomInEditor = EditorGUILayout.Toggle(showRoomInEditor);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                    int previousIndex = displayedRoomIndex;
                    EditorGUILayout.PrefixLabel("Displayed Room");
                    displayedRoomIndex = EditorGUILayout.Popup(displayedRoomIndex, rooms.ToArray());
                GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            
            if (previousIndex != displayedRoomIndex)
            {
                //map.???
            }
        }

        private Room GetRoomByName(string name)
        {
            return roomNameMap.TryGetValue(name, out Room room) ? room : null;
        }
    }
}
