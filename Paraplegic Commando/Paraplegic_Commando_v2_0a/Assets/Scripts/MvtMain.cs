using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MvtMain : MonoBehaviour, IUpdatable
{
    bool isPressed;

    public int PriorityLevel => 3;

    float deltaMvt;
    float pressLevel = 0;

    const float SENSIBILITE = 5f;

    Vector3 prevPos;

    private GameObject Player;

    private void OnEnable()
    {
        MvtMain updatable = this;
        GestionnaireUpdate.GetInstance().AddObjToUpdateList(updatable);
    }

    public void UpdateObj()
    {
        //Debug.Log(AssociatedController.name);



        //if (pressLevel > 0.5f)
        //    pressLevel = 1f;
        //else //if (pressLevel < 0.05f)
        //    pressLevel = 0;

        deltaMvt =  GetMvt();




        //if (pressLevel != 0)
        //    Debug.Log("Press Level : " + pressLevel);

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

    public float GetDeltaMvt() => deltaMvt;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
}
