using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 10f;

    private const float MinFov = 15f;
    private const float MaxFov = 90f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }

        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, MinFov, MaxFov);
        Camera.main.fieldOfView = fov;
    }
}
