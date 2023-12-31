﻿using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Bubble
{
    public class ColorMergeHelper
    {
        private bool IsValidNode(IBubbleNodeController n, IBubbleNodeController source, HashSet<IBubbleNodeController> visitedNodes)
        {
            return n != null && n.BubbleType == source.BubbleType && visitedNodes.Contains(n) == false;
        }

        public async UniTask<IEnumerable<IBubbleNodeController>> MergeNodes(IBubbleNodeController source)
        {
            var count = 0;
            IEnumerable<IBubbleNodeController> visitedNodes = BubbleUtility.Dfs(source, IsValidNode, ref count);
            
            if (count > 2)
            {
                await HideNodes(visitedNodes);
                return visitedNodes;
            }
            else
            {
                return null;
            }
            
        }

        public async UniTask HideNodes( IEnumerable<IBubbleNodeController> elements)
        {
            foreach (var bubbleNodeController in elements)
            {
                await bubbleNodeController.ExplodeNodeAsync();
            }
        }
    }
}