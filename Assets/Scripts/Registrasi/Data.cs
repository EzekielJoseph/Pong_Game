using UnityEngine;

public class Data
{
    public string nama;
    public string email;
    public string domisili;
    public int umur;

    public Data(string nama, string email, string domisili, int umur)
    {
        this.nama = nama;
        this.email = email; 
        this.domisili = domisili;
        this.umur = umur;
    }

    public virtual void TampilkanDataDebug()
    {
        Debug.Log("Nama: " + nama);
        Debug.Log("Email: " + email);
        Debug.Log("Domisili: " +domisili);
        Debug.Log("Umur: " +  umur);
    }
}