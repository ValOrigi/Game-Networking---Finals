using TMPro;
using UnityEngine;

public class PlayerChampionCustomizeUI : MonoBehaviour
{
    [SerializeField] GameObject ErrorPanel;
    [SerializeField] TextMeshProUGUI errorMessage;

    private void Start()
    {
        ErrorPanel.SetActive(false);
    }

    public void MissingArmor()
    {
        ErrorPanel.SetActive(true);
        errorMessage.text = "You have no armor.";
    }
    public void UnusedPoints()
    {
        ErrorPanel.SetActive(true);
        errorMessage.text = "You have unused points.";
    }
}
