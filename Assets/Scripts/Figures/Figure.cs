using System.Collections;
using UnityEngine;
using Utils;

namespace Figures
{
    public enum FigureType
    {
        One,
        Two,
        Three,
        Four,
        Five
    }

    public class Figure : MonoBehaviour
    {
        private Transform _transform;
        private float _speed;
        private Coroutine _playingRoutine;

        private Transform CachedTransform
        {
            get
            {
                if (_transform == null)
                {
                    _transform = GetComponent<Transform>();
                }

                return _transform;
            }
        }

        public string Name { get; set; }
        public int Id { get; set; }
        public FigureType Type { get; set; }
        public Vector3 ToMovePosition { get; set; }
        public EventsManager Events { get; set; }


        private float _timer = 5f;
        private bool _isMoveFirstTime;

        private void Update()
        {
            if (!_isMoveFirstTime)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _isMoveFirstTime = true;
                    StartCoroutine(MoveTo(ToMovePosition));
                }
            }
        }

        private IEnumerator MoveTo(Vector3 position)
        {
            while (Vector3.Distance(CachedTransform.position, position) > 0.01f)
            {
                var step = 0.005f;
                CachedTransform.position = Vector3.Lerp(CachedTransform.position, position, _speed * Time.deltaTime);
                _speed += step;
                yield return null;
            }
        }

        public void Move()
        {
            if (_playingRoutine != null)
            {
                StopCoroutine(_playingRoutine);
            }

            _playingRoutine = StartCoroutine(MoveTo(ToMovePosition));
        }
    }
}