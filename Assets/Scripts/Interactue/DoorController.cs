using UnityEngine;
using System.Collections;

public class DoorController : InteractiveObject
{
    private string lastKeyReceived = "";
    private bool doorAuthorized = false;

    [Header("Rotation")]
    [SerializeField] private Vector3 rotationOpen;
    private Quaternion open;
    private Quaternion close;

    private void Start()
    {
        open = Quaternion.Euler(rotationOpen);
        close = transform.rotation;
        lastKeyReceived = "";
    }

    private void Update()
    {
        // Si el jugador está mirando esta puerta y llegó una tarjeta recientemente
        if (input && doorAuthorized)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, open, 5 * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, close, 5 * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        ArduinoObserver.OnCardId += GetId;
    }

    private void OnDisable()
    {
        ArduinoObserver.OnCardId -= GetId;
    }

    private void GetId(string id)
    {
        if (input)
        {
            lastKeyReceived = id.Trim();
            doorAuthorized = true;
            StartCoroutine(ResetAuthorization(3f)); 
        }
    }

    private IEnumerator ResetAuthorization(float delay)
    {
        yield return new WaitForSeconds(delay);
        doorAuthorized = false;
        lastKeyReceived = "";
    }
}
