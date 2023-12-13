namespace todolist_api1
{
    public class ApplicationContext : Model.dbContext // контест бд
    {
        public static Model.dbContext Context { get; } = new Model.dbContext();
    }
}
