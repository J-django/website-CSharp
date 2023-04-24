namespace website.models
{
    public class chatResponse
    {
        public string? id { get; set; }
        public string? @object { get; set; }
        public int? created { get; set; }
        public List<choices>? choices { get; set; }
        public usage? usage { get; set; }
    }
}
