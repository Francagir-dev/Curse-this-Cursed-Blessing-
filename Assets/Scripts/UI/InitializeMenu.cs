using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

public class InitializeMenu : MonoBehaviour
{
    PlayerInput _controls;
   [SerializeField] private GameObject pressAnyKeyCanvas;
   [SerializeField] private GameObject mainMenuCanvas;
   [SerializeField] private UIResolver _UIResolver;
   private ReadOnlyArray<Gamepad> allGamepads;
 

   private void Awake()
   {
       _controls = new PlayerInput();
       _controls.UIControls.PressAnyKey.performed += ctx => PressAnyKeyAction();
       DetectGamepad();
       InputSystem.onDeviceChange +=
           (device, change) =>
           {
               switch (change)
               {
                   case InputDeviceChange.Added:
                       // New Device.
                       if(Gamepad.current != null)
                           _UIResolver.Identifier = "pressstartbutton";
                       else
                       _UIResolver.Identifier ="pressanykey";
                       break;
                   case InputDeviceChange.Disconnected:
                       // Device got unplugged.
                       if(Gamepad.current == null)
                           _UIResolver.Identifier = "pressanykey";
                       else
                           _UIResolver.Identifier ="pressanykey";
                       break;
               }
               _UIResolver.SetTexts();
           };

   }
   public void PressAnyKeyAction()
    {
        if (pressAnyKeyCanvas.activeInHierarchy)
        {
            pressAnyKeyCanvas.SetActive(false);
            mainMenuCanvas.SetActive(true);
        }
    }
   private void OnEnable()
   {
       _controls.UIControls.PressAnyKey.Enable();
   }

   private void OnDisable()
   {
       _controls.UIControls.PressAnyKey.Disable();
   }

   private void Update()
   {
       DetectGamepad();
   }

   void DetectGamepad()
   {
    
   }
   
}
