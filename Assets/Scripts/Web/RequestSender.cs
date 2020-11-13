using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Utils;

namespace Web.Request
{
    public class RequestSender
    {
        public readonly string URL = "https://owncloud.lindenvalley.de/index.php/s/lXIIkptI7tKJRL8/download";
        private EventsManager EventsManager { get; set; }
        private float _timeToCall = 10f;

        public RequestSender(EventsManager eventsManager)
        {
            EventsManager = eventsManager;
        }

        public IEnumerator SendTo(string url)
        {
            while (true)
            {
                var response = UnityWebRequest.Get(url);
                yield return response.SendWebRequest();
                var jsonFromResponse = response.downloadHandler.text;
                EventsManager.SendJson(jsonFromResponse);
                yield return new WaitForSeconds(_timeToCall);
            }
        }
    }
}