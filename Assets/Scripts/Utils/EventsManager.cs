using System;
using UnityEngine;
using Utils.JsonObject;

namespace Utils
{
    public class EventsManager : MonoBehaviour
    {
        public event Action<string> OnJsonReceived;

        public void SendJson(string json)
        {
            OnJsonReceived?.Invoke(json);
        }

        public event Action<CubeStorage> OnCubeStorageReceived;

        public void SendCubeStorage(CubeStorage storage)
        {
            OnCubeStorageReceived?.Invoke(storage);
        }
    }
}