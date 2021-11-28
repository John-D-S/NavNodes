using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavNodesFramework.Utilities
{    
    [RequireComponent(typeof(MeshRenderer))]
    public class SortableObject : MonoBehaviour
    {
        /// <summary>
        /// the value by which sortable objects are sorted
        /// </summary>
        public float value;

        /// <summary>
        /// the settable material of this sortableObject's meshrenderer
        /// </summary>
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
            //initialize the meshrenderer of this SortableObject
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void FixedUpdate()
        {
            //update the position of this sortable object to match the value.
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
        }
    }
}
