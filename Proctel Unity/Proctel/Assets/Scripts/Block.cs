using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class Block : MonoBehaviour
{
    public Camera camera;
    public float cameraZDistance;

    public GameObject posX, posY, posZ, negX, negY, negZ;
    public List<GameObject> connectionPoints;
    public bool withinArea;
    bool connected;
    public GameObject parentConnection;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        cameraZDistance = camera.WorldToScreenPoint(transform.position).z; //Z axis of the game object for the screen view
        connectionPoints.Add(posX); connectionPoints.Add(posY); connectionPoints.Add(posZ); connectionPoints.Add(negX); connectionPoints.Add(negY); connectionPoints.Add(negZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (connected)
        {
            transform.position = parentConnection.transform.position;
        }
    }

    public void connectToNeighbor() //for the block to connect to its neighboring block
    {
        foreach (GameObject con in connectionPoints)
        {
            if (!con.GetComponent<Connector>().connected && con.GetComponent<Connector>().withinArea)
            {
                con.GetComponent<Connector>().Connect();
                connected = true;
            }
        }
    }

    public void disconnectToNeighbor()
    {
        connected = false;
        foreach (GameObject con in connectionPoints)
        {
            if (con.GetComponent<Connector>().connected)
            {
                con.GetComponent<Connector>().Disconnect();
            }
        }
    }
}
