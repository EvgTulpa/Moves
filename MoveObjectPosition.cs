using System;
using System.Collections;
using UnityEngine;

namespace DailyChallenge
{
    [ExecuteInEditMode]
    public sealed class MoveObjectPosition : MonoBehaviour
    {
        public event Action OnComplete;
        
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _start;
        [SerializeField] private Vector3 _end;
        [SerializeField] private float _speed;
        [SerializeField] private float _time;
        
        private Coroutine _coroutine;

        public void SetSpeed(float value)
        {
            _speed = value;
        }
        
        public void Move(Vector3 start, Vector3 end, float time)
        {
            _start = start;
            _end = end;
            _time = time;
            _coroutine = StartCoroutine(StartMovement());
        }

        [ContextMenu("TestMove")]
        private void TestMove()
        {
            _coroutine = StartCoroutine(StartMovement());
        }
        
        [ContextMenu("TestStopMove")]
        private void TestStopMove()
        {
            StopMovement();
        }
        
        private IEnumerator StartMovement()
        {
            float elapsedTime = 0;
            while (elapsedTime < _time)
            {
                _target.localPosition = Vector3.Lerp(_start, _end, (elapsedTime / _time));
                elapsedTime += Time.deltaTime*_speed;
                yield return null;
            }

            _target.localPosition = _end;
            OnComplete?.Invoke();
        }

        private void StopMovement()
        {
            if(_coroutine != null)
                StopCoroutine(_coroutine);
        }
    }
}