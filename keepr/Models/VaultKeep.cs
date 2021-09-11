using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
  public class VaultKeep
  {
      public int Id { get; set; }
      [Required]
      public string CreatorId { get; set; }
      [Required]
      public int VaultId { get; set; }
      [Required]
      public int KeepId { get; set; }
  }
}