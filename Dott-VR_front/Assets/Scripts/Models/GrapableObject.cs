using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class GrapableObject
    {
        public int id;
        public string name;

        public Vector3 position;
        public Quaternion rotation;

        public int era_id;

    }
}