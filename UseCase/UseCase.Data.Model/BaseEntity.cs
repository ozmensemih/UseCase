using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UseCase.Data.Model
{
    public abstract class BaseEntity
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime? DateCreated { get; set; }
        public Guid? UserCreatedId { get; set; }
        [ForeignKey("UserCreatedId")]
        public virtual User UserCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public Guid? UserModifiedId { get; set; }
        [ForeignKey("UserModifiedId")]
        public virtual User UserModified { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] ConcurrencyStamp { get; set; }
    }
}