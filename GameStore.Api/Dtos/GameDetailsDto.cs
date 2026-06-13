using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Api.Dtos
{
    public record GameDetailsDto(
                int Id,
        string Name,
        int GenreI,
        decimal Price,
        DateOnly ReleaseDate
    );

}