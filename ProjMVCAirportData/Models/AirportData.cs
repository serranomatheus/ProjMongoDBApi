using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProjMVCAirportData.Models
{
    [Table("Airport")]
    public class AirportData
    {
        #region Properties
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Code { get; set; }
        public string Continent { get; set; }
        #endregion
    }
}
