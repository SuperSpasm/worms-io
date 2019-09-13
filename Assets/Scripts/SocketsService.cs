using System;
using System.Collections.Generic;
using DefaultNamespace;
using LitJson;
//using Newtonsoft.Json;
//using Quobject.SocketIoClientDotNet.Client;
//using SocketIO;
using UnityEngine;
using WebSocketSharp;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SocketsService))]
public class SocketsServiceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = target as SocketsService;

        if (GUILayout.Button("Connect to server"))
        {
            SocketsService.Connect();
        }

        if (GUILayout.Button("Send Test Explosion"))
        {
            script.TestExplosion();
        }
    }
}
#endif

public class TestObject
{
    public Vector2 TestVector;
}

public class SocketWrapper
{
//    private Socket socket;
//    private List<string> chatLog = new List<string>();
//    private string url;
//
//    public SocketWrapper(string url)
//    {
//        this.url = url;
//    }
//    
//    void DoOpen()
//    {
//        if (socket != null) return;
//        
//        socket = IO.Socket (socket);
//        socket.On (Socket.EVENT_CONNECT, () => {
//            lock(chatLog) {
//                // Access to Unity UI is not allowed in a background thread, so let's put into a shared variable
//                chatLog.Add("Socket.IO connected.");
//            }
//        });
//        
//        socket.On ("chat", (data) => {
//            string str = data.ToString();
//
//            var chat = JsonConvert.DeserializeObject<ChatData> (str);
//            string strChatLog = "user#" + chat.id + ": " + chat.msg;
//
//            // Access to Unity UI is not allowed in a background thread, so let's put into a shared variable
//            lock(chatLog) {
//                chatLog.Add(strChatLog);
//            }
//        });
//    }
//
//    void DoClose() {
//        if (socket != null) {
//            socket.Disconnect ();
//            socket = null;
//        }
//    }


}

//internal class ChatData
//{
//    public string id;
//    public object msg;
//}

public class SocketsService : MonoBehaviour
{
//    [SerializeField] private SocketIOComponent _staticSocket;
//    private static SocketIOComponent _staticSocket;
    private static SocketWrapper _staticSocket;
    
    public static event Action<WorldStateModel> OnWorldStateUpdate;
    public static event Action<UserStateModel> OnUserStateUpdate;
    public static event Action<ExplosionModel> OnExplosion;

    public const string TestReceiveEvent = "TestReceive";
    public const string TestSendEvent = "TestSend";
    
    private void Start()
    {
//        _staticSocket = _staticSocket;
//        _staticSocket.Connect();
//        _staticSocket.DoOpen();
//        _staticSocket..On(TestReceiveEvent, ev =>
//        {
//            Debug.Log($"Received test data ({ev.name}): \n{ev.data.str}");
//        });
    }
    
    public static void Connect()
    {
        /*
        _staticSocket.On("connection", ev =>
        {
            var json = ev.data.str;
            Debug.Log($"OnConnection! Received: {json}");
            var response = JsonMapper.ToObject<ConnectionResponseModel>(json);
            Debug.Log($"Deserialized Connection response. World: {response.WorldState}. User: {response.UserState}");

            OnWorldStateUpdate?.Invoke(response.WorldState);
            OnUserStateUpdate?.Invoke(response.UserState);
        });
        */
        
        using (var ws = new WebSocket("ws://192.168.31.77:8080/"))
        {
            ws.OnMessage += (sender, e) =>
                Console.WriteLine("Laputa says: " + e.Data);

            ws.Connect();
            ws.Send("BALUS");
            Debug.Log("blaaaaaaaaaa");
            Console.ReadKey(true);
        }
    }

    public static void CreateUser(UserStateModel stateModel)
    {
        throw new NotImplementedException();
    }

    public static void SetHitPlayer(int guid)
    {
        throw new NotImplementedException();
    }

    public static void SetExplosion(ExplosionModel explosion)
    {
        throw new NotImplementedException();
    }

    public static void SetWorldState(WorldStateModel stateModel)
    {
        throw new NotImplementedException();
    }

    [ContextMenu("TestSend")]
    public void TestSend()
    {
        var obj = new TestObject
        {
            TestVector = new Vector2(1.1f, 2.2f)
        };
        
        var json = JsonMapper.ToJson(obj);
        Debug.Log($"Sending test json: {json}");
//        SendData(TestSendEvent, json);
    }

    [ContextMenu("TestReceive")]
    public void TestReceive()
    {
        var str = "{\"TestVector\":{\"x\":1.1000000238418579, \"y\":2.2000000476837158}}";
        var obj = JsonMapper.ToObject<TestObject>(str);
        Debug.Log("Received Test Vector:" + obj.TestVector);
    }

    public void TestExplosion()
    {
     //   _staticSocket.Emit("explosion", new JSONObject("{\"test\": 1}"));
    }


    private static void SendData(string methodName, string json)
    {
       // _staticSocket.SendMessage(methodName, json);
    }
}
