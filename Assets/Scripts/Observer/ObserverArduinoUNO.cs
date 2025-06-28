using System.IO.Ports;
using UnityEngine;
using System;

public class ArduinoObserver : MonoBehaviour
{
    public static event Action<string> OnCardId;
    public static ArduinoObserver Instance;

    private SerialPort serialPort;

    [SerializeField] private string portName = "COM3";
    [SerializeField] private int baudRate = 9600;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance=this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open();
            serialPort.ReadTimeout = 100;
            Debug.Log("Serial port opened");
        }
        catch (Exception e)
        {
            Debug.LogError("Error opening serial port: " + e.Message);
        }
    }

    void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                if (serialPort.BytesToRead > 0)
                {
                    string uid = serialPort.ReadLine().Trim();
                    Debug.Log("UID received: " + uid);

                    OnCardId?.Invoke(uid);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error reading serial port: " + e.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("Serial port closed");
        }
    }
}
