using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame : Photon.MonoBehaviour
{
    #region MessageFrame
    //public Sprite _MessageFrame;
    private Image messageFrame;
    private GameObject messageer;
    [SerializeField] private GameObject panel_sender;
    [SerializeField] private GameObject panel_reciever;
    [SerializeField] private GameObject messages;
    [SerializeField] private ScrollRect scrollbar;
    public GameObject image;
    #endregion
    public GameObject Chat;
    Chat chat;
    PhotonView _photonView;
    public InputField inputField;
    public string inputLine;
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        chat =Chat.GetComponent<Chat>();
    }

    public void Send()
    {
        inputLine = inputField.text;
        if (inputLine != "")
        {
            this.photonView.RPC("MessageFrame", PhotonTargets.All, this.inputLine);
        }
    }

    [PunRPC]
    public void MessageFrame(string inputline)
    {
        var messageFrameObject = PhotonNetwork.Instantiate("Image_1", Vector3.zero,Quaternion.identity,0);


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




        //this.messageFrame = messageFrameObject.AddComponent<Image>();
        //messageFrame.sprite = _MessageFrame;
        /*if(Properties.Role() == 0)
        {
            if(photonView.isMine)
            messageFrameObject.transform.SetParent(panel_sender.transform);
        }
        else
        {
            messageFrameObject.transform.SetParent(panel_reciever.transform);
        }*/

        if (_photonView.isMine)
        {
            messageFrameObject.transform.SetParent(panel_sender.transform);
        }
        else
        {
            messageFrameObject.transform.SetParent(panel_reciever.transform);
        }
        //messageFrameObject.transform.SetParent(panel_reciever.transform);
        //イメージの移動、権限による位置変換

        messageer = Instantiate(messages);
        messageer.AddComponent<LayoutElement>();
        messageer.transform.SetParent(messageFrameObject.transform);
        Text msg_text = messageer.GetComponent<Text>();
        msg_text.fontSize = 16;
        msg_text.text = inputLine;
        ContentSizeFitter c = messageer.AddComponent<ContentSizeFitter>();
        c.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        Invoke("Set", 0.05f);
    }

    void Set()
    {
        scrollbar.verticalNormalizedPosition = 0;
    }
}
