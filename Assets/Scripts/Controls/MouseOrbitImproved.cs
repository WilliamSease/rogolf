using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{
    public const int LEFT_MOUSE_BUTTON = 0;
    public const int RIGHT_MOUSE_BUTTON = 1;

    public Vector3 targetPosition;
    public float distance;
    public float xSpeed;
    public float ySpeed;
    public float xRotationSenitivity;
    public float yRotationSenitivity;
    public float scrollSensitivity;
    public float yMinimum;
    public float yMaximum;
    public float minimumDistance;
    public float maximumDistance;

    private Rigidbody controlsRigidbody;

    float x = 0.0f;
    float y = 0.0f;
    
    public float mouseSensitivity = 1.0f;

    void Start()
    {
        // Apply default parameters
        distance = 100f;
        xSpeed = 25.0f;
        ySpeed = 500.0f;
        xRotationSenitivity = 0.002f * 50f;
        yRotationSenitivity = 0.002f;
        scrollSensitivity = 100;
        yMinimum = -20f;
        yMaximum = 60f;
        minimumDistance = 2f;
        maximumDistance = 1000f;

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        controlsRigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (controlsRigidbody != null)
        {
            controlsRigidbody.freezeRotation = true;
        }
    }
    
    void LateUpdate()
    {
        if (targetPosition != null)
        {
            if (Input.GetMouseButton(RIGHT_MOUSE_BUTTON))
            {   
                x += Input.GetAxis("Mouse X") * xSpeed * xRotationSenitivity * mouseSensitivity;
                y -= Input.GetAxis("Mouse Y") * ySpeed * yRotationSenitivity * mouseSensitivity;
            }
            y = ClampAngle(y, yMinimum, yMaximum);
            
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity, minimumDistance, maximumDistance);

            Vector3 negativeDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negativeDistance + targetPosition;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360F;
        if (angle > 360f) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
    
    public static void SetMouseSensitivity(float to)
    {
        MouseOrbitImproved MOI = FindObjectOfType<MouseOrbitImproved>();
        if (MOI != null) MOI.mouseSensitivity = to;
    }
}