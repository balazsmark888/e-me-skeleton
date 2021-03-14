namespace e_me.Model.DBContext
{
    public interface IApplicationDbContextFactory
    {
        ApplicationDbContext Create();
    }
}
