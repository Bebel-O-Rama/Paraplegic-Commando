using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathZone : MonoBehaviour
{
    Vector3 PositionInitiale;
    Quaternion RotationInitiale;
    void Awake()
    {
        PositionInitiale = GameObject.FindGameObjectWithTag("Player").transform.position;
        RotationInitiale = GameObject.FindGameObjectWithTag("Player").transform.rotation;

    }
    private void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);

        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = true;
        GameObject.FindGameObjectWithTag("Player").transform.position = PositionInitiale;
        GameObject.FindGameObjectWithTag("Player").transform.rotation = RotationInitiale;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
    }
}
