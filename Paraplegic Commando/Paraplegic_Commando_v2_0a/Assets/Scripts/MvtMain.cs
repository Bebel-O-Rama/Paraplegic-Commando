using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MvtMain : MonoBehaviour, IUpdatable
{
    public int PriorityLevel => 3;

    InputDevice associatedController;

    float deltaMvt;
    float pressLevel;
    Vector3 ancPos;

    public void UpdateObj()
    {
        associatedController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out pressLevel);

        if (pressLevel > 0.9f)
            pressLevel = 1f;
        else if (pressLevel < 0.05f)
            pressLevel = 0;

        deltaMvt =  GetDeltaMvt() * pressLevel;

        ancPos = this.transform.localPosition;
    }

    private float GetDeltaMvt()
    {
        //CONTINUER ICI




        return (ancPos - this.transform.localPosition).magnitude /* angle du cossin*/;
    }

    // Start is called before the first frame update
    void Start()
    {
        associatedController = this.GetComponent<XRController>().inputDevice;
    }
}
