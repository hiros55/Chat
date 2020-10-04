using UnityEngine;
using UnityEngine.UI;

public class Properties : MonoBehaviour
{
    
    [SerializeField] private GameObject StartCanvas;
    [SerializeField] private GameObject ChatCanvas;
    [SerializeField] private GameObject RoomDisplay;
    [SerializeField] private GameObject CreateButton;
    [SerializeField] private static string RoomName;
    public GameObject RoomSetPanel;
    public GameObject RoomPropertyPanel;
    public GameObject OnlyCopyPanel;
    public  GameObject EmptyRoomText;
    public GameObject EmptyUserText;
    public InputField RoomCreateInputField;

    private void Start()
    {
        //Screen.SetResolution(800, 1050, false, 60);
        SwitchCanvasSetBool(true);
    }

    public void CreateRoom()
    {
        SwitchPanelSetBool(true);
        SwitchCanvasSetBool(false);
    }

    public void SearchRoom()
    {
        SwitchPanelSetBool(false);
        SwitchCanvasSetBool(false);
    }

    public void NameGet()
    {
        RoomName = RoomCreateInputField.text;
    }

    public static string NameStringPass()
    {
        return RoomName;
    }

     void SwitchCanvasSetBool(bool b)
    {
        StartCanvas.SetActive(b);
        ChatCanvas.SetActive(!b);
    }

    void SwitchPanelSetBool(bool b)
    {
        RoomSetPanel.SetActive(b);
        RoomDisplay.SetActive(!b);
    }

    public void PanelSetBool(bool b)
    {
        RoomSetPanel.SetActive(b);
        RoomDisplay.SetActive(b);
    }

    public void Escape()
    {
        StartCanvas.SetActive(true);
        ChatCanvas.SetActive(false);
        EmptyUserText.SetActive(false);
    }
    

}
