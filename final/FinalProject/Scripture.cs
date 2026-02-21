    public class Scripture
    {
        public string Reference { get; private set; }
        public string Content { get; private set; }
        
        public int Weight { get; set; }

        public Scripture(string reference, string content)
        {
            Reference = reference;
            Content = content;
            Weight = 10;
        }

        public override string ToString() => $"[{Reference}]\n\"{Content}\"";
    }
