using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MvtChaise : MonoBehaviour, IUpdatable
{
    public int PriorityLevel => 10;


    [SerializeField]
    float DistRelRoueGauche = 0.5f;
    [SerializeField]
    float DistRelRoueDroite = 0.5f;
    [SerializeField]
    float PushStabilityFactor = 2.5f;
    [SerializeField]
    float momentumFactor = 0.15f;
    [SerializeField]
    float turnSensitivity = 0.4f;


    float ancForceDroite;
    float ancForceGauche;
    float ForceDroite;
    float ForceGauche;

    MvtMain RHandMvt;
    MvtMain LHandMvt;
    WheelZone zoneRoue;
    GameObject Player;
    Rigidbody rbPlayer;

    private void OnEnable ()
    {

        //MvtChaise updatable = this;
        //GestionnaireUpdate.GetInstance().AddObjToUpdateList(updatable);
    }

    public void UpdateObj()
    {
        Debug.Log($"Main gauche fait avancer de {Player.transform.forward * GetDeltaMvtMainGauche() /** GestionnaireInputs.GetInstance().GetLeftHandPress()*/}");
        Debug.Log($"Main droite fait avancer de {Player.transform.forward * GetDeltaMvtMainDroite() /** GestionnaireInputs.GetInstance().GetLeftHandPress()*/}");

        rbPlayer.AddForceAtPosition(turnSensitivity * Player.transform.forward * GetDeltaMvtMainDroite() /** GestionnaireInputs.GetInstance().GetRightHandPress()*/,
            Player.transform.position + Player.transform.right * DistRelRoueDroite - Vector3.up * 0.02f, ForceMode.Impulse);
        rbPlayer.AddForceAtPosition(turnSensitivity * Player.transform.forward * GetDeltaMvtMainGauche() /** GestionnaireInputs.GetInstance().GetLeftHandPress()*/,
            Player.transform.position - Player.transform.right * DistRelRoueGauche - Vector3.up * 0.02f, ForceMode.Impulse);
        rbPlayer.AddForce(Player.transform.forward * PushStabilityFactor * (GetDeltaMvtMainDroite() + GetDeltaMvtMainGauche()) /** GestionnaireInputs.GetInstance().GetRightHandPress()*/, ForceMode.Impulse);
    }

    private float GetDeltaMvtMainGauche()
    {
        ForceGauche = (LHandMvt.GetDeltaMvt() + ancForceGauche * momentumFactor) / (1+rbPlayer.velocity.magnitude);
        ancForceGauche = ForceGauche;
        if (zoneRoue.isLHInZone)
            return ForceGauche;
        ancForceGauche *= momentumFactor;
        return ancForceGauche;
    }

    private float GetDeltaMvtMainDroite()
    {
        ForceDroite = (RHandMvt.GetDeltaMvt() + ancForceDroite * momentumFactor) / (1 + rbPlayer.velocity.magnitude);
        ancForceDroite = ForceDroite;
        if (zoneRoue.isRHInZone)
            return ForceDroite;
        ancForceDroite *= momentumFactor;
        return ancForceDroite;
    }

    // Start is called before the first frame update
    void Start()
    {
        MvtChaise updatable = this;
        GestionnaireUpdate.GetInstance().AddObjToUpdateList(updatable);

        Player = GameObject.FindGameObjectWithTag("Player");
        rbPlayer = Player.GetComponent<Rigidbody>();
        zoneRoue = Player.transform.Find("Chair").GetComponent<WheelZone>();     //zoneRoue = FindObjectOfType<WheelZone>();

        RHandMvt = GameObject.Find("RightHand").GetComponent<MvtMain>();
        LHandMvt = GameObject.Find("LeftHand").GetComponent<MvtMain>();
    }
}
