using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MvtMain : MonoBehaviour, IUpdatable
{
    public int PriorityLevel => 3;

    float deltaMvt;
    float pressLevel;

    const float SENSIBILITE = 0.25f;

    Vector3 prevPos;
    InputDevice associatedController;

    private GameObject Player;

    public void UpdateObj()
    {

        if (associatedController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out pressLevel))
            Debug.Log("Trigger marche");

        if (pressLevel > 0.9f)
            pressLevel = 1f;
        else if (pressLevel < 0.05f)
            pressLevel = 0;

        deltaMvt =  GetMvt() * pressLevel;

        prevPos = this.transform.localPosition;
    }

    private float GetMvt()
    {
        Vector3 vectChaiseMain = (prevPos - this.transform.localPosition) - (prevPos - this.transform.localPosition).x * Player.transform.right;
        Vector3 vectAngleA = prevPos - prevPos.x * Player.transform.right;
        Vector3 vectAngleB = this.transform.localPosition - this.transform.localPosition.x * Player.transform.right;
        

        float deltaPosNorm = vectChaiseMain.magnitude;
        float deltaAngle = Mathf.Sin(Vector3.SignedAngle(vectAngleA, vectAngleB, Player.transform.right) * Mathf.Deg2Rad / 2);
        float torqueModifier = vectAngleA.magnitude;


        return SENSIBILITE * deltaAngle * deltaPosNorm * torqueModifier / Time.deltaTime;
    }

    public float GetDeltaMvt()
    {
        return deltaMvt;
    }

    // Start is called before the first frame update
    void Start()
    {
        associatedController = this.GetComponent<XRController>().inputDevice;
        Player = GameObject.FindGameObjectWithTag("Player");
    }
}
