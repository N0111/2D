using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class CannonController
    {
        private Transform _MuzzleTransform;
        private Transform _targetTransform;

        private Vector3 _dir;
        private Vector3 _axis;
        private float _angle;

        public CannonController(Transform muzzleT, Transform playerT)
        {
            _MuzzleTransform = muzzleT;
            _targetTransform = playerT;
        }

        public void Update()
        {
            _dir = _targetTransform.position - _MuzzleTransform.position;
            _angle = Vector3.Angle(Vector3.down, _dir);
            _axis = Vector3.Cross(Vector3.down, _dir);

            _MuzzleTransform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }
    }
}