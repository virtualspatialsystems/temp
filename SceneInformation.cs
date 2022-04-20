using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInformation
{
    public static string CurrentScene { get; set; }
    public static Language CurrentLanguage { get; set; } = Language.DE;
    public static bool SubtitleEnabled { get; set; } = true;
}