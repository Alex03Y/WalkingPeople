using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalkingPeople.Scripts.Utilits
{
    public class SpriteSheetPlayer : MonoBehaviour
    {
        [Serializable]
        public class PlayerState
        {
            public string StateName;
            public Sprite[] Sprites;
            public float AnimationLength;
        }
 
        [SerializeField] private SpriteRenderer _image;
        [SerializeField] private List<PlayerState> _states = new List<PlayerState>();
   
        private PlayerState _activeState;
        private IEnumerator _activeAnimation;

        public void StartAnimation()
        {
            _activeState = _states[0];

            // todo: test case
            StartCoroutine(_activeAnimation = StateAnimation());
        }

        public void StopAnimation()
        {
            if (_activeAnimation != null) StopCoroutine(_activeAnimation);
        }


        public void SetState(string stateName)
        {
            // string comparsion is expensive(todo: rewrite)
            _activeState = _states.Find(x => x.StateName.Equals(stateName));
 
            if (_activeAnimation != null)
            {
                StopCoroutine(_activeAnimation);
                _activeAnimation = null;
            }
            StartCoroutine(_activeAnimation = StateAnimation());
        }

        public void SetState(int count)
        {
            if (count > _states.Count) throw new NotImplementedException("[SpriteSheetPlayer] Requested animation number exceeds their available number");
            
            _activeState = _states[count];
            if (_activeAnimation != null)
            {
                StopCoroutine(_activeAnimation);
                _activeAnimation = null;
            }
            StartCoroutine(_activeAnimation = StateAnimation());
        }
 
        private IEnumerator StateAnimation()
        {
            var framesCount = (float) _activeState.Sprites.Length;
            var delayPerFrames = (_activeState.AnimationLength / framesCount) / 60f; 
 
            while (_activeState != null)
            {
                foreach (var sprite in _activeState.Sprites)
                {
                    _image.sprite = sprite;
                    yield return new WaitForSeconds(delayPerFrames);
                }
            }
 
            _image.sprite = null;
        }
    }
}
