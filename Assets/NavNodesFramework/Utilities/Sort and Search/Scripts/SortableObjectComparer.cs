using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavNodesFramework.Utilities
{
    public class SortableObjectComparer : Comparer<SortableObject>
    {
        public override int Compare(SortableObject _x, SortableObject _y)
        {
            if(_x.value > _y.value)
                return 1;
            if(_x.value < _y.value)
                return -1;
            return 0;
        }
    }
}