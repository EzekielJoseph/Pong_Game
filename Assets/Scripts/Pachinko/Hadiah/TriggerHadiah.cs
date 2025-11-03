using UnityEngine;

public class TriggerHadiah : MonoBehaviour
{
    public string hadiahMessage = "";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log(hadiahMessage);
            WinPanelManager panel = FindAnyObjectByType<WinPanelManager>();
            if (panel != null)
            {
                panel.ShowWinPanel(hadiahMessage);
            }
        }
    }
}
