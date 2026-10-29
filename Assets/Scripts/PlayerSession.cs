using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSession
{
    public static string PlayerName { get; private set; } = "";
    public static bool HasName => !string.IsNullOrWhiteSpace(PlayerName);

    public static void SetName(string name) => PlayerName = name.Trim();
    public static void Clear() => PlayerName = "";
}
