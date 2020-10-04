using UnityEngine;
using UnityEngine.UI;

public class Playerprefs : MonoBehaviour
{
    private string value;
    [SerializeField] private InputField NameinputField;
    [SerializeField] private GameObject PlayerprefsSetPanel;


    private void Start()
    {
        if(PlayerPrefs.GetString("name") == null)
        {
            PlayerprefsSetPanel.SetActive(true);
        }
        else
        {
            PlayerprefsSetPanel.SetActive(false);
        }
    }

    public void SetPlayerprefs()
    {
        value = NameinputField.text;
        PlayerPrefs.SetString("name", value);
        PlayerPrefs.Save();
        NameinputField.text = "";
        PlayerprefsSetPanel.SetActive(false);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("name");
        PlayerPrefs.Save();
        PlayerprefsSetPanel.SetActive(true);
    }

}
