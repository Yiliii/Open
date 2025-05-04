using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        // Reset Game State
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.currentSpawnPoint = "";
            GameStateManager.Instance.wallyDead = false;
            GameStateManager.Instance.edRoomDiscovered = false;
        }

        // Reset Inventory
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.ResetInventory();
        }
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {
        Debug.Log("game has been quit");
        Application.Quit();
    }
}
