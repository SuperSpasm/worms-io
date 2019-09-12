using System;
using DefaultNamespace;
using LitJson;
using SocketIO;
using UnityEngine;

public class TestObject
{
    public Vector2 TestVector;
}

public class SocketsService : MonoBehaviour
{
    [SerializeField] private SocketIOComponent _socket;
    private static SocketIOComponent _staticSocket;
    
    public static event Action<WorldState> OnWorldStateUpdate;
    public static event Action<UserStateModel> OnUserStateUpdate;
    public static event Action<ExplosionModel> OnExplosion;

    public const string TestReceiveEvent = "TestReceive";
    public const string TestSendEvent = "TestSend";
    
    private void Start()
    {
        _staticSocket = _socket;
        _socket.On(TestReceiveEvent, ev =>
        {
            Debug.Log($"Received test data ({ev.name}): \n{ev.data.str}");
        });
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

    public static void SetWorldState(WorldState state)
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


    private static void SendData(string methodName, string json)
    {
        _staticSocket.SendMessage(methodName, json);
    }
}
