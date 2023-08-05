using Bubble;
using UnityEngine;

namespace Signals
{
    public class BubbleCollisionSignal
    {
        public IBubbleNodeController StrikerNode { get; set; }
        public Collision2D CollisionObject { get; set; }
    }
}