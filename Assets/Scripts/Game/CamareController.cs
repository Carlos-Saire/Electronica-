using UnityEngine;

public class CamareController : MonoBehaviour
{
    [Header("Camera Settings")]
    public float sensitivity = 2f;

    private float rotationX = 0f;
    private Transform playerBody;

    [Header("Raycast")]
    [SerializeField] LayerMask objectLayer;
    private RaycastHit raycastObject;
    private bool confirmsInput = true;
    InteractiveObject interactive;
    private Transform GetCamera()
    {
        return transform;
    }

    private void OnEnable()
    {
        PlayerController.OnGetCamera += GetCamera;
    }

    private void OnDisable()
    {
        PlayerController.OnGetCamera -= GetCamera;
    }

    private void Start()
    {
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateCamera();
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward * 2f, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out raycastObject, 2f, objectLayer))
        {
            if (confirmsInput)
            {
                interactive = raycastObject.collider.gameObject.GetComponent<InteractiveObject>();
                interactive.SetInput(true);
                confirmsInput = false;
            }
        }
        else
        {
            if (!confirmsInput)
            {
                interactive.SetInput(false);
                interactive = null;
                confirmsInput = true;
            }
        }
    }
    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
