using System;


namespace Samples
{
    public class EventViewModel 
    {
        public string Text { get; } = DateTime.Now.ToString("T");
        public string Detail { get; set; }
    }
}