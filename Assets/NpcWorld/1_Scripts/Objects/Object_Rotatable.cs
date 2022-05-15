using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class Object_Rotatable : MonoBehaviour
    {
        #region Platform Types

        [System.Serializable]
        public enum PlatformTypes
        {
            Push,
            AddImpact
        }

        public PlatformTypes platformType;

        #endregion

        #region Rotate Direction

        [System.Serializable]
        public enum RotateDir
        {
            xAxis,
            xAxisReverse,
            yAxis,
            yAxisReverse,
            zAxis,
            zAxisReverse
        }

        public RotateDir rotateDirection;
        private Vector3 _rotateDir;

        #endregion

        [SerializeField] private bool _isPlayerHere;
        [SerializeField] private float _pushForce = 2f;
        [SerializeField] private float _impactForce = 10f;
        [SerializeField] private int _platformSpeed = 100;

        IPushable pushable;

        private void Start()
        {
            _isPlayerHere = false;
        }

        void Update()
        {
            switch (rotateDirection)
            {
                case RotateDir.xAxis:

                    _rotateDir = Vector3.right;

                    break;

                case RotateDir.xAxisReverse:

                    _rotateDir = -Vector3.right;

                    break;

                case RotateDir.yAxis:

                    _rotateDir = Vector3.up;

                    break;

                case RotateDir.yAxisReverse:

                    _rotateDir = -Vector3.up;

                    break;
                case RotateDir.zAxis:

                    _rotateDir = Vector3.forward;

                    break;

                case RotateDir.zAxisReverse:

                    _rotateDir = -Vector3.forward;

                    break;
            }

            gameObject.transform.Rotate(_rotateDir * _platformSpeed * Time.deltaTime);

            if (_isPlayerHere && pushable != null)
            {
                switch (platformType)
                {
                    case PlatformTypes.Push:

                        pushable.PushPlayer(_rotateDir, _pushForce);

                        break;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Opponent"))
            {
                _isPlayerHere = true;
                pushable = other.GetComponent<IPushable>();

                if (pushable != null)
                {
                    switch (platformType)
                    {
                        case PlatformTypes.AddImpact:

                            pushable.AddImpact(-_rotateDir, _impactForce);
                            break;
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Opponent"))
            {
                _isPlayerHere = false;
                pushable = null;
            }
        }

    }
}