using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    List<IUpdatable> updatablesScripts;
    // Start is called before the first frame update
    void Start()
    {
        GetComponents<IUpdatable>(updatablesScripts);
    }

    // Update is called once per frame
    void Update()
    {
        updatablesScripts.ForEach(x => x.UpdateObj());
    }
}
