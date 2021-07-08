namespace InTheAction.Services.GetDataFromTMDB.DTOs
{
    public class PersonDTO
    {
        public string Name { get; set; }

        public string CoverImageUrl { get; set; }

        public string Biography { get; set; }

        public string Gender { get; set; }

        public string Birthday { get; set; }

        public string Deathday { get; set; }

        public string Birthplace { get; set; }

        public virtual ICollection<MovieActor> Movies { get; set; }

        public virtual ICollection<ActorComment> Comments { get; set; }
    }
}
