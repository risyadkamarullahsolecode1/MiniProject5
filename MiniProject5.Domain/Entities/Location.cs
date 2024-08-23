using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MiniProject5.Domain.Entities;

[Table("location")]
public partial class Location
{
    [Key]
    [Column("location")]
    [StringLength(100)]
    public string Location1 { get; set; } = null!;

    [InverseProperty("LocationNavigation")]
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
