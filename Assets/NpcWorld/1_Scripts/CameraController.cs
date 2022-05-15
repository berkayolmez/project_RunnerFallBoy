using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class CameraController : MonoBehaviour
    {
        public GameObject[] cameraList;
        private int _currentCamera;

        private void Start()
        {
            _currentCamera = 0;
            
            foreach(var a in cameraList)
            {
                a.SetActive(false);
            }

            cameraList[0].SetActive(true);
        }

        public void ChangeCamera(int camNo)
        {
            cameraList[_currentCamera].SetActive(false);
            _currentCamera = camNo;
            cameraList[_currentCamera].SetActive(true);
        }
    }
}