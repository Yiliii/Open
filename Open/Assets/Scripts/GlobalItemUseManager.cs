using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalItemUseManager : MonoBehaviour
{
    private InventoryUIController inventoryUI;
    public GameObject interactionEmote;

    void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUIController>();
        if (interactionEmote) interactionEmote.SetActive(false);
    }

    void Update()
    {
        if (inventoryUI != null && inventoryUI.IsInventoryOpen()) return;
        CollectableItem held = InventoryManager.Instance.itemInHand;
        if (held != null && held.itemName == "Knife"){
            if (interactionEmote) interactionEmote.SetActive(true);
        }
        else{
                if (interactionEmote) interactionEmote.SetActive(false);
            }

        // Allow suicide anywhere
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (held != null && held.itemName == "Knife")
            {
                // Only allow suicide if NOT in final scene or Wally scene manager doesn't exist
                if (GameObject.FindObjectOfType<FinalWallyInteractionManager>() == null)
                {
                    SceneManager.LoadScene("suicide_ending");
                }
            }
            
        }
    }
}