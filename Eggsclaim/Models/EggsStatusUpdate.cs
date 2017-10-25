using System;

namespace Eggsclaim.Models
{
    public class EggsStatusUpdate
    {
        public DateTime Timestamp { get; }
        public bool EggsPresent { get; }
        
        public EggsStatusUpdate(DateTime timestamp, bool eggsPresent)
        {
            Timestamp = timestamp;
            EggsPresent = eggsPresent;
        }
    }
}
