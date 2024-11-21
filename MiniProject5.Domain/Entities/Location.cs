using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MiniProject5.Domain.Entities;

[Table("location")]
public partial class Location
{
    [Key]
    public int Id { get; set; }
    [Column("location")]
    [StringLength(100)]
    public string Locations { get; set; } = null!;

    [InverseProperty("LocationNavigation")]
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
