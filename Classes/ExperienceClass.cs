namespace ZackV2.Classes;

public class ExperienceClass
{
    public class ExpModel
    {
        public string Role { get; set; }
        public string Company { get; set; }
        public string Dates { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public List<string> Technologies { get; set; }
        public List<Link> Links { get; set; }
        public List<string> ExampleImages { get; set; }
    }

    public class Link
    {
        public string Text { get; set; }
        public string Url { get; set; }
    }
}