using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlockDrag : MonoBehaviour
{
    public Camera camera;
    public GameObject grabbedBlock;
    public GameObject clickLocationVisualizer;
    public float cameraZDistanceOfBlock;
    public float originalCameraZDistanceOfBlock;
    public Ray clickLocation;
    public RaycastHit hitObject;
    public bool dragging;

    public Player playerControls;
    public InputAction dragBlock;

    private void Awake()
    {
        playerControls = new Player(); //inherit from gamemanager later
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dragging == true)
        {
            MoveBlock();
        }
    }

    bool FireRayCast()
    {
        //Debug.Log("attempting to fire");
        clickLocation = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(clickLocation.origin, clickLocation.direction, Color.green);
        if (Physics.Raycast(clickLocation, out hitObject, 100))
        {
            
            clickLocationVisualizer.transform.position = hitObject.point;
            grabbedBlock = hitObject.transform.gameObject;
            return true;
        }
        return false;
    }

    void MoveBlock()
    {
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDistanceOfBlock); //Z axis added to screen point
        Vector3 newWorldPosition = camera.ScreenToWorldPoint(screenPosition);
        grabbedBlock.transform.position = newWorldPosition;
    }

    private void OnEnable()
    {
        //Cursor.lockState = CursorLockMode.Confined; //best to assign to gamemanager
        dragBlock = playerControls.TurretCreation.DragandDrop;
        dragBlock.started += StartDragBlock;
        dragBlock.canceled += StopDragBlock;
        dragBlock.Enable();
    }

    private void OnDisable()
    {
        //Cursor.lockState = CursorLockMode.None; //best to assign to gamemanager
        dragBlock.Disable();
    }

    private void StartDragBlock(InputAction.CallbackContext context)
    {
        if (FireRayCast())
        {
            if(grabbedBlock.tag == "Block")
            {
                dragging = true;
                cameraZDistanceOfBlock = grabbedBlock.GetComponent<Block>().cameraZDistance;
                grabbedBlock.GetComponent<Block>().disconnectToNeighbor();
            }
        }
    }
    private void StopDragBlock(InputAction.CallbackContext context)
    {
        if(dragging == true)
        {
            grabbedBlock.GetComponent<Block>().connectToNeighbor();
        }
        dragging = false;

    }
}
