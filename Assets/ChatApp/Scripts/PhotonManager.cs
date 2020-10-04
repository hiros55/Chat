using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour
{
    [SerializeField] private GameObject ChatScript;
    [SerializeField] private GameObject PropertiesScript;
    
    private GameObject[] RoomPanels = new GameObject[256];
    private int i;
    private bool RoomUserCount  = false;
    Properties properties;
    Chat chat;

    private void Start()
    {
        properties = PropertiesScript.GetComponent<Properties>();
        chat = ChatScript.GetComponent<Chat>();
        properties.EmptyUserText.SetActive(false);
    }


    public void InLobby()
    { 
        PhotonNetwork.ConnectUsingSettings(null);
        PhotonNetwork.JoinLobby();
    }

    public void OutLobby()
    {
        PhotonNetwork.Disconnect();
    }

    private void SetUserName()
    {
        foreach (var p in PhotonNetwork.playerList)
        {
            if (PlayerPrefs.GetString("name") != p.name)
            {
                chat.Headtext.text = p.name;
            }
        }
    }

     public void OnPhotonPlayerConnected()
     {
        SetUserName();
        properties.EmptyUserText.SetActive(false);
    }


    /*void OnDisconnectedFromPhoton()
    {
        Debug.Log("切断");
    }

    void OnJoinedLobby()
    {
        Debug.Log("ロビー入室");
    }

    void OnJoinedRoom()
    {
        Debug.Log("ルーム入室");
    }

    void OnPhotonJoinFailed()
    {
        Debug.Log("ルーム入室失敗");
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("現在作成されている部屋はありません\n部屋を作ってください。");
    }*/


    public void OnReceivedRoomListUpdate()
    {
        i = 0;
        foreach( RoomInfo roomInfo in PhotonNetwork.GetRoomList())
        {
            if (roomInfo.PlayerCount < 2　&& roomInfo.PlayerCount > 0)
            {
                if (RoomPanels[i] == null)
                {
                    RoomPanels[i] = Instantiate(properties.OnlyCopyPanel);

                    GameObject inRoomButton = RoomPanels[i].transform.FindChild("InRoomButton").gameObject;
                    GameObject roomNameText = RoomPanels[i].transform.FindChild("RoomNameText").gameObject;
                    GameObject playerCountText = RoomPanels[i].transform.FindChild("PlayerCountText").gameObject;

                    RoomPanels[i].transform.SetParent(properties.RoomPropertyPanel.transform);

                    RoomPanels[i].transform.localPosition = new Vector2(70, -15 - i * 120);


                    roomNameText.GetComponent<Text>().text = "部屋名：" + roomInfo.Name.ToString();
                    playerCountText.GetComponent<Text>().text ="部屋人数：" + roomInfo.PlayerCount.ToString();


                    Button button  = inRoomButton.GetComponent<Button>();
                    this.AddEvent(button,roomInfo.name,RoomPanels[i]);//roomInfoの内容を渡す
                }
            }
            else
            {
                Destroy(RoomPanels[i]);
            }
            i++;
        }
        Invoke("ChangeRoomPanels",1);
        SearchRoomText();
    }

    private void AddEvent(Button button,string roomName,GameObject roomPanels)
    {
        button.onClick.AddListener(() => { InRoomPanel(roomName,roomPanels); });
    }

    private void InRoomPanel( string roomName,GameObject roomPanels)
    {
        Destroy(roomPanels);
        properties.PanelSetBool(false);
        PhotonNetwork.JoinRoom(roomName);
        properties.EmptyUserText.SetActive(false);
        PhotonNetwork.playerName = PlayerPrefs.GetString("name");
        Invoke("SetUserName", 1);
    }

    private void ChangeRoomPanels()
    {
        if (PhotonNetwork.GetRoomList().Length <= 0)
            return;

        for (int i = 0; i < PhotonNetwork.GetRoomList().Length; i++)
        {
            if (RoomPanels[i] == null)
            {
                for (int l = i; l < PhotonNetwork.GetRoomList().Length; l++)
                {
                    RoomPanels[l] = RoomPanels[l + 1];
                }
            }
            RoomPanels[i].transform.localPosition = new Vector2(70, -15 - i * 120);
        }
    }


    public void SearchRoomText()
    {
        if(PhotonNetwork.GetRoomList().Length <= 0)
        {
            properties.EmptyRoomText.SetActive(true);
        }
        else
        {
            properties.EmptyRoomText.SetActive(false);
        }
    }



    public void Createroom()
    {
        string roomName = Properties.NameStringPass();

        if(roomName == null || roomName == "")
        {
            properties.RoomCreateInputField.placeholder.GetComponent<Text>().text = "既に存在、もしくは未入力です";
            return;
        }

        PhotonNetwork.autoCleanUpPlayerObjects = false;

        if(PlayerPrefs.GetString("name") == null)
        {
            string userName = PhotonNetwork.player.ID.ToString();
        }
        else
        {
            string userName = PlayerPrefs.GetString("name");
        }
        string userId = PhotonNetwork.player.ID.ToString();

        PhotonNetwork.playerName = PlayerPrefs.GetString("name");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.customRoomPropertiesForLobby = new string[] { "userName", "userId" };
        roomOptions.maxPlayers = 2; 
        roomOptions.isOpen = true; 
        roomOptions.isVisible = true;
        PhotonNetwork.CreateRoom(roomName,roomOptions,null);

        properties.RoomSetPanel.SetActive(false);
        //他ユーザー入室までテキスト表示
        properties.EmptyUserText.SetActive(true);
    }
}