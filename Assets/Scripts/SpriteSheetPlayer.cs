using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSheetPlayer : MonoBehaviour
{
    [Serializable]
    public class PlayerState
    {
        public string StateName;
        public Sprite[] Sprites;
    }
 
    [SerializeField] private Image _image;
    [SerializeField] private float _framesPerSecond;
    [SerializeField] private List<PlayerState> _states = new List<PlayerState>();
   
    private PlayerState _activeState;
    private IEnumerator _activeAnimation;
 
    private void Awake()
    {
        _activeState = _states[0];
       
        // todo: test case
        StartCoroutine(_activeAnimation = StateAnimation());
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
 
    private IEnumerator StateAnimation()
    {
        var framesCount = (float) _activeState.Sprites.Length;
        var delayPerFrames = _framesPerSecond / framesCount;
 
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
