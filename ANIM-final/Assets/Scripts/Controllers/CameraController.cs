using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform[] pinPoints;
    [SerializeField] float speed;
    [SerializeField] int index = 0;

    Vector3 target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void Update()
    {
        target = pinPoints[index].position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        UpdateCameraPosition();
    }

    void UpdatePOV(int direction) { index = (index + direction) % pinPoints.Length; }


    void UpdateCameraPosition() {
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
        transform.forward = pinPoints[index].forward;
    }
}
