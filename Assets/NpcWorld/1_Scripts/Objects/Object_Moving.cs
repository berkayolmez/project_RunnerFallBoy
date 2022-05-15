using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace npcWorld
{
    public class Object_Moving : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _startingPoint;
        [SerializeField] private Transform[] _points;
        private int i;

        private void Start()
        {
            transform.position = _points[_startingPoint].position;
        }

        private void Update()
        {
            if(Vector3.Distance(transform.position,_points[i].position)<0.02f)
            {
                i++;
                if(i == _points.Length)
                {
                    i = 0;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, _points[i].position, _speed * Time.deltaTime);
        }
    }
}