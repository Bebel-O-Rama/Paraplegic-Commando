using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelZone : MonoBehaviour
{
    public bool isInZone;
    [SerializeField]
    bool IsOnRightSide;
    GameObject Hand;
    // Start is called before the first frame update
    void Start()
    {
        Hand = IsOnRightSide ? GameObject.Find("RH") : GameObject.Find("LH");
    }

    private void OnTriggerEnter(Collider other) => IsOnRightSide = other.gameObject == Hand.gameObject ? true : IsOnRightSide;
    private void OnTriggerExit(Collider other) => IsOnRightSide = other.gameObject == Hand.gameObject ? false : IsOnRightSide;
}
