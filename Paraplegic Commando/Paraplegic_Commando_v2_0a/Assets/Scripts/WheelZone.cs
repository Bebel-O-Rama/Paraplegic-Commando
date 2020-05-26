using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WheelZone : MonoBehaviour, IUpdatable
{
    public bool isRHInZone;
    public bool isLHInZone;

    [SerializeField]
    float Rayon = 1;
    [SerializeField]
    float Largeur = 0.1f;
    [SerializeField]
    float Distance = 0.5f;

    GameObject RHand;
    GameObject LHand;

    public int PriorityLevel { get => 5;}

    private void Awake()
    {
        GestionnaireUpdate.Startup();
        WheelZone updatable = this;
        GestionnaireUpdate.GetInstance().AddObjToUpdateList(updatable);
    }

    // Start is called before the first frame update
    void Start()
    {

        RHand = GameObject.Find("RH");
        LHand = GameObject.Find("LH");


        //Hand = IsOnRightSide ? GameObject.Find("RH") : GameObject.Find("LH");
    }

    //private void Update()
    //{
    //    isInZone = CheckIfHandInZone();
    //}

    private bool CheckIfHandInZone(GameObject Hand, float dist)
    {
        return Hand.transform.localPosition.x - Distance > -Largeur / 2 &&
               Hand.transform.localPosition.x - Distance <  Largeur / 2 &&
               Vector3.Distance(Hand.transform.localPosition - dist * Vector3.right, Vector3.zero) < Rayon;
    }

    //private void OnTriggerEnter(Collider other) => IsOnRightSide = other.gameObject == Hand.gameObject ? true : IsOnRightSide;
    //private void OnTriggerExit(Collider other) => IsOnRightSide = other.gameObject == Hand.gameObject ? false : IsOnRightSide;

    public void UpdateObj()
    {
        isRHInZone = CheckIfHandInZone(RHand, Distance);
        isLHInZone = CheckIfHandInZone(LHand, -Distance);
    }
}
