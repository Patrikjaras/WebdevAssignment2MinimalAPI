
using Microsoft.EntityFrameworkCore;
using MinimalApi2.Data;
using MinimalApi2.Models;

namespace MinimalApi2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DataContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
     

            app.MapGet("/book", async (DataContext context) =>
            {
                return await context.Books.ToListAsync();
                
            });

            app.MapGet("/book/{id}", async (DataContext context, int id) =>
            {
                var foundBook = await context.Books.FindAsync(id);
                if (foundBook is null)
                {
                    return Results.NotFound("Book not found");
                }
                else
                {
                    return Results.Ok(foundBook);

                }


                
            });

            app.MapPost("/book", async (DataContext context, Book book) =>
            {
                context.Books.Add(book);
                await context.SaveChangesAsync();
                return Results.Ok(await context.Books.ToListAsync());
            });

            

            app.MapPut("/book/{id}", async (DataContext context, Book updatedBook, int id) =>
            {
                var book = await context.Books.FindAsync(id);
                if (book is null)
                {
                    return Results.NotFound("Books does not exist");

                }
                else
                {
                    book.Title = updatedBook.Title;
                    book.Author = updatedBook.Author;
                    await context.SaveChangesAsync();   
                    return Results.Ok(await context.Books.ToListAsync());
                    

                }
            });

            app.MapDelete("/book/{id}", async (DataContext context, int id) =>
            {
                var book = await context.Books.FindAsync(id);
                if (book is null)
                {
                    return Results.NotFound("Book does not exist");
                }
                else
                {
                    context.Books.Remove(book);
                    await context.SaveChangesAsync();
                    return Results.Ok(await context.Books.ToListAsync());
                }
            });

            app.Run();
        }
    }
}