using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Models;

[Index(nameof(Codi), IsUnique = true)]
public class Xip
{
    public int XipID { get; set; }

    [Required]
    [StringLength(15, MinimumLength = 15)]
    public string Codi { get; set; } = null!;

    [Required]
    public DateTime DataImplantacio { get; set; }

    public virtual Animal? Animal { get; set; }

    public Xip()
    {
        DataImplantacio = DateTime.Today;
    }

    public Xip(int xipId, string codi, DateTime dataImplantacio)
    {
        XipID = xipId;
        Codi = codi;
        DataImplantacio = dataImplantacio;
    }
}
