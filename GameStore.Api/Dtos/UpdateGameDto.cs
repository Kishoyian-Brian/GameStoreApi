using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Api.Dtos
{
    public record UpdateGameDto(
      [Required] [StringLength(15)]string Name,
         [Range(1,50)]int GenreId,
        [Required][Range(1,100)]decimal Price,
       [Required] DateOnly ReleaseDate
    );
  
}