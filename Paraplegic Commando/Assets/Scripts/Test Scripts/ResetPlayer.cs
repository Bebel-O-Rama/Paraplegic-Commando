using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ResetPlayer : MonoBehaviour
{
    [SerializeField]
    protected OVRInput.Controller m_controller;
    Vector3 PositionInitiale;
    // Start is called before the first frame update
    void Awake()
    {
        PositionInitiale = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    void Update()
    {
        OVRInput.Update();
        if (OVRInput.GetDown(OVRInput.Button.One, m_controller))
        {
            Debug.Log("A pressed");
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = true;
            GameObject.FindGameObjectWithTag("Player").transform.position = PositionInitiale;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
