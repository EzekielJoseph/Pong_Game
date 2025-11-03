using UnityEngine;
using System.IO.Ports;

public class RandomSpawn : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxX = 2.6f;
    public float minX = -2.6f;
    public float xPos = 0f;
    public float yPosition = 6.3f;
    public bool isBallRelease = false;

    private Vector2 spawnPosition;
    private SerialPort controlSerial;

    public void ConnectToPort(string port)
    {
        controlSerial = new SerialPort(port, 115200);
        Debug.Log("Trying to connect to port " + port);

        if (!controlSerial.IsOpen)
        {
            try
            {
                controlSerial.Open();
                Debug.Log("Serial port opened successfully.");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to open serial port: " + e.Message);
            }
        }
    }

    void Start()
    {
        ConnectToPort(UserDataManager.Instance.Port);
        spawnPosition = new Vector2(xPos, yPosition);
        transform.position = spawnPosition;

        rb.isKinematic = true;
    }

    void Update()
    {

        // Baca input dari serial jika ada
        string data = null;
        if(controlSerial!=null){
            if(controlSerial.IsOpen && controlSerial.BytesToRead > 0)
        {
                try
                {
                    data = controlSerial.ReadLine().Trim();
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error reading from serial port: " + e.Message);
                }
            }
        }

        bool isPanelActive = WinPanelManager.Instance != null && WinPanelManager.Instance.IsPanelActive();

        // Kontrol gerakan kiri-kanan jika panel belum aktif
        if (!isPanelActive)
        {
            if (data == "LEFT" || Input.GetKey(KeyCode.LeftArrow))
            {
                xPos -= 0.1f;
            }
            else if (data == "RIGHT" || Input.GetKey(KeyCode.RightArrow))
            {
                xPos += 0.1f;
            }
            else if ((data == "SPACE" || Input.GetKeyDown(KeyCode.Space)) && !isBallRelease)
            {
                rb.isKinematic = false;
                rb.velocity = Vector2.zero;
                isBallRelease = true;
                Debug.Log("Spawned at: " + spawnPosition);
            }
        }

        if ((data == "SPACE" || Input.GetKeyDown(KeyCode.Space)) && isPanelActive)
        {
            WinPanelManager.Instance.SkipCountdown();
        }

        xPos = Mathf.Clamp(xPos, minX, maxX);

        if (!isBallRelease)
        {
            spawnPosition = new Vector2(xPos, yPosition);
            transform.position = spawnPosition;
        }
    }

    void OnApplicationQuit()
    {
        closePort();
    }

    public void closePort()
    {
        if (controlSerial != null && controlSerial.IsOpen)
        {
            controlSerial.Close();
            Debug.Log("Serial port closed.");
        }
    }
}
