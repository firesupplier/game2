using UnityEngine;
using UnityEngine.InputSystem;

// More about MonoBehaviour class: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/MonoBehaviour.html

// MonoBehaviour offers methods that execute at different stages of the running program (Start, Update, Awake, ...).
// You can read more about these methods and the program lifecycle here: https://docs.unity3d.com/6000.0/Documentation/Manual/execution-order.html

// Our PlaneScript makes the plane rotate on input from the user (keyboard, gamepad, joystick, ...)
// This is achieved via the Unity's Input System. More info here: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.8/manual/index.html

public class PlaneScript : MonoBehaviour
{

    // Public variables are visible in the Inspector panel inside Unity
    public float sensitivity = 0.1f; // A factor that controls the speed of rotating

    // Private variables are only visible to this script
    private InputAction moveAction; // The action that moves our plane

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // From our InputSystem, we get the action with the name "Move"
        // You can find and manage the list of actions in Unity > Edit > Project Settings > Input System Package
        this.moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        // Each frame, we read the value of our action.
        // The "Move" action returns a 2D vector, where:
        //    * component x can have values -1 (backward, e.g. by pressing the key S), 0 (no relevant key is pressed), or 1 (forward, e.g. by pressing the key W)
        //    * component y can have values -1 (left, e.g. by pressing the key A), 0 (no relevant key is presed), or 1 (right, e.g. by pressing the key D)
        Vector2 moveDirection = this.moveAction.ReadValue<Vector2>();

        // We construct an Euler rotation, rotating on X and Z axis
        Vector3 newEulerRotation = new Vector3(moveDirection.x, 0.0f, moveDirection.y);
        // We scale the rotation vector so the plane does not rotate too fast
        Vector3 scaledRotation = newEulerRotation * this.sensitivity;
        
        // We get the transform component of our object and perform the rotation
        this.transform.Rotate(scaledRotation);
    }
}
