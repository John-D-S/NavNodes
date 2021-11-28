using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavNodesFramework.Utilities
{    
    [RequireComponent(typeof(MeshRenderer))]
    public class SortableObject : MonoBehaviour
    {
        public float value;

        public Material Mat
        {
            set
            {
                meshRenderer.material = value;
            }
        }

        private MeshRenderer meshRenderer;

        public void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void FixedUpdate()
        {
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
        }
    }
}
