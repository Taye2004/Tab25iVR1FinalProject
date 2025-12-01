using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles scene loading operations.
/// To use this, attach it to a GameObject (like an empty GameObject or a Button).
/// Then, call the public methods from your UI or game logic.
/// NOTE: The scenes you want to load MUST be added to the Build Settings (File -> Build Settings).
/// </summary>
public class SceneLoader : MonoBehaviour
{
    // Important: Use the exact name of the scene as it appears in the Build Settings.
    [Tooltip("The name of the scene you want to load (e.g., 'Level2').")]
    public string sceneToLoad = "Sports"; // Updated default scene name to "Sports"

    /// <summary>
    /// Loads the scene specified by the 'sceneToLoad' variable.
    /// This is the best method to link to a UI button's OnClick event.
    /// </summary>
    public void LoadNextScene()
    {
        // Check if the scene name is valid before attempting to load
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogError("Scene name is not set in the inspector! Please provide a valid scene name.");
            return;
        }

        try
        {
            // Load the scene synchronously (blocking until the scene is loaded)
            SceneManager.LoadScene(sceneToLoad);
            Debug.Log("Successfully loading scene: " + sceneToLoad);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load scene '{sceneToLoad}'. Make sure it's added to File > Build Settings. Error: {e.Message}");
        }
    }

    /// <summary>
    /// A generic method to load a scene by its name, useful if you have multiple buttons
    /// calling the same script but passing different scene names.
    /// </summary>
    /// <param name="name">The name of the scene to load.</param>
    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// Loads the next scene in the build order list.
    /// </summary>
    public void LoadNextSceneInBuildOrder()
    {
        // Get the current scene's index in the Build Settings
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Calculate the index of the next scene (wraps around to 0 if at the last scene)
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        SceneManager.LoadScene(nextSceneIndex);
    }

    /// <summary>
    /// Quits the application (only works in a built game).
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

        // Optional: Stop play mode in the Unity editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}