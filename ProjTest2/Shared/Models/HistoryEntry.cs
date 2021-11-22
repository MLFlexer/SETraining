using System;

namespace ProjTest2.Shared.Models
{
    public class HistoryEntry
    {

        
       
        public HistoryEntry(DateTime date)
        {
            Date = date;
        }
        public int Id {get; set; }
        public DateTime Date { get; set; }
        public Content? Content { get; set; }
        public Learner? Learner { get; set; }
    }
}