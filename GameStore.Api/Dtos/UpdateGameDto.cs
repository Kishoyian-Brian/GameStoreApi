using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Api.Dtos
{
    public record UpdateGameDto(
         string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate
    );
  
}