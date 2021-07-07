namespace InTheAction.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using InTheAction.Data.Common.Models;

    using static InTheAction.Data.Common.DataValidation.Character;

    public class MovieActor : BaseDeletableModel<int>
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int ActorId { get; set; }

        public virtual Actor Actor { get; set; }

        [Required]
        [MaxLength(CharacterNameMaxLength)]
        public string CharacterName { get; set; }
    }
}
