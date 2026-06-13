using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Api.Dtos
{
    public record UpdateGameDto(
      [Required] [StringLength(15)]string Name,
        [Required][StringLength(15)]string Genre,
        [Required][Range(1,100)]decimal Price,
       [Required] DateOnly ReleaseDate
    );
  
}