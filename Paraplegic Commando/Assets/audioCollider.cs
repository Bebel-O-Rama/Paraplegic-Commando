using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioCollider : MonoBehaviour
{
    public AudioClip[] clip;
    public AudioSource source;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Ball")
        {
            source.clip = clip[Random.Range(0, clip.Length)];
            source.Play();
        }
    }
}
