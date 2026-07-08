using System.ComponentModel;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.VFX;

public class PlayerInteraction : MonoBehaviour
{
    InputAction placeBlock;
    InputAction destoryBlock;
    InputAction identifyBlock;

    //Player Inputs
    [SerializeField] private InputActionReference blockInput;
    [SerializeField] private InputActionReference placeInput;
    [SerializeField] private InputActionReference breakInput;
    [SerializeField] private InputActionReference inspectInput;

    private bool blockInteraction;

    RaycastHit blockSelected;
    public byte blockId;


    [SerializeField] WorldManager worldManager; //This will be swapped to the player object when that's done
    [SerializeField] CinemachineCamera cam; // What is to be swapped to

    private Container container2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockId = 1;
    }

    private void OnEnable()
    {
        blockInput.action.performed += SelectBlock;
        blockInput.action.canceled -= SelectBlock;

        placeInput.action.performed += PlaceBlock;
        placeInput.action.canceled -= PlaceBlock;

        breakInput.action.performed += BreakBlock;
        breakInput.action.canceled -= BreakBlock;

        inspectInput.action.performed += CheckBlock;
        inspectInput.action.canceled -= CheckBlock;
    }

    

    private bool WhatAmILookingAt()
    {
        return Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out blockSelected, Mathf.Infinity);
    }

    private void SelectBlock(InputAction.CallbackContext ctx)
    {

        blockId = (byte) ctx.action.GetBindingIndexForControl(ctx.control);
        Debug.Log(blockId);
    }

    private void PlaceBlock(InputAction.CallbackContext ctx)
    {
        Debug.Log("Placed");
        WhatAmILookingAt();
        worldManager.CreateContainer(blockSelected, blockId);
    }

    private void BreakBlock(InputAction.CallbackContext ctx)
    {
        Debug.Log("Destoryed");
        WhatAmILookingAt();
        worldManager.DestroyContainer(blockSelected.transform);
    }

    private void CheckBlock(InputAction.CallbackContext ctx)
    {
        Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * blockSelected.distance, Color.yellow);
        //Debug.Log(blockSelected.point);
        //Debug.Log(blockSelected.normal);
        Debug.Log(blockSelected.transform.name);
    }


 }

