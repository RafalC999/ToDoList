namespace TodoList.Api
{
    public class Program
    {

        //TODO: dodaæ generowanie tokenu po podaniu user password, rejestracja logowanie
        // dodaæ do klasy BaseEntity ModifiedOn ModifiedBy, CreatedOn, CreatedBy migracja, update bazy
        // dodaæ "AuditInterceptor" https://www.youtube.com/watch?v=mAlO3OuoQvo



        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpContextAccessor();


            builder.Services.AddSwaggerGen(options =>
            {

            });

            builder.Services.AddTodoListApiServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            //app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();


            app.MapControllers();

            app.Run();
        }
    }
}