using Fungible.Movement;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void OnClick()
    {
        MovementHistoryController.instance.PopRoom();
    }
}