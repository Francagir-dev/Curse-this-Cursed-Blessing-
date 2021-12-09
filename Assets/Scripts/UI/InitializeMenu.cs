using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InitializeMenu : MonoBehaviour
{
    PlayerInput _controls;
   [SerializeField] private GameObject pressAnyKeyCanvas;
   [SerializeField] private GameObject mainMenuCanvas;
   [SerializeField] private UIResolver _UIResolver;
  
 

   private void Awake()
   {
     
       _controls = new PlayerInput();
       _controls.UIControls.PressAnyKey.performed += ctx => PressAnyKeyAction();
       _UIResolver.Identifier = "pressanykey";

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
}
