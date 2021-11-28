using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using TMPro;

using UnityEngine;

using Random = UnityEngine.Random;

namespace NavNodesFramework.Utilities
{
    public class SortManager : MonoBehaviour
    {
        [SerializeField, Tooltip("The SortableObject script attatched to the prefab that is to be spawned and sorted")] 
        private SortableObject sortableObject;
        [SerializeField, Min(2), Tooltip("The number of sortableObjects to spawn and sort")] 
        private int numberOfObjectsToSpawn = 2;

        [SerializeField, Tooltip("The material of unhighlighted sortableObjects")] 
        private Material normalMat;
        [SerializeField, Tooltip("The material of highlighted sortableObjects")] 
        private Material highlightMat;
        private float SearchValueA;
        [SerializeField, Tooltip("The text display that shows ValueA")] private TextMeshProUGUI searchValueADisplayText;
        private float SearchValueB;
        [SerializeField, Tooltip("The text display that shows ValueB")] private TextMeshProUGUI searchValueBDisplayText;
        private List<SortableObject> spawnedSortableObjects = new List<SortableObject>(); 
        
        void Start()
        {
           RegenerateSortables(); 
        }

        /// <summary>
        /// Deletes current sortables and places them again with new randomized values
        /// </summary>
        public void RegenerateSortables()
        {
            //destroy old sortableObjects and clear the list
            for(int i = 0; i < spawnedSortableObjects.Count; i++)
            {
                Destroy(spawnedSortableObjects[i].gameObject);
            }
            spawnedSortableObjects.Clear();
            
            //instantiate new sortable objects in the world and add their sortableObject component to the list
            for(int i = 0; i < numberOfObjectsToSpawn; i++)
            {
                spawnedSortableObjects.Add(Instantiate(sortableObject.gameObject).GetComponent<SortableObject>());
            }

            //randomize the value of each sortable object
            foreach(SortableObject spawnedSortableObject in spawnedSortableObjects)
            {
                spawnedSortableObject.value = Random.Range(0, 10f);
            }
            
            //line all the objects up in a row according to their position in the spawnedSortableObjects list
            ArrangeByOrder();
            //reset the materials of each object to the unhighlighted material
            ResetMaterials();
        }

        /// <summary>
        /// set the searchValueA to the given float and update the display text to match
        /// </summary>
        public void SetSearchValueA(float _A)
        {
            SearchValueA = _A;
            searchValueADisplayText.text = SearchValueA.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// set the searchValueB to the given float and update the display text to match
        /// </summary>
        public void SetSearchValueB(float _B)
        {
            SearchValueB = _B;
            searchValueBDisplayText.text = SearchValueB.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// change the material of every spawnedSortableObject in spawnedSortableObjects to be the normalMat
        /// </summary>
        private void ResetMaterials()
        {
            foreach(SortableObject spawnedSortableObject in spawnedSortableObjects)
            {
                spawnedSortableObject.Mat = normalMat;
            }
        }

        /// <summary>
        /// reset the material of every spawned Sortable Object and set the material of the
        /// given list of spawned sortable objects to be the highlightedMat
        /// </summary>
        private void HighlightObjects(List<SortableObject> _spawnedObjectsToHighlight)
        {
            ResetMaterials();
            foreach(SortableObject spawnedSortableObject in _spawnedObjectsToHighlight)
            {
                spawnedSortableObject.Mat = highlightMat;
            }
        }

        /// <summary>
        /// Hilights the spawned sortable objects with values between _a and _b
        /// </summary>
        private void SearchBetween(float _a, float _b)
        {
            //if the two values are too close together, give up
            if(Math.Abs(_a - _b) < 0.000001)
                return;
            //make the smaller value the min and the greater value the max;
            float min = _a < _b ? _a : _b;
            float max = _a > _b ? _a : _b;
            //initialize the list of objects to highlight
            List<SortableObject> objectsToHighlight = new List<SortableObject>();
            //go through each object and add it to the list of objects to highlight if it is between min and max
            foreach(SortableObject spawnedSortableObject in spawnedSortableObjects)
            {
                if(spawnedSortableObject.value > min && spawnedSortableObject.value < max)
                {
                    objectsToHighlight.Add(spawnedSortableObject);
                }
            }
            //change the materials to highlight the objectsToHighlight list
            HighlightObjects(objectsToHighlight);
        }

        /// <summary>
        /// searches for the values between SearchValueA and SearchValueB and changes their materials to highlight them
        /// </summary>
        public void Search()
        {
            SearchBetween(SearchValueA, SearchValueB);
        }

        /// <summary>
        /// shuffles the order of the spawnedSortableObjects list
        /// </summary>
        public void Shuffle()
        {
            List<SortableObject> shuffledSpawnedSortableObjects = spawnedSortableObjects.OrderBy(i => Guid.NewGuid()).ToList();
            spawnedSortableObjects = shuffledSpawnedSortableObjects;
            ArrangeByOrder();
        }

        /// <summary>
        /// Sorts the spawenedSortableObjects list in ascending order by each item's value
        /// </summary>
        public void Sort()
        {
            spawnedSortableObjects.Sort(new SortableObjectComparer());
            ArrangeByOrder();
        }
        
        /// <summary>
        /// change the world position of each sortableObject in spawnedSortableObjects to match the order of the list.
        /// </summary>
        private void ArrangeByOrder()
        {
            int noOfObjects = spawnedSortableObjects.Count;
            for(int i = 0; i < noOfObjects; i++)
            {
                Vector3 pos = spawnedSortableObjects[i].transform.position;
                spawnedSortableObjects[i].transform.position = new Vector3(-noOfObjects * 0.5f + i, pos.y, 0);
            }
        }
    }
}
