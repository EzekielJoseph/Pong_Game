using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanelManager : MonoBehaviour
{
    public static WinPanelManager Instance;
    public RandomSpawn randomSpawn; // Referensi ke RandomSpawn untuk mengakses metode spawn

    public GameObject panel;
    public TMP_Text winText;
    public TMP_Text countdownText;

    private Coroutine countdownCoroutine;
    private bool panelActive = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // mencegah duplikat jika ada scene reload
        }
    }

    void Start()
    {
        panel.SetActive(false);
        countdownText.gameObject.SetActive(false);
    }

    public void ShowWinPanel(string hadiah)
    {
        winText.text = hadiah;
        StartCoroutine(DelayShowPanel());
    }

    private IEnumerator DelayShowPanel()
    {
        yield return new WaitForSeconds(2f); // Delay sebelum panel tampil
        panel.SetActive(true);
        panelActive = true;

        countdownText.gameObject.SetActive(true);
        countdownCoroutine = StartCoroutine(CountdownToNextScene(15));
    }

    private IEnumerator CountdownToNextScene(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {
            countdownText.text = "Pindah dalam: " + count + " detik";
            yield return new WaitForSeconds(1f);
            count--;
        }
        LoadRegistrasiScene();
    }

    public void SkipCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        LoadRegistrasiScene();
    }

    private void LoadRegistrasiScene()
    {
        panelActive = false;
        randomSpawn.closePort();
        SceneManager.LoadScene("Registrasi");
    }

    public bool IsPanelActive()
    {
        return panelActive;
    }
}
