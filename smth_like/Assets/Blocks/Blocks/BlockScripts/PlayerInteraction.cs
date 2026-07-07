using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerInteraction : MonoBehaviour
{
    InputAction placeBlock;
    InputAction destoryBlock;

    private bool blockInteraction;


    [SerializeField] WorldManager worldManager; //This will be swapped to the player object when that's done

    private Container container2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        placeBlock = InputSystem.actions.FindAction("Place");
        destoryBlock = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        if (placeBlock.IsPressed() && !blockInteraction)
        {
            blockInteraction = true;
            Debug.Log("Placed");
            worldManager.CreateContainer();

        }
        if (destoryBlock.IsPressed() && !blockInteraction)
        {
            blockInteraction = true;
            Debug.Log("Destoryed");
            worldManager.DestroyContainer();
        }
        if (!placeBlock.IsPressed() && !destoryBlock.IsPressed())
        {
            blockInteraction = false;
        }
    }
}
