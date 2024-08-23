using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MiniProject5.Domain.Entities;

[Table("dependents")]
public partial class Dependent
{
    [Key]
    [Column("dependentno")]
    public int Dependentno { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("sex", TypeName = "character varying")]
    public string? Sex { get; set; }

    [Column("dob")]
    public DateOnly Dob { get; set; }

    [Column("relationship")]
    [StringLength(50)]
    public string Relationship { get; set; } = null!;

    [Column("empno")]
    public int? Empno { get; set; }

    [ForeignKey("Empno")]
    [InverseProperty("Dependents")]
    public virtual Employee? EmpnoNavigation { get; set; }
}
