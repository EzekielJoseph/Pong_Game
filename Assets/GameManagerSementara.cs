using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class GameManagerSementara : MonoBehaviour
{
    SerialPort serialPort;
    private string serialInput = "";

    // FOR LINE RENDERER
    private bool isPortConnected;


    void Awake()
    {
    }

    public void ConnectToPort(string port)
    {

        if (string.IsNullOrEmpty(port))
        {
            Debug.LogWarning("No Port Is Selected");
            return;
        }
        serialPort = new SerialPort(port, 115200);
        Debug.Log("Connecting to port: " + port);

        serialPort.ReadTimeout = 1000;
        serialPort.DataReceived += dapatData;

        if (!serialPort.IsOpen)
        {
            try
            {
                serialPort.Open();
                Debug.Log("Serial port opened successfully.");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to open serial port: " + e.Message);
            }
        }

    }

    private void dapatData(object sender, SerialDataReceivedEventArgs e)
    {
        Debug.Log("Data received from ESP");
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(5); // wait for the port to be ready
        ConnectToPort("COM10");

        yield return new WaitForSeconds(4); // wait for the initial setup
        isPortConnected = true;
    }

    void Update()
    {
        ReadSerialInput();
    }

    void ReadSerialInput()
    {
        if(!isPortConnected)
        {
            return;
        }

        if (serialPort == null)
        {
            return;
        }

        if (serialPort.IsOpen)
        {
            //serialInput = serialPort.ReadLine().Trim();
            //if (!string.IsNullOrEmpty(serialInput))
            //{
            //    Debug.Log("Received from ESP: " + serialInput);
            //}
            string data = serialPort.ReadLine();
            Debug.Log("Received from ESP: " + data);
        }
    }

    private void OnDestroy()
    {
        CloseSerialPort();
    }

    void CloseSerialPort()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                serialPort.Close();
                Debug.Log("Serial port ditutup.");
                System.Threading.Thread.Sleep(200); // beri waktu OS melepas port
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Gagal menutup port: " + e.Message);
            }
        }
    }


}
