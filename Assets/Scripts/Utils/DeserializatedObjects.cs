using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.JsonObject
{
    [Serializable]
    public class Coordinate
    {
        [SerializeField] private int x;
        [SerializeField] private int y;
        [SerializeField] private int z;

        public Vector3 Get => new Vector3(x, y, z);
    }

    [Serializable]
    public class CubeData
    {
        [SerializeField] private string modifiedData;
        [SerializeField] private Coordinate coordinate;
        [SerializeField] private string name;
        [SerializeField] private int id;
        [SerializeField] private int type;

        public string ModifiedData => modifiedData;
        public Coordinate Coordinate => coordinate;
        public string Name => name;
        public int Id => id;
        public int Type => type;
    }

    [Serializable]
    public class CubeStorage
    {
        [SerializeField] private string name;
        [SerializeField] private int id;
        [SerializeField] private string modifiedData;
        [SerializeField] private List<CubeData> data;

        public string Name => name;
        public int Id => id;
        public string ModifiedData => modifiedData;
        public List<CubeData> Data => data;
    }
}