using System.Collections.Generic;
using UnityEngine;

namespace UnityMessageCenter.Basic.Network
{
    [System.Serializable]
    public class EventPayload
    {
        private readonly Dictionary<string, object> _data;

        public EventPayload(Dictionary<string, object> data)
        {
            _data = data ?? new Dictionary<string, object>();
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            if (_data.TryGetValue(key, out var value) && int.TryParse(value?.ToString(), out var result))
            {
                return result;
            }

            return defaultValue;
        }

        public string GetString(string key, string defaultValue = "")
        {
            if (_data.TryGetValue(key, out var value))
            {
                return value?.ToString() ?? defaultValue;
            }

            return defaultValue;
        }

        public bool GetBool(string key, bool defaultValue = false)
        {
            if (_data.TryGetValue(key, out var value) && bool.TryParse(value?.ToString(), out var result))
            {
                return result;
            }

            return defaultValue;
        }

        public void Set(string key, object value)
        {
            _data[key] = value;
        }

        public bool HasKey(string key)
        {
            return _data.ContainsKey(key);
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(_data);
        }
        public static EventPayload FromJson(string jsonString)
        {
            var data = JsonUtility.FromJson<Dictionary<string, object>>(jsonString);
            return new EventPayload(data);
        }
    }

}