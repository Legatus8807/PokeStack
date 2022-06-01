using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Newtonsoft.Json;
using PokeStack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PokeStack.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PokeStackContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("PokeStackContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var client = new HttpClient())
{
    string url = String.Format("https://pokeapi.co/api/v2/pokemon/charmander");
    var response = client.GetAsync(url).Result;

    string responseAsString = await response.Content.ReadAsStringAsync();

    //PokeGetModel result = JsonConvert.DeserializeObject<PokeGetModel>(responseAsString);
    PokeGetModel result = JsonConvert.DeserializeObject<PokeGetModel>(responseAsString);
    Debug.WriteLine(message: result.types[0].type.name);
    if (result.types.Count == 1)
    {
        Debug.WriteLine("N/A");
    }
    else
    {
        Debug.WriteLine(result.types[1].type.name);
    }
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PokeModels}/{action=Index}/{id?}");

app.Run();
