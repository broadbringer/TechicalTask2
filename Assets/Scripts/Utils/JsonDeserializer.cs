using UnityEngine;
using Utils.JsonObject;

namespace Utils
{
    public class JsonDeserializer
    {
        private EventsManager _eventsManager;

        public JsonDeserializer(EventsManager eventsManager)
        {
            _eventsManager = eventsManager;
            _eventsManager.OnJsonReceived += Deserialize;
        }

        private void Deserialize(string json)
        {
            var storage = GetCubeStorage(json);
            _eventsManager.SendCubeStorage(storage);
        }

        private CubeStorage GetCubeStorage(string json)
        {
            return JsonUtility.FromJson<CubeStorage>(json);
        }
    }
}