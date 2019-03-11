using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public class LogListCount : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(ResLoader.SharedLoadedReses.Count);
        }
    }
}