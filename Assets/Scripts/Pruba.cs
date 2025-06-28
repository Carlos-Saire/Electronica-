using UnityEngine;
using System.IO.Ports;

public class RFIDReaderUnity : MonoBehaviour
{
    SerialPort serialPort;

    void Start()
    {
        // Cambia "COM4" por el puerto correcto en tu PC
        serialPort = new SerialPort("COM3", 9600);
        try
        {
            serialPort.Open();
            serialPort.ReadTimeout = 100; 
            Debug.Log("Puerto serial abierto");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error abriendo puerto serial: " + e.Message);

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
                    string uid = serialPort.ReadLine().Trim(); // lee la línea y limpia saltos de línea
                    Debug.Log("UID recibido: " + uid);

                    if (uid == "04a26b3fd1")  // Cambia por tu UID real
                    {
                        Debug.Log("¡Tarjeta autorizada detectada!");
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Error leyendo puerto serial: " + e.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("Puerto serial cerrado");
        }
    }
}
