using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightWheel : MonoBehaviour
{
    public WheelCollider wC;
    [SerializeField] float powa;
    // Update is called once per frame
    void Update()
    {
        wC.motorTorque = powa * Time.deltaTime * Input.GetAxis("AccelRight");
    }
}
