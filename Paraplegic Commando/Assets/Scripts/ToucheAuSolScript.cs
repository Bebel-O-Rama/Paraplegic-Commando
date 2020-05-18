using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToucheAuSolScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) => GameObject.FindGameObjectWithTag("Player").GetComponent<MvtChaiseRoulante>().toucheAuSol = true;

    private void OnTriggerExit(Collider other) => GameObject.FindGameObjectWithTag("Player").GetComponent<MvtChaiseRoulante>().toucheAuSol = false;
}
