using UnityEngine;
using UnityEngine.InputSystem;

public class PlaneController : MonoBehaviour
{
    public float throttleIncrement = 0.1f;

    public float maxThrottle = 200f;

    public float responsiveness = 10f;

    private float throttle;
    private float roll, pitch, yaw;
    private Rigidbody rb;


    PlaneInputAction action;

    private float responseModifier {
        get { return (rb.mass / 10f) * responsiveness; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        action = new PlaneInputAction(); // à changer quand il y aura un manager pour éviter que l'avion ne crée son propre controlleur
        action.Plane.Enable();
    }

    private void Update()
    {
        ThrottleInput();
        axisInputs();
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * maxThrottle * throttle);
        rb.AddTorque(transform.up * yaw * responseModifier);
        rb.AddTorque(transform.right * pitch * responseModifier);
        rb.AddTorque(transform.forward * roll * responseModifier);
    }

    void ThrottleInput() {
        float input = action.Plane.Throttle.ReadValue<float>();
        if (input > 0) 
        {
            throttle += throttleIncrement;
        }
        else if (input < 0)
        {
            throttle -= throttleIncrement;
        }
        throttle = Mathf.Clamp(throttle, 0, maxThrottle);
    }

    void axisInputs()
    {
        roll = action.Plane.Roll.ReadValue<float>();
        pitch = action.Plane.Pitch.ReadValue<float>();
        yaw = action.Plane.Yaw.ReadValue<float>();
    }

    


}
