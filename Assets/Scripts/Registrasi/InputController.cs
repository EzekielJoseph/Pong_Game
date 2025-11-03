using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    public TMP_InputField namaInput;
    public TMP_InputField emailInput;
    public TMP_InputField domisiliInput;
    public TMP_InputField umurInput;

    public GameObject assetPanel; 
    public AudioClip buttonClickSfx;
    private AudioSource audioSource;


    void Start()
    {
        if (assetPanel != null)
        {
            assetPanel.SetActive(false); // Hide panel saat scene dimulai
        }

        audioSource = GetComponent<AudioSource>();
    }

    void PlayClickSfx()
    {
        if(audioSource != null && buttonClickSfx != null)
        {
            audioSource.PlayOneShot(buttonClickSfx);
        }
    }

    public void onRegister()
    {
        string nama = namaInput.text;
        string email = emailInput.text;
        string domisili = domisiliInput.text;
        int umur = int.Parse(umurInput.text);

        Data userData = new Data(nama, email, domisili, umur);

        userData.TampilkanDataDebug();

        UserDataManager.Instance.SetData(userData);

        SceneManager.LoadScene("Game");
    }

    public void onAssetClick()
    {
        PlayClickSfx(); // Mainkan efek suara saat tombol diklik
        assetPanel.SetActive(true); // Tampilkan panel attribution
    }

    public void onBackClick()
    {
        PlayClickSfx(); // Mainkan efek suara saat tombol diklik
        assetPanel.SetActive(false); // Sembunyikan panel attribution
    }
}
