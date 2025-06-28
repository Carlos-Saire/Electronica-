using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform mainCameraTransform;
    private Vector2 currentInput; 
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        mainCameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    private void GetPlayerMove(Vector2 input)
    {
        currentInput = input; 
    }

    private void FixedUpdate()
    {
        Vector3 forward = mainCameraTransform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = mainCameraTransform.right;
        right.y = 0;
        right.Normalize();

        Vector3 moveDir = forward * currentInput.y + right * currentInput.x;
        if (moveDir.magnitude > 1f)
            moveDir.Normalize();

        rb.linearVelocity = moveDir * moveSpeed + new Vector3(0, rb.linearVelocity.y, 0);
    }

    private void OnEnable()
    {
        InputReader.OnPlayerMove += GetPlayerMove;
    }
    private void OnDisable()
    {
        InputReader.OnPlayerMove -= GetPlayerMove;
    }
}
