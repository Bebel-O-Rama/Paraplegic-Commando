using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMvtScript : MonoBehaviour
{
    Rigidbody rbody { get; set; }
    Vector3 dirQty { get; set; }
    float turnQty { get; set; }

    [SerializeField]
    MvtChaiseRoulante MainD;

    [SerializeField]
    MvtChaiseRoulante MainG;

    GameObject player;
    private Vector3 ancPos;
    private Quaternion ancRot;
    public const float impactMvt = 0.05f;
    public const float MAX_SPEED = 500f; 
    public const float threshold = 0.10f; 
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rbody = player.GetComponent<Rigidbody>();
        dirQty = new Vector3();

        //Definition des gachettes a utiliser pour le mvt de la chaise
        MainD.triggerBtn = OVRInput.Axis1D.PrimaryIndexTrigger;
        MainG.triggerBtn = OVRInput.Axis1D.SecondaryIndexTrigger;

        MainD.isRight = true;
        MainG.isRight = false;

        MainD.SetMain("HR");
        MainG.SetMain("HL");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        MainD.UpdateRoue();
        MainG.UpdateRoue();
    }

    public void Move(float rMvt, float lMvt)
    {
        float turnAmount = rMvt - lMvt;
        float forwardAmount = rMvt + lMvt - Mathf.Abs(turnAmount);

        dirQty = Vector3.Lerp(-rbody.velocity, this.transform.forward * forwardAmount, impactMvt);


        rbody.AddForce(dirQty, ForceMode.Impulse);
        rbody.AddTorque(this.transform.up * turnAmount, ForceMode.Impulse);
    }

    public void Move()
    {
        float rMvt = MainD.TrouverDeltaMvt(true);
        float lMvt = MainG.TrouverDeltaMvt(false);

        Debug.Log(string.Format("Mvt main droite : {0} \n Mvt main gauche : {1}", rMvt, lMvt));

        MainD.Avancer(rMvt);
        MainG.Avancer(lMvt);

        //rbody.AddForceAtPosition(player.transform.forward * lMvt * 0.05f, player.transform.right * -0.1f + player.transform.position, ForceMode.Impulse);
        //rbody.AddForceAtPosition(player.transform.forward * rMvt * 0.05f, player.transform.right * 0.1f + player.transform.position, ForceMode.Impulse);

        //float forwardAmount = rMvt + lMvt;//) / ((Mathf.Abs(3 * turnAmount) + 2) / 2); //revoir
        //float turnAmount = rMvt - lMvt / (forwardAmount + 1);

        //if (forwardAmount > threshold)
        //{
        //    //dirQty = Vector3.Lerp(rbody.velocity, this.transform.forward * MAX_SPEED, impactMvt * forwardAmount);
        //    Vector3 deltaPos = ancPos - this.transform.position;

        //    rbody.AddForce((impactMvt * forwardAmount * this.transform.forward - deltaPos)/(1+rbody.velocity.magnitude), ForceMode.Impulse);
        //}
        //if (turnAmount > threshold)
        //{
        //    //Vector3 deltaRotPos = ancPos - ;
        //    turnQty = turnAmount / (rbody.velocity.magnitude + 1);
        //    rbody.AddTorque(/*this.*/(Vector3.up * turnQty) / (1+rbody.angularVelocity.magnitude*0.5f), ForceMode.Impulse);

        //}
        //ancPos = this.transform.position;
    }
    //Retourne vrai si le bouton est enfoncé et le cossin est dans la zone
   
}
