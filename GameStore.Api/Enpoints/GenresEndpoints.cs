using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Enpoints
{
    public static class GenresEndpoints
    {
        public static void MapGenresEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("/genres");

            //Get/genres
            group.MapGet("/", async (GameStoreContext dbContext) =>
               await dbContext.Genres
                            .AsNoTracking()
                            .Select(genre => new GenreDto(
                                genre.Id,
                                genre.Name
                            )).ToListAsync()
            );
        }
    }
}