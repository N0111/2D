using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {

        [SerializeField] private LevelObjectView _playerView;
        private PlayerController _playerController;
        private CameraController _cameraController;

        private void Awake()
        {
            _playerController = new PlayerController(_playerView);
            _cameraController = new CameraController(_playerView, Camera.main.transform);
        }

        void Update()
        {
            _playerController.Update();
            _cameraController.Update();
        }
    }
}