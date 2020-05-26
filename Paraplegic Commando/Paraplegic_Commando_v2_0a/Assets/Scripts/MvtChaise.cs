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

    MvtMain RHandMvt;
    MvtMain LHandMvt;
    WheelZone zoneRoue;
    GameObject Player;
    Rigidbody rbPlayer;

    public void UpdateObj()
    {
        rbPlayer.AddForceAtPosition(Player.transform.forward * GetDeltaMvtMainDroite(), Player.transform.position + Player.transform.right * DistRelRoueDroite, ForceMode.Impulse);
        rbPlayer.AddForceAtPosition(Player.transform.forward * GetDeltaMvtMainGauche(), Player.transform.position - Player.transform.right * DistRelRoueGauche, ForceMode.Impulse);
    }

    private float GetDeltaMvtMainGauche()
    {
        if (zoneRoue.isLHInZone)
            return LHandMvt.GetDeltaMvt();
        return 0;
    }

    private float GetDeltaMvtMainDroite()
    {
        if (zoneRoue.isRHInZone)
            return RHandMvt.GetDeltaMvt();
        return 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rbPlayer = Player.GetComponent<Rigidbody>();
        zoneRoue = Player.GetComponentInChildren<WheelZone>();     //zoneRoue = FindObjectOfType<WheelZone>();

        RHandMvt = GameObject.Find("RightHand").GetComponent<MvtMain>();
        LHandMvt = GameObject.Find("LeftHand").GetComponent<MvtMain>();
    }
}
