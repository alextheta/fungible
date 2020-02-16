#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Fungible.Inventory;
using Fungible.Movement;
using UnityEditor;
using UnityEngine;

namespace Fungible.Editor
{
    [CustomEditor(typeof(Map))]
    public class MapEditor : UnityEditor.Editor
    {
        private int firstRoomIndex;
        private int displayedRoomIndex;
        private bool showRoomInEditor;
        private string newRoomName;
        private string replaceRoomName;
        private readonly List<string> roomNames = new List<string>();
        private readonly Dictionary<string, Room> roomNameMap = new Dictionary<string, Room>();

        SerializedProperty firstRoom;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (roomNames.Count > 0)
            {
                FirstRoomBlock();
                DisplayedRoomBlock();
            }

            NewRoomBlock();

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            Map map = (Map) target;

            RebuildMapData();

            firstRoom = serializedObject.FindProperty("firstRoom");
            if (roomNames.Count > 0 && firstRoom.objectReferenceValue != null)
                firstRoomIndex = GetIndexByName(firstRoom.objectReferenceValue.name);
        }

        private void FirstRoomBlock()
        {
            GUILayout.BeginHorizontal("box");

            int previousIndex = firstRoomIndex;
            firstRoomIndex = EditorGUILayout.Popup(new GUIContent("First Room"), previousIndex, roomNames.ToArray());

            if (previousIndex != firstRoomIndex)
                firstRoom.objectReferenceValue = GetRoomByName(roomNames[firstRoomIndex]);

            GUILayout.EndHorizontal();
        }

        private void DisplayedRoomBlock()
        {
            Room room = roomNameMap[roomNames[displayedRoomIndex]];
            
            GUILayout.BeginVertical("box");

            GUILayout.BeginHorizontal();
            bool prevShowRoomInEditor = showRoomInEditor;
            showRoomInEditor = EditorGUILayout.Toggle(new GUIContent("Display Room"), prevShowRoomInEditor);
            
            if (GUILayout.Button("Go To GameObject"))
                Selection.activeGameObject = room.gameObject;

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            int previousIndex = displayedRoomIndex;
            displayedRoomIndex = EditorGUILayout.Popup(new GUIContent("Displayed Room"),
                                                       previousIndex,
                                                       roomNames.ToArray());

            if (showRoomInEditor && (!prevShowRoomInEditor || previousIndex != displayedRoomIndex))
            {
                DisableRoom(previousIndex);
                SetDisplayedRoom(displayedRoomIndex);
            }
            else if (prevShowRoomInEditor && !showRoomInEditor)
                ResetDisplayedRoom();

            if (GUILayout.Button("Remove Room"))
            {
                RemoveRoom(displayedRoomIndex);
                return;
            }

            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            
            replaceRoomName = EditorGUILayout.TextField("Rename Room", replaceRoomName);
            if (GUILayout.Button("Rename Room"))
                RenameRoom(replaceRoomName);
            
            GUILayout.EndHorizontal();
            
            room.backgroundResourceName = EditorGUILayout.TextField("Room Background File", room.backgroundResourceName);

            GUILayout.EndVertical();
        }

        private void NewRoomBlock()
        {
            GUILayout.BeginVertical("box");

            newRoomName = EditorGUILayout.TextField("New Room Name", newRoomName);
            
            if (GUILayout.Button("Create Room"))
                CreateRoom(newRoomName);
            
            GUILayout.EndVertical();
        }

        private Room GetRoomByName(string name)
        {
            return roomNameMap.TryGetValue(name, out Room room) ? room : null;
        }

        private int GetIndexByName(string name)
        {
            return roomNameMap.Keys.ToList().IndexOf(name);
        }

        private void SetDisplayedRoom(int roomIndex)
        {
            Map map = (Map) target;
            Room room = roomNameMap[roomNames[roomIndex]];
            SpriteRenderer spriteRenderer = map.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = room.LoadSprite();
            room.gameObject.SetActive(true);
            replaceRoomName = room.name;
            map.AdjustBackground();
        }

        private void ResetDisplayedRoom()
        {
            Map map = (Map) target;
            map.GetComponent<SpriteRenderer>().sprite = null;
            DisableRoom(displayedRoomIndex);
        }

        private void DisableRoom(int index)
        {
            Room room = roomNameMap[roomNames[index]];
            room.gameObject.SetActive(false);
        }

        private void RemoveRoom(int index)
        {
            Room room = roomNameMap[roomNames[index]];
            DestroyImmediate(room.gameObject);
            RebuildMapData();
        }
        
        private void CreateRoom(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                Debug.LogError("Room name is empty");
                return;
            }

            if (roomNameMap.ContainsKey(name))
            {
                Debug.LogError("Room " + name + " is already exists");
                return;
            }

            Map map = (Map) target;
            GameObject roomObject = new GameObject();
            roomObject.name = name;
            roomObject.AddComponent<Room>();
            roomObject.transform.parent = map.transform;
            roomObject.SetActive(false);
            RebuildMapData();
        }

        private void RenameRoom(string name)
        {
            roomNameMap[roomNames[displayedRoomIndex]].name = name;
            RebuildMapData();
        }
        
        private void RebuildMapData()
        {
            Map map = (Map) target;
            
            roomNames.Clear();
            roomNameMap.Clear();

            foreach (Transform roomTransform in map.transform)
            {
                roomNames.Add(roomTransform.name);
                roomNameMap.Add(roomTransform.name, roomTransform.GetComponent<Room>());
            }
        }
    }
}
#endif