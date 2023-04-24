using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public BoxCollider snapArea;
    public Block hostBlock;
    public GameObject objectWithinBounds;
    public bool connected = false;
    public bool withinArea = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Connect()
    {
        hostBlock.transform.position = objectWithinBounds.transform.position;
        connected = true;
        objectWithinBounds.SetActive(false);
        gameObject.SetActive(false);
    }
    
    public void Disconnect()
    {
        connected = false;
        gameObject.SetActive(true);
        objectWithinBounds.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something Entered with tag of " + other.gameObject.tag);
        if (other.gameObject.tag == "Connector")
        {
            objectWithinBounds = other.gameObject;
            withinArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Connector")
        {
            withinArea = false;
        }
    }
    
}
