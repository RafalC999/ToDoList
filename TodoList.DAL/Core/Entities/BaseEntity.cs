using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TodoList.DAL.Entities;

namespace TodoList.DAL.Core.Entities
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public DateTime CreateOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        [ForeignKey("CreatedBy")]
        public string CreatedById { get; set; }

        [ForeignKey("ModifiedBy")]
        public string ModifiedById { get; set; }

        [JsonIgnore]
        public ApplicationUser CreatedBy { get; set; }
        [JsonIgnore]
        public ApplicationUser ModifiedBy { get; set; }
    }
}
