using UnityEngine;

public class HideCursor : MonoBehaviour
{
    private void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // You can add code here to unlock and show the cursor when needed
        // For example, you may want to show the cursor when you pause the game
        // and hide it again when you resume.
        // To do that, you can use Cursor.lockState and Cursor.visible accordingly.
    }
}
