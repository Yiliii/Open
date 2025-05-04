using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EdRoomExitTrigger : MonoBehaviour
{
    public string targetScene = "WallyConfront"; 
    public string targetSpawnPoint = "Spawn2"; 

    private bool canInteract = false;
    private bool hasTriggered = false;

    private void Update()
    {
        if (hasTriggered || !canInteract) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            hasTriggered = true;

            GameStateManager.Instance.edRoomDiscovered = true;

            if (!string.IsNullOrEmpty(targetSpawnPoint))
                GameStateManager.Instance.currentSpawnPoint = targetSpawnPoint;

            SceneManager.LoadScene(targetScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            canInteract = false;
    }
}
