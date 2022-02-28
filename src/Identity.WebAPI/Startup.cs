namespace Identity.WebAPI
{
    public sealed class Startup
    {

        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        private IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services)
        {
            services.AddHttpContextAccessor();
        }
    }
}
