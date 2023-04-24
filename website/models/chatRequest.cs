namespace website.models
{
    public class chatRequest
    {
        public string? model { get; set; }
        public List<message>? messages { get; set; }
    }
}
