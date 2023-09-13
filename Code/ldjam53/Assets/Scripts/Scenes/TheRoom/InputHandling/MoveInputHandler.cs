using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;



namespace Assets.Scripts.Scenes.TheRoom.InputHandling
{
    public class MoveInputHandler
    {
        private Vector3 selectedMoveStart;
        private Vector3 selectedMoveEnd;
        private Transform target;
        private Action callBack;


        private float lerpPercentage = 1f;
        private bool isMoving = false;

        public void UpdateHandler()
        {
            if (isMoving)
            {
                lerpPercentage = Mathf.MoveTowards(lerpPercentage, 1f, Time.deltaTime * 5f);
                if (lerpPercentage >= 1)
                {
                    target.position = selectedMoveEnd;
                    if (callBack != default)
                    {
                        callBack.Invoke();
                    }
                    isMoving = false;
                }
                else
                {
                    target.position = Vector3.Lerp(selectedMoveStart, selectedMoveEnd, lerpPercentage);
                }
            }
        }

        public bool IsMoving()
        {
            return isMoving;
        }

        public void Init(Transform target, Action callBack)
        {
            this.callBack = callBack;
            this.target = target;
        }

        public void StartMove(Vector3 dir, Transform target, Action callBack)
        {
            Init(target, callBack);
            StartMove(dir, target.position);
        }

        public void StartMove(Vector3 dir, Vector3 start)
        {
            selectedMoveStart = start;
            selectedMoveEnd = selectedMoveStart + dir;
            lerpPercentage = 0;
            isMoving = true;
        }
    }
}
