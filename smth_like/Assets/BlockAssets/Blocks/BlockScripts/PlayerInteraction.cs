using System.ComponentModel;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.VFX;

public class PlayerInteraction : MonoBehaviour
{

    //Player Inputs
    [SerializeField] private InputActionReference blockInput;
    [SerializeField] private InputActionReference placeInput;
    [SerializeField] private InputActionReference breakInput;
    [SerializeField] private InputActionReference inspectInput;
    [SerializeField] private InputActionReference factoryInput;


    [SerializeField] BlockFactory blockFactory; //This will be swapped to the player object when that's done
    [SerializeField] CinemachineCamera cam; // What is to be swapped to

    private Container container2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    int blockId = 0;

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

        //factoryInput.action.performed += StartFactory;
        //factoryInput.action.canceled -= StartFactory;
    }

    

    private RaycastHit WhatAmILookingAt()
    {
        RaycastHit blockSelected;
        Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out blockSelected, Mathf.Infinity);
        return blockSelected;
    }

    private void SelectBlock(InputAction.CallbackContext ctx)
    {

        blockId = ctx.action.GetBindingIndexForControl(ctx.control);
        Debug.Log(blockId);
    }

    private void PlaceBlock(InputAction.CallbackContext ctx)
    {
        Debug.Log("Placed");
        RaycastHit blockChosen = WhatAmILookingAt();
     
        blockChosen.transform.GetComponent<Block>().CreateBlock(blockChosen, blockId);
    }

    private void BreakBlock(InputAction.CallbackContext ctx) //Move to block class rather than worldManager 
    {
        Debug.Log("Destoryed");
        RaycastHit blockChosen = WhatAmILookingAt();
        blockChosen.transform.GetComponent<Block>().DestroyBlock(blockChosen.transform);
    }

    private void CheckBlock(InputAction.CallbackContext ctx)
    {
        RaycastHit blockSelected = WhatAmILookingAt();
        Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * blockSelected.distance, Color.yellow);
        //Debug.Log(blockSelected.point);
        //Debug.Log(blockSelected.normal);
        Debug.Log(blockSelected.transform.name);
    }

    //private void StartFactory(InputAction.CallbackContext ctx) //Move this to the block itself
    //{
        //WhatAmILookingAt();
        //worldManager.FactoryCheck(blockSelected);
    //}


 }

