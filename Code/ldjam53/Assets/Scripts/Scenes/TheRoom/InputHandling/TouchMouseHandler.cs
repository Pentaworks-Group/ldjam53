using System;

using Assets.Scripts.Base;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Scenes.TheRoom.InputHandling
{
    public class TouchMouseHandler
    {


        private (Vector2, Vector2) prevPinch = default;
        private Vector2 clickDown;


        public Action<Vector2> leftClick;
        public Action<Vector2> rightClick;
        public Action<Vector2> zoomClick;

        private bool isActionActive = false;
        private InputAction action;

        private enum InputAction
        {
            LeftClick, RightClick, MouseWheel, SingleTouch, MultiTouch
        }

        public void Init(Action<Vector2> leftClick,   Action<Vector2> rightClick,  Action<Vector2> zoomClick)
        {
            this.leftClick = leftClick;
            this.rightClick = rightClick;
            this.zoomClick = zoomClick;
        }


        public void UpdateTouchHandler()
        {
            if (isActionActive)
            {
                EndHandling();
            }
            else
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    StartHandling();
                }
            }
            
        }

        private void StartHandling()
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickDown = Input.mousePosition;
                action = InputAction.LeftClick;
                isActionActive = true;
            }
            else if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                prevPinch = (touch1.position, touch2.position);
                action = InputAction.MultiTouch;
                isActionActive = true;
            }
            else if (Input.touchCount == 1)
            {
                Touch touch1 = Input.GetTouch(0);
                clickDown = touch1.position;
                action = InputAction.SingleTouch;
                isActionActive = true;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                clickDown = Input.mousePosition;
                action = InputAction.RightClick;
                isActionActive = true;
            } else if (Input.mouseScrollDelta.y != 0)
            {
                zoomClick.Invoke(new Vector2(0, Math.Sign(Input.mouseScrollDelta.y)));
            }
        }

        private void EndHandling()
        {
            if (action == InputAction.LeftClick)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    isActionActive = false;

                } else
                {
                    Vector2 click = Input.mousePosition;
                    leftClick.Invoke(click - clickDown);
                    clickDown = Input.mousePosition;
                }                    
            }
            else if (action == InputAction.RightClick)
            {
                if (Input.GetMouseButtonUp(1))
                {
                    isActionActive = false;
                } else
                {
                    Vector2 click = Input.mousePosition;
                    rightClick.Invoke(click - clickDown);
                    clickDown = Input.mousePosition;
                }
            } else if (action == InputAction.SingleTouch)
            {
                if (Input.touchCount != 1)
                {
                    isActionActive = false;
                } else
                {
                    leftClick.Invoke(Input.touches[0].position - clickDown);
                    clickDown = Input.mousePosition;
                }
            }
            else if (action == InputAction.MultiTouch)
            {
                if (Input.touchCount != 2)
                {
                    isActionActive = false;
                }
                else
                {
                    Vector2 touch1 = Input.GetTouch(0).position;
                    Vector2 touch2 = Input.GetTouch(1).position;
                    if (touch1 != prevPinch.Item1 && touch2 != prevPinch.Item2)
                    {
                        var dist2 = touch1 - touch2;
                        var dist1 = prevPinch.Item1 - prevPinch.Item2;
                        if (Math.Abs(dist1.magnitude - dist2.magnitude) > 5)
                        {
                            rightClick.Invoke(dist2 - dist1);
                            prevPinch = (touch1, touch2);
                        }
                    }
                }
            } else if (action == InputAction.MouseWheel)
            {
                if (Input.mouseScrollDelta.y == 0)
                {
                    isActionActive = false;
                } else
                {
                    zoomClick.Invoke(new Vector2(0, Math.Sign(Input.mouseScrollDelta.y)));
                }
            }
        }



    }

}
