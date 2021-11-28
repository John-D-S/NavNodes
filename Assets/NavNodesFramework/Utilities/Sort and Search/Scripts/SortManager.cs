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
        [SerializeField] private SortableObject sortableObject;
        [SerializeField, Min(2)] private int numberOfObjectsToSpawn = 2;

        [SerializeField] private Material normalMat;
        [SerializeField] private Material highlightMat;
        private float SearchValueA;
        [SerializeField] private TextMeshProUGUI searchValueADisplayText;
        private float SearchValueB;
        [SerializeField] private TextMeshProUGUI searchValueBDisplayText;
        private List<SortableObject> spawnedSortableObjects = new List<SortableObject>(); 
        
        void Start()
        {
           RegenerateSortables(); 
        }

        public void RegenerateSortables()
        {
            for(int i = 0; i < spawnedSortableObjects.Count; i++)
            {
                Destroy(spawnedSortableObjects[i].gameObject);
            }
            spawnedSortableObjects.Clear();
            
            for(int i = 0; i < numberOfObjectsToSpawn; i++)
            {
                spawnedSortableObjects.Add(Instantiate(sortableObject.gameObject).GetComponent<SortableObject>());
            }

            foreach(SortableObject spawnedSortableObject in spawnedSortableObjects)
            {
                spawnedSortableObject.value = Random.Range(0, 10f);
            }
            
            ArrangeByOrder();
            ResetMaterials();
        }

        public void SetSearchValueA(float _A)
        {
            SearchValueA = _A;
            searchValueADisplayText.text = SearchValueA.ToString(CultureInfo.InvariantCulture);
        }

        public void SetSearchValueB(float _B)
        {
            SearchValueB = _B;
            searchValueBDisplayText.text = SearchValueB.ToString(CultureInfo.InvariantCulture);
        }

        private void ResetMaterials()
        {
            foreach(SortableObject spawnedSortableObject in spawnedSortableObjects)
            {
                spawnedSortableObject.Mat = normalMat;
            }
        }

        private void HighlightObjects(List<SortableObject> _spawnedObjectsToHighlight)
        {
            ResetMaterials();
            foreach(SortableObject spawnedSortableObject in _spawnedObjectsToHighlight)
            {
                spawnedSortableObject.Mat = highlightMat;
            }
        }

        private void SearchBetween(float _a, float _b)
        {
            if(Math.Abs(_a - _b) < 0.000001)
                return;
            float min = _a < _b ? _a : _b;
            float max = _a > _b ? _a : _b;
            List<SortableObject> objectsToHighlight = new List<SortableObject>();
            foreach(SortableObject spawnedSortableObject in spawnedSortableObjects)
            {
                if(spawnedSortableObject.value > min && spawnedSortableObject.value < max)
                {
                    objectsToHighlight.Add(spawnedSortableObject);
                }
            }
            HighlightObjects(objectsToHighlight);
        }

        public void Search()
        {
            SearchBetween(SearchValueA, SearchValueB);
        }

        public void Shuffle()
        {
            List<SortableObject> shuffledSpawnedSortableObjects = spawnedSortableObjects.OrderBy(i => Guid.NewGuid()).ToList();
            spawnedSortableObjects = shuffledSpawnedSortableObjects;
            ArrangeByOrder();
        }

        public void Sort()
        {
            spawnedSortableObjects.Sort(new SortableObjectComparer());
            ArrangeByOrder();
        }
        
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
