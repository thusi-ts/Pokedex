Lav connection i appsettings.json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=Pokedex;Trusted_Connection=True"
},

Inject connection og Repository i Startup.cs
services.AddDbContext<pokeDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
services.AddScoped<IPokedexRepository, PokedexRepository>();


Install Entity Framework Core 5.0:
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools

Migrattion and database:
Add-Migration InitialCreate
Update-Database

Kan afprøves det i Postman eller Swagger

Eksample af en pokemon 
{
    "name": "Bulbasaur",
    "translations": {
      "en-gb": "Bulbasaur",
      "ja-jp": "フシギダネ",
      "zh-cn": "妙蛙种子",
      "fr-fr": "Bulbizarre"
    },
    "type": [
      "Grass",
      "Poison"
    ],
    "base": {
      "HP": 45,
      "Attack": 49,
      "Defense": 49,
      "Sp. Attack": 65,
      "Sp. Defense": 65,
      "Speed": 45
    }
  } 

Endpoints:
Hente alle pokemon
/api/Pokemons verb GET
Gemme en pokemon
/api/Pokemons verb PUT
Hente en pokemon
/api/Pokemons/{id} verb GET
Update en pokemon
/api/Pokemons/{id} verb POST
Slette en pokemon
/api/Pokemons/{id} verb DELETE
Slette flere pokemon
/api/Pokemons/{ids} verb DELETE
Slette mange pokemon (int array)
/api/Pokemons/{ids} verb DELETE

