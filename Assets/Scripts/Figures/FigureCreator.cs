using Figures;
using UnityEngine;
using Utils.JsonObject;

namespace Utils
{
    public class FigureCreator : MonoBehaviour
    {
        [SerializeField] private Figure _figurePrefab;
        [SerializeField] private EventsManager _eventsManager;
        [SerializeField] private Game.Application _application;

        private void OnEnable()
        {
            _eventsManager.OnCubeStorageReceived += CreateCubes;
        }

        private void CreateCubes(CubeStorage storage)
        {
            var customizator = new FigureCustomization();
            foreach (var cubeData in storage.Data)
            {
                var tempFigure = Instantiate(_figurePrefab, new Vector3(), Quaternion.identity);
                customizator.SetName(tempFigure, cubeData.Name);
                customizator.SetID(tempFigure, cubeData.Id);
                customizator.SetType(tempFigure, cubeData.Type);
                customizator.SetLinkToEventsManager(tempFigure, _eventsManager);
                customizator.SetToMovePosition(tempFigure, cubeData.Coordinate.Get);
                _application.CreatedFigures.Add(tempFigure);
            }

            _eventsManager.OnCubeStorageReceived -= CreateCubes;
        }
    }

    public class FigureCustomization
    {
        public void SetID(Figure figure, int id)
        {
            figure.Id = id;
        }

        public void SetName(Figure figure, string name)
        {
            figure.Name = name;
            figure.gameObject.name = name;
        }

        public void SetType(Figure figure, int typeId)
        {
            figure.Type = (FigureType) typeId;
        }

        public void SetLinkToEventsManager(Figure figure, EventsManager eventsManager)
        {
            figure.Events = eventsManager;
        }

        public void SetToMovePosition(Figure figure, Vector3 position)
        {
            figure.ToMovePosition = position;
        }
    }
}