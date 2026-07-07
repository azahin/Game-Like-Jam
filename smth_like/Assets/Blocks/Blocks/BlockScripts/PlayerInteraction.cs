using System.ComponentModel;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerInteraction : MonoBehaviour
{
    InputAction placeBlock;
    InputAction destoryBlock;
    InputAction identifyBlock;

    private bool blockInteraction;

    RaycastHit blockSelected;


    [SerializeField] WorldManager worldManager; //This will be swapped to the player object when that's done
    [SerializeField] CinemachineCamera cam; // What is to be swapped to

    private Container container2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        placeBlock = InputSystem.actions.FindAction("Place");
        destoryBlock = InputSystem.actions.FindAction("Attack");
        identifyBlock = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (placeBlock.IsPressed() && !blockInteraction)
        {
            blockInteraction = true;
            Debug.Log("Placed");
            WhatAmILookingAt();
            worldManager.CreateContainer(blockSelected);

        }
        if (destoryBlock.IsPressed() && !blockInteraction)
        {
            blockInteraction = true;
            Debug.Log("Destoryed");
            WhatAmILookingAt();
            worldManager.DestroyContainer(blockSelected.transform);
        }
        if (!placeBlock.IsPressed() && !destoryBlock.IsPressed())
        {
            blockInteraction = false;
        }

        if (identifyBlock.IsPressed() && WhatAmILookingAt())
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * blockSelected.distance, Color.yellow);
            //Debug.Log(blockSelected.point);
            //Debug.Log(blockSelected.normal);
            Debug.Log(blockSelected.transform.name);
        }
    }

    private bool WhatAmILookingAt()
    {
        return Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out blockSelected, Mathf.Infinity);
    }
}
