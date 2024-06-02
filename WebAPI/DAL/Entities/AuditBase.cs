using System.ComponentModel.DataAnnotations;

namespace WebAPI.DAL.Entities
{
    public class AuditBase
    {
        [Key]
        public virtual Guid Id { get; set; } //PK de todas las tablas
        public virtual DateTime CreatedDate { get; set; } //guarda todo registro nuevo 
        public virtual DateTime? ModifiedDate { get; set; } //actualiza todo registro nuevo 
    }
}
