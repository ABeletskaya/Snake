using System;
using System.IO;

namespace Snake
{
    public class Records
    {
        public void ExistRecordFile(out string message)
        {
            if (!File.Exists(@"../../ Record.txt"))
            {
                message = "0 - Play the game to note your record";
                File.WriteAllText(@"../../ Record.txt", message);
            }
            else
                message = File.ReadAllText(@"../../ Record.txt");
        }

        public void UpdateRecord(ref string message, int score)
        {
            int oldRecord = int.Parse(message.Split(' ')[0]);
            if (oldRecord < score)
            {
                message = $"{score} - Max record, {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";
                File.WriteAllText(@"../../ Record.txt", message);
            }
        }
    }
}
