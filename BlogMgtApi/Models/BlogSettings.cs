namespace BlogMgtApi.Models
{
    public class BlogSettings : IBlogSettings
    {
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
