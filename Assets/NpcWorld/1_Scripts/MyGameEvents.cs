using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class MyGameEvents : MonoBehaviour
    {
        public int targetFps = 60;

        private void Awake()
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = targetFps;
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            if(Application.targetFrameRate!=targetFps)
            {
                Application.targetFrameRate = targetFps;
            }
        }


    }
}