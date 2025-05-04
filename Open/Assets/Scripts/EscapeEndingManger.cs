using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeEndingManager : MonoBehaviour
{
    [Tooltip("Name of the ending scene if Ed room not discovered")]
    public string undiscoveredEndingScene = "Escape Ending";

    [Tooltip("Name of the ending scene if Ed room discovered")]
    public string discoveredEndingScene = "Escape_after_discovery_Ending";

    private bool canInteract = false;

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            string targetScene = GameStateManager.Instance.edRoomDiscovered
                ? discoveredEndingScene
                : undiscoveredEndingScene;

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