using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using Figures;
using Utils;
using Utils.JsonObject;
using Web.Request;

namespace Game
{
    public class Application : MonoBehaviour
    {
        [SerializeField] private EventsManager _eventsManager;

        private RequestSender requestSender;
        private JsonDeserializer _jsonDeserializer;
        private CubeStorage _storage;
        private DateComparer _comparer;

        public List<Figure> CreatedFigures { get; private set; }

        private void Start()
        {
            _eventsManager.OnCubeStorageReceived += OnNewStorageReceived;
            CreatedFigures = new List<Figure>();
            SetDependencies();
            StartCoroutine(requestSender.SendTo(requestSender.URL));
        }

        private void SetDependencies()
        {
            requestSender = new RequestSender(_eventsManager);
            _jsonDeserializer = new JsonDeserializer(_eventsManager);
            _comparer = new DateComparer();
        }

        private float _updateTime = 10f;
        private float _currentTime = 0f;

        private void OnNewStorageReceived(CubeStorage storage)
        {
            if (_storage == null)
            {
                _storage = storage;
                return;
            }

            var currentTime = DateTime.ParseExact(_storage.ModifiedData, "MM-dd-yyyy", CultureInfo.InvariantCulture);
            var toCompareTime = DateTime.ParseExact(storage.ModifiedData, "MM-dd-yyyy", CultureInfo.InvariantCulture);
            if (!_comparer.IsSameDates(currentTime, toCompareTime))
            {
                for (var i = 0; i < storage.Data.Count; i++)
                {
                    var oldPosition = CreatedFigures[i].ToMovePosition;
                    var newPosition = storage.Data[i].Coordinate.Get;
                    if (oldPosition.Equals(newPosition))
                    {
                        continue;
                    }

                    CreatedFigures[i].ToMovePosition = newPosition;
                    CreatedFigures[i].Move();
                }
            }

            _storage = storage;
        }
    }

    public class DateComparer
    {
        public bool IsSameDates(DateTime firstDate, DateTime secondDate) => GetCompareValue(firstDate, secondDate) == 0;

        private int GetCompareValue(DateTime firstDate, DateTime secondDate)
        {
            return firstDate.CompareTo(secondDate);
        }
    }
}