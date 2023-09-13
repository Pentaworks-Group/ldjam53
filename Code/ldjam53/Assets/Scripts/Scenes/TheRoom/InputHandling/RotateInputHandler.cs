using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;



namespace Assets.Scripts.Scenes.TheRoom.InputHandling
{
    public class RotateInputHandler
    {

        private Quaternion camRotationTarget;
        private Quaternion camRotationStart;
        private Transform target;
        private Action callBack;


        private float slerpPercentage = 1f;
        private bool isRotating = false;

        private float rotationAngle = 15;

        public void UpdateHandler()
        {
            if (isRotating)
            {
                slerpPercentage = Mathf.MoveTowards(slerpPercentage, 1f, Time.deltaTime * 2f);
                if (slerpPercentage >= 1)
                {
                    target.rotation = camRotationTarget;
                    if (callBack != default)
                    {
                        callBack.Invoke();
                    }
                    isRotating = false;
                }
                else
                {
                    target.rotation = Quaternion.Slerp(camRotationStart, camRotationTarget, slerpPercentage);
                }
            }
        }

        public void SetAngle(float angle)
        {
            rotationAngle = angle;
        }

        public bool IsRotating()
        {
            return isRotating;
        }

        public void Init(Transform target, Action callBack)
        {
            this.callBack = callBack;
            this.target = target;
        }

        public void StartRotation(Vector3 dir, Transform target, Action callBack)
        {
            Init(target, callBack);
            StartRotation(dir, target.rotation);
        }

        public void StartRotation(Vector3 axis, Quaternion start)
        {
            camRotationStart = start;
            camRotationTarget = Quaternion.AngleAxis(rotationAngle, axis) * camRotationStart;
            Debug.Log("start:" + camRotationStart.eulerAngles);
            Debug.Log("target:" + camRotationTarget.eulerAngles);
            slerpPercentage = 0;
            isRotating = true;
        }
    }
}
