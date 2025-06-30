using System.IO.Ports;
using UnityEngine;

public class GarraControl : MonoBehaviour
{
    SerialPort puerto = new SerialPort("COM3", 9600); 

    public Transform garra;
    public float velocidad = 0.01f;

    private void Start()
    {
        try
        {
            if (!puerto.IsOpen)
            {
                puerto.Open();
                puerto.ReadTimeout = 100;
                Debug.Log("Conectado a Arduino");
            }
        }
        catch
        {
            Debug.LogWarning("No se pudo abrir el puerto. Revisa el COM o conexión.");
        }
    }

    private void Update()
    {
        LeerJoystickYControlarGarra();
        VerificarCajaSimulada(); // ← test teclado
    }

    void LeerJoystickYControlarGarra()
    {
        if (puerto.IsOpen)
        {
            try
            {
                string data = puerto.ReadLine();
                if (data.StartsWith("JOY:"))
                {
                    string[] valores = data.Replace("JOY:", "").Trim().Split(',');

                    if (valores.Length == 2 &&
                        int.TryParse(valores[0], out int x) &&
                        int.TryParse(valores[1], out int y))
                    {
                        float ejeX = (x - 512) / 512f;
                        float ejeY = (y - 512) / 512f;

                        Vector3 movimiento = new Vector3(ejeX, ejeY, 0f) * velocidad * Time.deltaTime;
                        garra.position += movimiento;
                    }
                }
            }
            catch { /* dato incompleto */ }
        }
    }

    void VerificarCajaSimulada()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            puerto.Write("B");
            Debug.Log("Caja INCORRECTA → LED rojo + buzzer");
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            puerto.Write("G");
            Debug.Log("Caja CORRECTA → LED verde");
        }
    }

    private void OnApplicationQuit()
    {
        if (puerto.IsOpen)
            puerto.Close();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!puerto.IsOpen) return;

        if (other.CompareTag("CajaBuena"))
        {
            puerto.Write("G");
            Debug.Log("✅ Caja buena detectada");
        }
        else if (other.CompareTag("CajaMala"))
        {
            puerto.Write("B");
            Debug.Log("❌ Caja mala detectada");
        }
    }
}