using OculusSampleFramework;
using OVRTouchSample;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MvtChaiseRoulante : MonoBehaviour
{
    //[SerializeField] //S'assurrer que ZoneRoueDroite possède un component OVRGrabbable [Testé en Awake()]
    //GameObject ZoneRoue; //this
    [SerializeField]
    OVRGrabber MainAssociee;

    public float qteMvt;
    float enfoncementPiton;

    const float grabBegin = 0.55f;
    const float grabEnd = 0.35f;
    [SerializeField]
    private float ForceFact = 0.25f;

    [SerializeField]
    /*const*/ float threshMvt = 0.05f;
    Vector3 prevPos;

    [SerializeField]
    protected OVRInput.Controller m_controller = OVRInput.Controller.None;

    public OVRInput.Axis1D triggerBtn { get; set; }


    GameObject player;
    OVRTouchSample.Hand handAnim;
    Rigidbody rbPlayer;

    bool estDansLaZone;
    bool aAvancer;
    public bool toucheAuSol;
    public bool isRight;
    protected float prevEnfoncementPiton;
    private Vector3 debugPos;
    private Vector3 ancPos;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rbPlayer = player.GetComponent<Rigidbody>();
        //if (!this.GetComponent<OVRGrabbable>())
        //    throw new System.Exception();
        prevPos = MainAssociee.transform.localPosition;
        prevEnfoncementPiton = 0;

        ancPos = player.transform.position;

        Debug.Log(triggerBtn);
    }

    public void SetMain(string tag) => handAnim = GameObject.FindGameObjectWithTag(tag).GetComponent<OVRTouchSample.Hand>();

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(MainAssociee.ToString() + " : " + TrouverDeltaMvt());

        //if (aAvancer)
        //    UpdateInfoMvt();
        //Deplacer(TrouverDeltaMvt(MainAssociee.transform.position, prevPos));

        //if (EstMvtValide) //revoir
        //{
        //    prevEnfoncementPiton = enfoncementPiton;
        //    aAvancer = true;
        //}
        //else
        //    aAvancer = false;

        //prevPos = MainAssociee.transform.localPosition;

        
    }

    public void UpdateRoue()
    {
        prevEnfoncementPiton = TrouverEnfoncementPiton();
        //enfoncementPiton = TrouverEnfoncementPiton(triggerBtn);

        ancPos = this.transform.position;
        prevPos = MainAssociee.transform.position;
    }

    private float TrouverEnfoncementPiton()
    {
        //if (isRight)
        //    return OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);
        //else
        //    return OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, m_controller);
        return !handAnim.m_isPointing ? 1 : 0;
    }

    //private bool EstMvtValide() => estDansLaZone && CheckForGrabOrRelease(TrouverEnfoncementPiton());

    //Retourne vrai si le bouton est enfoncé et le cossin est dans la zone
    private bool EstMvtValide() => !handAnim.m_isPointing && estDansLaZone;

    void OnTriggerEnter(Collider otherCollider) => estDansLaZone = otherCollider.gameObject == handAnim.gameObject ? true : estDansLaZone;

    void OnTriggerExit(Collider otherCollider) => estDansLaZone = otherCollider.gameObject == handAnim.gameObject ? false : estDansLaZone;

    protected bool CheckForGrabOrRelease(float flex) => (prevEnfoncementPiton >= grabEnd) || (flex >= grabBegin);

    //protected bool CheckForGrabOrRelease(bool isRight)
    //{
    //    if (isRight)
    //        return Input.GetAxis("Oculus_GearVR_RIndexTrigger") > grabBegin;
    //    return Input.GetAxis("Oculus_GearVR_LIndexTrigger") > grabBegin;
    //}

    private void MoveEnd()
    {
        return;
    }

    //private void Deplacer(float qteMvt)
    //{
    //    if (toucheAuSol)
    //        player.GetComponent<PlayerMvtScript>().Move(m_controller, qteMvt);
    //       // player.GetComponent<Rigidbody>().AddForce(this.transform.forward * qteMvt);
    //}

    public float TrouverDeltaMvt(bool isRight)
    {
        //Debug.Log(TrouverEnfoncementPiton());
        // Debug.Log("Bouton " + (isRight ? "droit pesé : " : "gauche pesé : ") + CheckForGrabOrRelease(isRight));
        //if (!EstMvtValide)
        //    return 0;


        if (!EstMvtValide(/*isRight*/))
            return 0;

        //Debug.Log(TrouverEnfoncementPiton());

        //int dir = isRight ? 1 : -1;
        int dir = -1;

        Vector3 vectChaiseMain = Vector3.ProjectOnPlane((prevPos - MainAssociee.transform.position), player.transform.right * dir);
        Vector3 vectChaiseMvtA = Vector3.ProjectOnPlane((MainAssociee.transform.position - this.transform.position), this.transform.right *dir);
        Vector3 vectChaiseMvtB = Vector3.ProjectOnPlane((prevPos - this.transform.position), this.transform.right * dir);

        
        //float deltaPosNorm = (posB - posA).magnitude * Time.deltaTime;
        float deltaPosNorm = vectChaiseMain.magnitude / Time.deltaTime;

        //float deltaAngle = Vector3.Angle((this.transform.position-posA), this.transform.position - posB) * Time.deltaTime;
        float deltaAngle = Mathf.Sin(Vector3.SignedAngle(vectChaiseMvtB, vectChaiseMvtA, this.transform.position) * Mathf.Deg2Rad /2) / Time.deltaTime; //Ajouter la distance du centre de la roue comme terme multiplicatif

        //DEBUG
        debugPos = this.transform.position + this.transform.forward * 10 * deltaAngle * deltaPosNorm; // * TrouverEnfoncementPiton();
        Debug.DrawLine(this.transform.position, debugPos, Color.blue, 0.33f);

        ////Enregistrer la position du grab initial et freiner en dehors d'un certain rayon
        //if (Mathf.Abs(deltaAngle * deltaPosNorm) < threshMvt)
        //    return -1 * OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller) * Vector3.Dot(rbPlayer.velocity, this.transform.forward); // À REVOIR SELON MECANIQUE DE FREINAGE
        return deltaAngle * deltaPosNorm * ForceFact; //* TrouverEnfoncementPiton();
    }

    public void Avancer(float force)
    {
        //Vector3 deltaPos = ((this.transform.position - ancPos).magnitude < 0.05f ? Vector3.zero : this.transform.position - ancPos);
        //rbPlayer.AddForceAtPosition(player.transform.forward * force - deltaPos*0.25f, this.transform.position);
        //Debug.Log(MainAssociee.name + " : " + force + ", " + deltaPos);

        Vector3 deltaPos = ((this.transform.position - ancPos).magnitude < 0.05f ? Vector3.zero : this.transform.position - ancPos);
        rbPlayer.AddForceAtPosition(player.transform.forward * force / (1+deltaPos.magnitude), this.transform.position);
        Debug.Log(MainAssociee.name + " : " + force + ", " + deltaPos);
    }
 
}
