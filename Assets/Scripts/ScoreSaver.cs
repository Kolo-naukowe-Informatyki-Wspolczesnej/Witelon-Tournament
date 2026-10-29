using System;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

public static class ScoreSaver
{
    private const string FileName = "wynik.csv";
    private const char Separator = ';';

    public static string FilePath
        => Path.Combine(Application.persistentDataPath, FileName);

    public static void Save(string playerName, double timeSeconds, int steps, double score)
    {
        try
        {
            bool newFile = !File.Exists(FilePath);
            var sb = new StringBuilder();

            if (newFile)
            {
                sb.AppendLine(string.Join(Separator.ToString(), "Nazwa szko³y", "Czas", "Iloœæ kroków", "Wynik", "Data"));
            }

            var time = TimeSpan.FromSeconds(timeSeconds);
            string timeText = string.Format("{0:00}:{1:00}:{2:00}", time.Minutes, time.Seconds, time.Milliseconds);

            sb.AppendLine(string.Join(Separator.ToString(),
                 Escape(playerName),
                 timeText,
                 steps.ToString(CultureInfo.InvariantCulture),
                 score.ToString("0.##", CultureInfo.InvariantCulture),
                 DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)));

            File.AppendAllText(FilePath, sb.ToString(), new UTF8Encoding(true));

            Debug.Log("Save score to:" + FilePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving score: " + e);
        }

    }

    private static string Escape(string value)
    {
        if (value.IndexOfAny(new[] { Separator, '"', '\n', '\r' }) >= 0)
        {
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
        return value;
    }

}
