using UnityEngine;

/// <summary>
/// Monitors the player's vertical position and resets them to their initial
/// starting point if they fall below a certain Y-level.
/// This script is intended to be attached to the main VR Camera Rig or Player object.
/// </summary>
public class PlayerReset : MonoBehaviour
{
    [Tooltip("The Y-coordinate below which the player will be reset.")]
    public float resetHeight = 0.0f;

    // Stores the position the player was at when the scene started.
    private Vector3 initialPosition;

    void Awake()
    {
        // Capture the player's position at the start of the game.
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Check the current Y position of the player object.
        if (transform.position.y <= resetHeight)
        {
            ResetPlayerPosition();
        }
    }

    /// <summary>
    /// Teleports the player to their initial starting point.
    /// </summary>
    private void ResetPlayerPosition()
    {
        // Set the player's position directly to the initial position.
        transform.position = initialPosition;

        // Optionally, reset velocity if the player object has a Rigidbody component
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Debug.Log("Player fell below Y=" + resetHeight + " and was reset to the initial starting position: " + initialPosition);
    }

    // Optional: Draw a visual cue in the Scene view to show the reset height
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Draw a horizontal line at the resetHeight Y-level
        Vector3 start = new Vector3(transform.position.x - 5, resetHeight, transform.position.z);
        Vector3 end = new Vector3(transform.position.x + 5, resetHeight, transform.position.z);
        Gizmos.DrawLine(start, end);
    }
}