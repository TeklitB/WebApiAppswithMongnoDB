namespace BlogMgtApi.Models
{
    public interface IBlogSettings
    {
        string UsersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
