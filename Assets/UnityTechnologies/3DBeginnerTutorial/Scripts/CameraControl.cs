using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player; // Assign your player's transform here
    public float mouseSensitivity = 100f;
    public Vector3 offset; // Offset position of the camera relative to the player

    private Vector3 lastPlayerPosition;

    void Start()
    {
        offset = transform.position - player.position;
        lastPlayerPosition = player.position;
    }

    void LateUpdate()
    {
        // Calculate player movement since last frame
        Vector3 playerMovement = player.position - lastPlayerPosition;

        // Apply the movement to the camera
        transform.position += playerMovement;

        // Update last player position
        lastPlayerPosition = player.position;

        // Handle camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.RotateAround(player.position, Vector3.up, mouseX);

        // Optional: Adjust the camera to always look at the player
        transform.LookAt(player);
    }
}
