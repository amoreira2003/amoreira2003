using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotScript : MonoBehaviour
{

    [Header("Assignable Fields")]
    [Space(1f)]
    public Item item;

    [Space(1f)]
    public int quantity = 1;

    [Space(4f)]

    [Header("Prefabs Reference")]
    [Space(1f)]
    public GameObject IconDisplay;

    TMP_Text quantityText;
    Image icon;
    GameObject player;


    bool displayQuantity = true;
    bool isHotbar = false;

    void Awake()
    {
        player = GameObject.Find("Player");
        icon = IconDisplay.GetComponent<Image>();
        quantityText = GetComponentInChildren<TMP_Text>();
    }
    void Start()
    {
        isHotbar = transform.parent.gameObject.tag.Equals("Hotbar");
    }
    void Update()
    {
        if (item == null)
        {
            icon.enabled = false;
            icon.sprite = null;
            quantityText.enabled = false;
            return;
        }

        icon.enabled = true;

        if (icon.sprite != item.icon)
        {
            icon.sprite = item.icon;
        }

        if (quantity > 1)
        {
            if (!quantityText.enabled && displayQuantity) quantityText.enabled = true;
            quantityText.text = quantity.ToString();
        }
        else
        {
            quantityText.enabled = false;
        }
    }

    public bool isHotbarItemSlot() {
        return isHotbar;
    }

    public string getQuantitySuffix() {
        return item.quantitySuffix;
    }

    public Item getItem()
    {
        return this.item;
    }

    public Sprite getItemIcon()
    {
        return getItem().icon;
    }

    public void setItem(Item newItem)
    {
        this.item = newItem;
    }

    public int getQuantity()
    {
        return this.quantity;
    }

    public bool isSingleItem()
    {
        return getQuantity() == 1;
    }

    public bool hasItem()
    {
        return !(getItem() == null);
    }


    public Image getIconObject()
    {
        return this.icon;
    }

    public bool isStackable()
    {
        return !(getItem().nonStackable);
    }

    public int getItemMaxStack()
    {
        return getItem().maxStack;
    }

    public void setQuantity(int newQuantity)
    {
        this.quantity = newQuantity;
    }

    public string getItemDescription()
    {
        return getItem().description;
    }

    public string getItemName()
    {
        return getItem().name;
    }

    public void removeQuantity(int newQuantity)
    {
        if (getQuantity() - newQuantity < 0)
        {
            setQuantity(0);
            return;
        }

        this.quantity = getQuantity() - newQuantity;
    }


    public void setIconRayCastStatus(bool boolean)
    {
        icon.raycastTarget = boolean;
    }

    public void setDisplayQuantity(bool toggle)
    {
        displayQuantity = toggle;
        quantityText.enabled = toggle;
    }

    public void Drop()
    {
        GameObject dropGameObject = Instantiate(item.dropGameObject, player.transform.position + player.transform.forward * 3 + player.transform.up, player.transform.rotation);
        Rigidbody rb = dropGameObject.GetComponent<Rigidbody>();
        Transform dropTransform = dropGameObject.transform;
        ItemDropScript dropScript = dropGameObject.GetComponent<ItemDropScript>();
        dropScript.setQuantity(quantity);
        dropScript.setItem(item);
        dropTransform.Rotate(Vector3.forward * 90f);
        rb.AddForce(player.transform.forward * 2f + player.transform.up * 1f, ForceMode.VelocityChange);
        setItem(null);
        quantity = 0;
    }

}
