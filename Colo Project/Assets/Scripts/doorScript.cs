using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour {

    public GameObject Door;
    public GameObject Door2;

    public bool doorIsOpening;


    void Update()
    {
        

        
        if (doorIsOpening == true)
        {
            Door.transform.Translate(Vector3.forward * Time.deltaTime * 5);
            

            Door2.transform.Translate(Vector3.forward * Time.deltaTime * 5);
        }

        if (Door.transform.rotation.z > 2f)
        {
            doorIsOpening = false;

        }

        if (Door2.transform.rotation.z > 1.5f)
        {
            doorIsOpening = false;
        }

    }

    // Fonction pour la porte :
    void OnMouseDown()
    { 

        
        doorIsOpening = true;

    }

}
