
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;


public class DeviceManager : MonoBehaviour
{
    private string[] temp;

    private void Awake()
    {
        temp = Input.GetJoystickNames();
      
    }
}
