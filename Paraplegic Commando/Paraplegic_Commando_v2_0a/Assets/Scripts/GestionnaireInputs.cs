using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.XR;

public class GestionnaireInputs : MonoBehaviour, IUpdatable
{
    public int PriorityLevel => 2;
    //private bool lastButtonState = false;

    //[System.Serializable]
    //public class PrimaryButtonEvent : UnityEvent<bool> { }

    List<InputDevice> inputDevices = new List<InputDevice>();

    InputDevice rightHand;
    InputDevice leftHand;

    [SerializeField]
    private XRNode handNode = XRNode.GameController;
    [SerializeField]
    private XRNode leftNode = XRNode.LeftHand;

    [SerializeField]
    private XRNode rightNode = XRNode.RightHand;


    //to avoid repeat readings
    private bool triggerIsPressed;
    private bool primaryButtonIsPressed;
    private bool primary2DAxisIsChosen;
    private Vector2 primary2DAxisValue = Vector2.zero;
    private Vector2 prevPrimary2DAxisValue;
    private bool gripIsPressed;

    private const float SEUIL_GACHETTE = 0.5f;

    //public PrimaryButtonEvent primaryButtonPress;


    //If this line shoots an error, that's because there's more than one GestionnaireUpdate
    private static GestionnaireInputs instance;
    private void Awake() => Startup();
    public static void Startup() => instance = FindObjectOfType<GestionnaireInputs>();

    public static GestionnaireInputs GetInstance() => instance;

    void Start()
    {
        //if (primaryButtonPress == null)
        //    primaryButtonPress = new PrimaryButtonEvent();

        GetDevices();
    }

    private void OnEnable()
    {
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
    }
    private void OnDisable()
    {
        InputDevices.deviceConnected -= OnDeviceConnected;
        InputDevices.deviceDisconnected -= OnDeviceDisconnected;
    }

    private void OnDeviceConnected(InputDevice obj) => GetDevices();

    private void OnDeviceDisconnected(InputDevice obj) => GetDevices();

    private void GetDevices()
    {


        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller |
                                                InputDeviceCharacteristics.TrackedDevice |
                                                   InputDeviceCharacteristics.HeldInHand |
                                                          InputDeviceCharacteristics.Left, inputDevices);

        if (inputDevices.Count >= 1)
            leftHand = inputDevices[0];
        else
            Debug.LogWarning("Left VR Controller not found");


        inputDevices.Clear();

        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller |
                                                InputDeviceCharacteristics.TrackedDevice |
                                                   InputDeviceCharacteristics.HeldInHand |
                                                         InputDeviceCharacteristics.Right, inputDevices);

        if (inputDevices.Count >= 1)
            rightHand = inputDevices[0];
        else
            Debug.LogWarning("Right VR Controller not found");

        //GameObject.Find("RightHand").GetComponent<MvtMain>().AssociatedController = inputDevices[0];

        inputDevices.Clear();

        InputDevices.GetDevicesAtXRNode(handNode, inputDevices);
        rightHand = inputDevices.FirstOrDefault(x => x.characteristics.HasFlag(InputDeviceCharacteristics.Right));
        leftHand = inputDevices.FirstOrDefault(x => x.characteristics.HasFlag(InputDeviceCharacteristics.Left));
    }

    public void UpdateObj()
    {
        //bool tempState = false;
        //bool invalidDeviceFound = false;
        //foreach (var device in inputDevices)
        //{
        //    bool buttonState = false;
        //    tempState = device.isValid // the device is still valid
        //                && device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonState) // did get a value
        //                && buttonState // the value we got
        //                || tempState; // cumulative result from other controllers

        //    if (!device.isValid)
        //        invalidDeviceFound = true;
        //}

        //if (tempState != lastButtonState) // Button state changed since last frame
        //{
        //    primaryButtonPress.Invoke(tempState);
        //    lastButtonState = tempState;
        //}

        //if (invalidDeviceFound || inputDevices.Count == 0) // refresh device lists
        //    GetDevices();
    }

    public float GetRightHandPress()
    {
        float pressLevel;
        bool isPressed;

        //if (!rightHand.isValid)
        //    Debug.LogWarning("Manette droite invalide");
        //if (InputHelpers.IsPressed(rightHand, InputHelpers.Button.Grip, out isPressed))//(rightHand.TryGetFeatureValue(CommonUsages.gripButton, out isPressed))
        //    Debug.Log($"Get Right Press {isPressed}");

        // capturing trigger button press and release    
        bool triggerButtonValue = false;
        isPressed = rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue;

        pressLevel = isPressed ? 1 : 0;

        return pressLevel;
    }
    public float GetLeftHandPress()
    {
        float pressLevel;
        bool isPressed;

        //if (!leftHand.isValid)
        //    Debug.LogWarning("Manette gauche invalide");
        //if (InputHelpers.IsPressed(leftHand, InputHelpers.Button.Grip, out isPressed)) //(leftHand.TryGetFeatureValue(CommonUsages.gripButton, out isPressed))
        //    Debug.Log($"Get Left Press : {isPressed}");

        bool triggerButtonValue = false;
        isPressed = leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue;

        pressLevel = isPressed ? 1 : 0;

        return pressLevel;
    }
}
