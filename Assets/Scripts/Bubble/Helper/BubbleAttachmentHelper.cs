﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Bubble
{
    public class BubbleAttachmentHelper
    {
        private readonly Vector2 _topRightDirection = new Vector2(1, 1).normalized;
        private readonly Vector2 _rightDirection = new Vector2(1, 0).normalized;
        private readonly Vector2 _bottomRightDirection = new Vector2(1, -1).normalized;
        private readonly Vector2 _bottomLeftDirection = new Vector2(-1, -1).normalized;
        private readonly Vector2 _leftDirection = new Vector2(-1, 0).normalized;
        private readonly Vector2 _topLeftDirection = new Vector2(-1, 1).normalized;

        private Dictionary<int, IBubbleNodeController> _viewToControllerMap;

        public void Configure(Dictionary<int, IBubbleNodeController> viewToControllerMap)
        {
            _viewToControllerMap = viewToControllerMap;
        }

        public void PlaceInGraph(Collision2D collision, IBubbleNodeController collisionNodeController,
            IBubbleNodeController strikerNodeController, TweenCallback callback)
        {
            var contactPoint = collision.contacts[0].point;
            contactPoint = collisionNodeController.Position - contactPoint;
            float angle = Mathf.Atan2(contactPoint.x, contactPoint.y) * 180 / Mathf.PI;
            if (angle < 0) angle = 360 + angle;
            var index = (int) (angle / 60);
            var position = collisionNodeController.Position;
            if (index == 0)
            {
                // bottom left of other
                position.x -= 0.5f;
                position.y--;
            }
            else if (index == 1)
            {
                // left of other
                position.x--;
            }
            else if (index == 2)
            {
                // top left of other
                position.x -= 0.5f;
                position.y++;
            }
            else if (index == 3)
            {
                // top right of other
                position.x += 0.5f;
                position.y++;
            }
            else if (index == 4)
            {
                // right of other
                position.x++;
            }
            else if (index == 5)
            {
                // bottom right of other
                position.x += 0.5f;
                position.y--;
            }

            strikerNodeController.SetPosition(position, true, 10, callback);
        }

        public async UniTask MapNeighbors(IBubbleNodeController strikerNodeController)
        {
            await UniTask.SwitchToMainThread();
            if (strikerNodeController.IsRemoved) return;

            var tasks = new List<Task<IBubbleNodeController>>
            {
                MapNeighborAtDirection(strikerNodeController.Position, _topRightDirection),
                MapNeighborAtDirection(strikerNodeController.Position, _rightDirection),
                MapNeighborAtDirection(strikerNodeController.Position, _bottomRightDirection),
                MapNeighborAtDirection(strikerNodeController.Position, _bottomLeftDirection),
                MapNeighborAtDirection(strikerNodeController.Position, _leftDirection),
                MapNeighborAtDirection(strikerNodeController.Position, _topLeftDirection)
            };

            await Task.WhenAll(tasks);

            // 6 neighbors in clockwise direction
            for (var i = 0; i < 6; i++)
            {
                strikerNodeController.SetNeighbor(i, tasks[i].Result);
            }
        }

        public async Task<IBubbleNodeController> MapNeighborAtDirection(Vector2 origin, Vector2 direction)
        {
            var hit = Physics2D.Raycast(origin, direction, 1, Constants.BubbleLayerMask);
            await UniTask.DelayFrame(1);
            Debug.DrawRay(origin, direction, Color.cyan);
            if (hit.collider != null)
            {
                Debug.DrawRay(origin, direction, Color.red);
                return _viewToControllerMap[hit.collider.gameObject.GetInstanceID()];
            }

            return null;
        }
    }
}