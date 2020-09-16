using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{
    public Vector3 targetPosition;
    public float distance;
    public float xSpeed;
    public float ySpeed;
    public float rotationSenitivity;
    public float scrollSensitivity;
    public float yMinLimit;
    public float yMaxLimit;
    public float distanceMin;
    public float distanceMax;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    void Start()
    {
        // Apply default parameters
        distance = 100f;
        xSpeed = 25.0f;
        ySpeed = 250.0f;
        rotationSenitivity = 0.005f;
        scrollSensitivity = 25;
        yMinLimit = -20f;
        yMaxLimit = 9999.9f;
        distanceMin = 2f;
        distanceMax = 1000f;

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (targetPosition != null)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * rotationSenitivity;
            y -= Input.GetAxis("Mouse Y") * ySpeed * rotationSenitivity;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity, distanceMin, distanceMax);

            /*
            RaycastHit hit;
            if (Physics.Linecast(targetPosition, transform.position, out hit))
            {
                distance -= hit.distance;
            }
            */

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + targetPosition;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}