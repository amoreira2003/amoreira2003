using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    GameObject canvasObject;
    GameObject hotbarObject;
    GameObject inventoryObject;

    GameObject equipmentObject;
    PlayerInventoryHandler inventoryHandler;

    bool isViewingInventory = false;
    Canvas canvas;

    public LayerMask pickableMask;
    public LayerMask containerMask;

    PlayerFpsCamera cameraScript;

    public TMP_Text itemDisplayText;

    public InventorySlotScript currentEquiped;

    public InventorySlotScript[] hotbarSlots;


    void Awake()
    {
        cameraScript = transform.Find("Main Camera").GetComponent<PlayerFpsCamera>();
        inventoryObject = transform.Find("UI/InventoryHandler/Inventory").gameObject;
        hotbarObject = transform.Find("UI/InventoryHandler/Hotbar").gameObject;
        canvasObject = transform.Find("UI").gameObject;
        inventoryHandler = inventoryObject.transform.parent.GetComponent<PlayerInventoryHandler>();
    }

    void Update()
    {
        hotbarSlotEquipment();

        if (Input.GetKeyDown(KeyCode.Tab)) isViewingInventory = !isViewingInventory; ToggleInventory();

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hitContainer, 5f, containerMask))
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.blue);
            Debug.Log("Shoot ContainerMask Raycast");
            var containerObject = hitContainer.collider.gameObject;
            Debug.Log(containerObject);
            ItemSlotContainer slotContainerScript = containerObject.GetComponent<ItemSlotContainer>();
            if (slotContainerScript == null) return;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Checkpoint 1");
                isViewingInventory = true;
                ToggleInventory();
                inventoryHandler.currentViewingSlot = slotContainerScript;
                slotContainerScript.viewerHandler = inventoryHandler;
                slotContainerScript.ToggleView(true);
            }

        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, 5f, pickableMask))
        {
            var dropObject = hit.collider.gameObject;
            ItemDropScript dropScript = dropObject.GetComponent<ItemDropScript>();
            if (dropObject == null) return;

            itemDisplayText.text = dropScript.getItemName() + $" ({dropScript.getQuantity()})";

            if (Input.GetKeyDown(KeyCode.E))
            {
                dropScript.tryToPick(inventoryHandler);
            }
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.green);
            Debug.Log($"Looking at {dropObject.name}", this);
        }
        else
        {
            if (itemDisplayText.text != string.Empty) itemDisplayText.text = string.Empty;
        }
    }

    void ToggleInventory()
    {
        inventoryObject.SetActive(isViewingInventory);
        cameraScript.setFpsCameraActive(!isViewingInventory);
        if (!isViewingInventory)
        {
            if (inventoryHandler.currentViewingSlot == null) return;
            inventoryHandler.currentViewingSlot.ToggleView(false);
            inventoryHandler.currentViewingSlot = null;
        }
    }

    void hotbarSlotEquipment()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            onEquip(hotbarSlots[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            onEquip(hotbarSlots[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            onEquip(hotbarSlots[2]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            onEquip(hotbarSlots[3]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            onEquip(hotbarSlots[4]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            onEquip(hotbarSlots[5]);
        }
    }

    void onEquip(InventorySlotScript slot)
    {

        if (currentEquiped == slot)
        {
            currentEquiped = null;
            return;
        }

        currentEquiped = slot;

        List<GameObject> equipments = new List<GameObject>();
        foreach (Transform equipmentObject in transform)
        {
            equipments.Add(equipmentObject.gameObject);
        }
    }

}
