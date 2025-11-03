using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UserDisplay : MonoBehaviour
{
    public TMP_Text namaText;
    public TMP_Text emailText;
    public TMP_Text domisiliText;
    public TMP_Text umurText;

    void Start()
    {
        Data userData = UserDataManager.Instance.GetData();
        if (userData != null)
        {
            TampilkanUserData(userData);
        }
        else
        {
            Debug.LogWarning("Tidak ada data user ditemukan.");
        }
    }

    public void TampilkanUserData(Data data)
    {
        namaText.text = "Nama: " + data.nama;
        emailText.text = "Email: " + data.email;
        domisiliText.text = "Domisili: " + data.domisili;
        umurText.text = "Umur: " + data.umur.ToString();
    }

    public void onBackButtonClicked()
    {
        SceneManager.LoadScene("Registrasi");
    }
}
