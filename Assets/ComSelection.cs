using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;

public class ComSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Dropdown dropdown;
    string[] portNames;
    public RandomSpawn randomspawn;

    void Start()
    {
        //dropdown.onValueChanged.AddListener(valuhanged);
        getPorts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueChanged(int index)
    {
        Debug.Log(portNames[index]);
        UserDataManager.Instance.Port = portNames[index];
    }

    public void getPorts()
    {
        Debug.Log("refresh");
        portNames = SerialPort.GetPortNames();

        List<TMP_Dropdown.OptionData> optiondata = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < portNames.Length; i++)
        {
            TMP_Dropdown.OptionData val = new TMP_Dropdown.OptionData();
            val.text = portNames[i];
            optiondata.Add(val);
        }

        dropdown.options = optiondata;
    }
}
