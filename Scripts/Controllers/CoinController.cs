using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class CoinController : IDisposable
    {
        private float _animationSpeed = 10;
        private AnimationConfig _config;
        private SpriteAnimatorController _controller;
        private InteractiveObjectView _playerView;
        private List<LevelObjectView> _coinViews;

        public CoinController(InteractiveObjectView playerV, List<LevelObjectView> coinViews)
        {
            _playerView = playerV;
            _coinViews = coinViews;

            _playerView.OnActivate += OnLevelObjectContact;

            _config = Resources.Load<AnimationConfig>("CoinAnimation");
            _controller = new SpriteAnimatorController(_config);
            foreach (LevelObjectView coin in coinViews)
            {
                _controller.StartAnimation(coin._renderer, AnimState.Run, true, _animationSpeed);
            }
        }

        private void OnLevelObjectContact(LevelObjectView contactView)
        {
            if(_coinViews.Contains(contactView))
            {
                _controller.StopAnimation(contactView._renderer);
                GameObject.Destroy(contactView.gameObject);
                _coinViews.Remove(contactView);
            }
        }
        void Update()
        {
            _controller.Update();
        }
        public void Dispose()
        {
            _playerView.OnActivate -= OnLevelObjectContact;
        }
    }
}
