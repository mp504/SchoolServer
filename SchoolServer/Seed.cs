using SchoolServer.Data;

namespace SchoolServer
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }


        public void SeedDataContext()
        {

        }
    }
}
