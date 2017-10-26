using System;

using SQLite;

namespace Eggsclaim.Models
{
    public class EggsStatus
    {
        [PrimaryKey, AutoIncrement]
        public int SequenceId { get; set; }
        
        public DateTime Timestamp { get; set; }
        
        public bool EggsPresent { get; set; }
        
        public EggsStatus() { }
    }
}
