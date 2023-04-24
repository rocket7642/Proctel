using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class TurretDesignGameManager : MonoBehaviour
{
    public Player playerControls;
    public InputAction rotateCameraPos;
    public InputAction rotateCameraRot;
    public InputAction spawn;
    public GameObject prefab;

    public float rotationSpeedPos = 60;
    public float rotationSpeedRot = 2000;

    private void Awake()
    {
        playerControls = new Player(); //inherit from gamemanager in future
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateCameraAround();
        rotateCameraItself();
    }

    private void OnEnable()
    {
        //Cursor.lockState = CursorLockMode.Confined; best to assign this in gamemanager
        rotateCameraPos = playerControls.TurretCreation.RotateCameraPos;
        rotateCameraRot = playerControls.TurretCreation.RotateCameraRot;
        spawn = playerControls.TestingFunctions.Spawn;

        spawn.performed += spawnCube;

        rotateCameraPos.Enable();
        rotateCameraRot.Enable();
        spawn.Enable();
    }

    private void OnDisable()
    {
        rotateCameraPos.Disable();
        rotateCameraRot.Disable();
        spawn.Disable();
    }

    private void rotateCameraAround()
    {
        transform.RotateAround(Vector3.zero, transform.up, -(rotateCameraPos.ReadValue<Vector2>().x * rotationSpeedPos * Time.deltaTime));
        transform.RotateAround(Vector3.zero, transform.right, (rotateCameraPos.ReadValue<Vector2>().y * rotationSpeedPos * Time.deltaTime));
    }
    private void rotateCameraItself()
    {
        transform.eulerAngles += new Vector3(0, 0, rotationSpeedRot * -rotateCameraRot.ReadValue<float>() * Time.deltaTime);
    }
    private void spawnCube(InputAction.CallbackContext context)
    {
        Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }
}
