using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TeeMessageType
{
    OnPlay = 1,
    OnPause = 2,
    OnContinue = 3,
    OnLose = 4,
    OnWin = 5,
    OnBackHome = 6,
    OnLoadScene = 7,
    OnCoinChange = 8,
    OnChangeValueCardUI = 9,
    OnChangeValueSkillUI = 10,
    OnCutScene = 11,
    OffCutScene = 12,
    OnClaimAds = 13,
    OnDoDailyTask = 14,
    GemChangeEffect = 15,
    OnDungeons = 16,
}
public class Message
{
    public TeeMessageType type;
    public object[] data;
    public Message(TeeMessageType type)
    {
        this.type = type;
    }
    public Message(TeeMessageType type, object[] data)
    {
        this.type = type;
        this.data = data;
    }
}
public interface IMessageHandle
{
    void Handle(Message message);
}
public class MessageManager : MonoBehaviour, ISerializationCallbackReceiver
{
    private static MessageManager instance = null;
    [HideInInspector] public List<TeeMessageType> _keys = new List<TeeMessageType>();
    [HideInInspector] public List<List<IMessageHandle>> _values = new List<List<IMessageHandle>>();
    private Dictionary<TeeMessageType, List<IMessageHandle>> subcribers = new Dictionary<TeeMessageType, List<IMessageHandle>>();
    public static MessageManager Instance { get { return instance; } }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    public void AddSubcriber(TeeMessageType type, IMessageHandle handle)
    {
        if (!subcribers.ContainsKey(type))
            subcribers[type] = new List<IMessageHandle>();
        if (!subcribers[type].Contains(handle))
            subcribers[type].Add(handle);
    }
    public void AddSubcriber(List<TeeMessageType> types, IMessageHandle handle)
    {
        foreach (TeeMessageType type in types)
            AddSubcriber(type, handle);
    }

    public void RemoveSubcriber(TeeMessageType type, IMessageHandle handle)
    {
        if (subcribers.ContainsKey(type))
            if (subcribers[type].Contains(handle))
                subcribers[type].Remove(handle);
    }

    public void RemoveSubcriber(List<TeeMessageType> types, IMessageHandle handle)
    {
        foreach (TeeMessageType type in types)
            RemoveSubcriber(type, handle);
    }

    public void SendMessage(Message message)
    {
        if (subcribers.ContainsKey(message.type))
            for (int i = subcribers[message.type].Count - 1; i > -1; i--)
                subcribers[message.type][i].Handle(message);
    }
    public void SendMessageWithDelay(Message message, float delay)
    {
        StartCoroutine(_DelaySendMessage(message, delay));
    }
    public void SendMessageWithDelayRealTime(Message message, float delay)
    {
        StartCoroutine(_DelayRealTimeSendMessage(message, delay));
    }
    private IEnumerator _DelaySendMessage(Message message, float delay)
    {
        yield return new WaitForSeconds(delay);
        SendMessage(message);
    }
    private IEnumerator _DelayRealTimeSendMessage(Message message, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SendMessage(message);
    }
    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();
        foreach (var element in subcribers)
        {
            _keys.Add(element.Key);
            _values.Add(element.Value);
        }
    }
    public void OnAfterDeserialize()
    {
    }
}

public class MessageSubcriber : IMessageHandle
{
    TeeMessageType[] messageTypes;
    public Action<Message> MessageHandle = delegate { };

    public void Handle(Message message)
    {
        MessageHandle(message);
    }

    public void SubcribeMessages(TeeMessageType[] messageTypes)
    {
        this.messageTypes = messageTypes;
        for (int i = 0; i < messageTypes.Length; i++)
            MessageManager.Instance.AddSubcriber(messageTypes[i], this);
    }

    public void UnsubcribeMessage(TeeMessageType messageType)
    {
        MessageManager.Instance.RemoveSubcriber(messageType, this);
    }

    public void UnsubcribeAllMessages()
    {
        MessageHandle = delegate { };
        for (int i = 0; i < messageTypes.Length; i++)
            MessageManager.Instance.RemoveSubcriber(messageTypes[i], this);
    }
}