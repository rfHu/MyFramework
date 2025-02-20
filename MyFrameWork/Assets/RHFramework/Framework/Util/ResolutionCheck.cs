﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RHFramework
{
    public partial class ResolutionCheck
    {
        public static float GetAspectRatio()
        {
            var isLandspace = Screen.width > Screen.height;
            return isLandspace ? (float)Screen.width / Screen.height : (float)Screen.height / Screen.width;
        }

        public static bool InAspectRange(float dstAspectRatio)
        {
            var aspect = GetAspectRatio();
            return aspect > (dstAspectRatio - 0.05) && aspect < (dstAspectRatio + 0.05);
        }

        public static bool IsPadResolution()
        {
            return InAspectRange(4.0f / 3);
        }

        public static bool IsPhoneResolution()
        {
            return InAspectRange(16.0f / 9);
        }

        public static bool IsPhone15Resolution()
        {
            return InAspectRange(3.0f / 2);
        }

        public static bool IsPhoneXResolution()
        {
            return InAspectRange(2436.0f / 1125);
        }
    }
}