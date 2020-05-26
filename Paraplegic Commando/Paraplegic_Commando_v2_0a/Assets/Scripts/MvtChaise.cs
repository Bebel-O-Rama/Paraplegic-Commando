using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MvtChaise : MonoBehaviour, IUpdatable
{
    public int PriorityLevel => 10;

    WheelZone zoneRoue;
    GameObject player;
    Rigidbody rbPlayer;

    [SerializeField]
    float DistRelRoueGauche = 0.5f;
    [SerializeField]
    float DistRelRoueDroite = 0.5f;

    public void UpdateObj()
    {
        rbPlayer.AddForceAtPosition(player.transform.forward * GetDeltaMvtMainDroite(), player.transform.position + player.transform.right * DistRelRoueDroite);
        rbPlayer.AddForceAtPosition(player.transform.forward * GetDeltaMvtMainGauche(), player.transform.position - player.transform.right * DistRelRoueDroite);
    }

    private float GetDeltaMvtMainGauche()
    {
        return 0;
    }

    private float GetDeltaMvtMainDroite()
    {
        return 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        zoneRoue = FindObjectOfType<WheelZone>();
        player = GameObject.FindGameObjectWithTag("Player");
        rbPlayer = player.GetComponent<Rigidbody>();
    }
}
