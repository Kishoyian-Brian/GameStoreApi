using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Api.Dtos
{
    public record CreateGmaeDto(
        string Name,
        string Genre,
        int Price,
        DateOnly ReleaseDate
    );
    
}