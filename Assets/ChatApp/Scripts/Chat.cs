using UnityEngine;
using UnityEngine.UI;

public class Chat : Photon.MonoBehaviour
{
    #region Private Fields
    [SerializeField] private Button Send_Button;
    [SerializeField] private InputField inputField;
    private string inputLine;

    #region MessageFrame
    private GameObject messageer;
    private GameObject messageFrameObject;
    private bool Permissions;
    [SerializeField] private GameObject Talk;
    [SerializeField] private GameObject messages;
    [SerializeField] private ScrollRect scrollbar;
    [SerializeField] private GameObject panel;
    [SerializeField] private Sprite Green;
    [SerializeField] private Sprite White;
    #endregion

     public Text Headtext;

    #endregion


    #region Original Methods

    public void Send()
    {
        inputLine = inputField.text;
        if (inputLine != "")
            photonView.RPC("MessageFrame", PhotonTargets.All, inputLine);
    }

    [PunRPC]
     void MessageFrame(string inputline)
    {

        messageFrameObject = Instantiate(Talk);
        VerticalLayoutGroup messageFrame_verticallayout = messageFrameObject.AddComponent<VerticalLayoutGroup>();
        messageFrame_verticallayout.childForceExpandWidth = false;
        messageFrame_verticallayout.childForceExpandHeight = false;
        messageFrame_verticallayout.childControlWidth = true;
        messageFrame_verticallayout.childControlHeight = true;
        
        //フレームレイアウト
        messageFrame_verticallayout.padding.left = 8;
        messageFrame_verticallayout.padding.right = 8;
        messageFrame_verticallayout.padding.top = 10;
        messageFrame_verticallayout.padding.bottom = 10;

        messageer = Instantiate(messages);
        messageer.AddComponent<LayoutElement>();
        
        Text msg_text = messageer.GetComponent<Text>();
        msg_text.fontSize = 16;
        msg_text.text = inputline;

        messageFrameObject.transform.SetParent(panel.transform);
        messageer.transform.SetParent(messageFrameObject.transform);
        scrollbar.verticalNormalizedPosition = 0;
        inputField.text = ""; 

        if (Permissions == true)
        {
            messageFrameObject.GetComponent<Image>().sprite = Green;
        }
        else
        {
            messageFrameObject.GetComponent<Image>().sprite = White;
        }

        Permissions = false;
    }

    public void Flag()
    {
        Permissions = true;
    }

    #endregion
}
