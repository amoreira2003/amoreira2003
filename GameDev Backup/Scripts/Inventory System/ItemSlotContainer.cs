using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSlotContainer : InventoryHandler
{
    [HideInInspector]
    public PlayerInventoryHandler viewerHandler;

    [HideInInspector]
    public GameObject visualInventory;
    [HideInInspector]
    public string ContainerName;
    [HideInInspector]
    public TMP_Text nameText;

    void Awake()
    {
        visualInventory = transform.Find("ContainerCanvas").gameObject;
        nameText = transform.Find("ContainerCanvas/ItemContainer/NameBackground/ItemContainerDisplay").GetComponent<TMP_Text>();
    }
    private void Start()
    {
        nameText.text = ContainerName;
    }

    public override List<InventorySlotScript> getItemSlots()
    {
        List<InventorySlotScript> allItemSlots = new List<InventorySlotScript>();

        foreach (Transform inventoryTransform in visualInventory.transform)
        {
                foreach (Transform itemSlotTransform in inventoryTransform)
                {
                    if (itemSlotTransform.gameObject.GetComponent<InventorySlotScript>() != null)
                    {
                        GameObject itemSlot = itemSlotTransform.gameObject;
                        allItemSlots.Add(itemSlot.GetComponent<InventorySlotScript>());
                    }
                }

            }
            Debug.Log("Polling Everything");
            return allItemSlots;
        }

        public void setContainerIconsRaycastAcceptance(bool boolean)
        {
            Debug.Log("<color=cyan>ItemContainer: " + getItemSlots().Count + "</color>");
            foreach (InventorySlotScript itemSlot in getItemSlots())
            {
                itemSlot.setIconRayCastStatus(boolean);
                Debug.Log("Swooshed ItemContainer 2 ");
            }

            Debug.Log("Swooshed ItemContainer");
        }

        public void ToggleView(bool view)
        {
            visualInventory.SetActive(view);
        }
    }
