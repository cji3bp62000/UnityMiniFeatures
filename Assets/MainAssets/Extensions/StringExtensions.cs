using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityMiniFeatures.Extensions
{
    public static class StringExtensions
    {
        public static string WithColor(this string source, Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{source}</color>";
        }

        public static string WithColor(this string source, string colorStr)
        {
            return $"<color=#{colorStr}>{source}</color>";
        }
    }
}
