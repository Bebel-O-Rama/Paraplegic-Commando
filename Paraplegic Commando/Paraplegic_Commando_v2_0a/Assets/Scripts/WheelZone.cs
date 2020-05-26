using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WheelZone : MonoBehaviour, IUpdatable
{
    bool debounceG;
    bool debounceD;


    public bool isRHInZone;
    public bool isLHInZone;

    [SerializeField]
    float Rayon = 0.35f;
    [SerializeField]
    float Largeur = 0.15f;
    [SerializeField]
    float LateralOffset = 0.25f;
    [SerializeField]
    float ForwardOffset = -0.25f;
    [SerializeField]
    float UpwardOffset = 0.0f;

    Vector3 Offset;

    GameObject RHand;
    GameObject LHand;
    GameObject Player;

    public int PriorityLevel { get => 5;}

    private void Awake()
    {
        GestionnaireUpdate.Startup();
        WheelZone updatable = this;
        GestionnaireUpdate.GetInstance().AddObjToUpdateList(updatable);
        Offset = Vector3.forward * ForwardOffset + Vector3.up * UpwardOffset;
    }

    // Start is called before the first frame update
    void Start()
    {

        RHand = GameObject.Find("RightHand");
        LHand = GameObject.Find("LeftHand");

        debounceD = true;
        debounceG = true;

        Player = GameObject.FindGameObjectWithTag("Player");

        //Hand = IsOnRightSide ? GameObject.Find("RH") : GameObject.Find("LH");
    }

    //private void Update()
    //{
    //    isInZone = CheckIfHandInZone();
    //}

    private bool CheckIfHandInZone(GameObject Hand, float dist)
    {

        //Debug
        Debug.DrawLine(Player.transform.position + Offset - dist * Player.transform.right - Player.transform.up * Rayon,
            Player.transform.position + Offset - dist * Player.transform.right + Player.transform.up * Rayon, Color.cyan, 0.1f);
        Debug.DrawLine(Player.transform.position + Offset - dist * Player.transform.right - Player.transform.forward * Rayon,
            Player.transform.position + Offset - dist * Player.transform.right + Player.transform.forward * Rayon, Color.yellow, 0.1f);
        Debug.DrawLine(Player.transform.position + Offset - dist * Player.transform.right - Vector3.right * (Largeur/2),
            Player.transform.position + Offset - dist * Player.transform.right + Vector3.right * (Largeur/2), Color.magenta, 0.1f);


        //REVIEW : Might change Vector3 in 3rd line to Player.transform
        return Hand.transform.localPosition.x - dist > -Largeur / 2 &&
               Hand.transform.localPosition.x - dist <  Largeur / 2 &&
               (Hand.transform.localPosition - Offset - dist * Vector3.right).magnitude <= Rayon;


    }

    //private void OnTriggerEnter(Collider other) => IsOnRightSide = other.gameObject == Hand.gameObject ? true : IsOnRightSide;
    //private void OnTriggerExit(Collider other) => IsOnRightSide = other.gameObject == Hand.gameObject ? false : IsOnRightSide;

    public void UpdateObj()
    {
        isRHInZone = CheckIfHandInZone(RHand, LateralOffset);
        isLHInZone = CheckIfHandInZone(LHand, -LateralOffset);

        if (isLHInZone && debounceG)
        {
            Debug.Log("Main gauche a entré dans zone");
            debounceG = false;
        }
        else if (!(isLHInZone && debounceG))
            debounceG = true;


        if (isRHInZone && debounceD)
        {
            Debug.Log("Main droite a entré dans zone");
            debounceD = false;
        }
        else if (!(isRHInZone && debounceD))
            debounceD = true;
    }
}
