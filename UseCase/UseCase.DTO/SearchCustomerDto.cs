using System.ComponentModel.DataAnnotations;

namespace UseCase.DTO
{
    public class SearchCorporationDto
    {
        [Required]
        public string TaxNumber { get; set; }
    }
}
