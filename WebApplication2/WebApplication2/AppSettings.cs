namespace WebApplication2
{
    public class AppSettings
    {
        public string ApplicationName { get; set; }
        public string WelcomeMessage { get; set; }
        public string ContactEmail { get; set; }
        public FeatureSettings Features { get; set; }
        public List<string> Colors { get; set; }
    }

    public class FeatureSettings
    {
        public bool Feature1 { get; set; }
        public bool Feature2 { get; set; }
    }
}
