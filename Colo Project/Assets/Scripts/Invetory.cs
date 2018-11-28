using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invetory : MonoBehaviour {

    public bool invetoryEnabled;
    public GameObject invetory;
    public GameObject itemDatabase;
    private Transform[] slot;
    public GameObject slotHolder;

    private bool pickedUpItem;
    float waitTimer = 0.5f;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GetAllSlots();
    }
    public void Update()
    {      
        //Enabling and disabling the invetory
        if (Input.GetKeyDown(KeyCode.I))
            invetoryEnabled = !invetoryEnabled;

        if (invetoryEnabled)
        {
            invetory.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Camera.main.GetComponent<vThirdPersonCamera>().enabled = false;
        }
        else
        {
            invetory.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Camera.main.GetComponent<vThirdPersonCamera>().enabled = true;

        }
      if(pickedUpItem == true)
        {
            waitTimer -= 1 * Time.deltaTime;
        }     
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {         
            AddItem(other.gameObject);
        }
    }
    public void AddItem(GameObject item)
    {
        GameObject rootItem;
        rootItem = item.GetComponent<ItemPickup>().rootItem;
        for(int i = 0; i < 25; i++)
        {
            if(slot[i].GetComponent<Slot>().empty == true && item.GetComponent<ItemPickup>().Pickedup == false)
            {
                slot[i].GetComponent<Slot>().item = rootItem;
                item.GetComponent<ItemPickup>().Pickedup = true;
                Destroy(item);
                
            }
        }
    }
    public void GetAllSlots()
    {
        slot = new Transform[25];
        for(int i = 0; i < 25; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i);            
        }
    }

   

}
