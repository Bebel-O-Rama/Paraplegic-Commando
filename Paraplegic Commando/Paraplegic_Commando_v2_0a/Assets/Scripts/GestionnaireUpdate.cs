using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GestionnaireUpdate : MonoBehaviour
{
    /*
     On peut considérer l'ajout de méthodes similaires pour le Awake() et le Start()
     
     À revoir dans developpement futur...
     
     */



    List<IUpdatable> UpdatablesScripts = new List<IUpdatable>();

    //If this line shoots an error, that's because there's more than one GestionnaireUpdate
    private static GestionnaireUpdate instance;
    private void Awake() => Startup();
    public static void Startup() => instance = FindObjectOfType<GestionnaireUpdate>();

    public static GestionnaireUpdate GetInstance() => instance;


    void Start()
    {
        //GetComponents<IUpdatable>(updatablesScripts);
        UpdatablesScripts.OrderBy(x => x.PriorityLevel);
    }


    void Update()
    {
        foreach (IUpdatable item in UpdatablesScripts)
            item.UpdateObj();
    }

    /// <summary>
    /// Adds an updatable object to a List which updates in a specific order
    /// </summary>
    /// <param name="updatable">The updatable object to be updated via its method UpdateObj(). The object needs to be added in the OnEnable() method</param>
    public void AddObjToUpdateList(IUpdatable updatable)
    {
        //if (UpdatablesScripts.Count != 0)
        //{
        //    UpdatablesScripts.Insert(UpdatablesScripts.FindIndex(x => x.PriorityLevel >= updatable.PriorityLevel), updatable);
        //    return;
        //}
        UpdatablesScripts.Add(updatable);

        Debug.Log($"Added {updatable} to update list with priority {updatable.PriorityLevel}");
    }
}
