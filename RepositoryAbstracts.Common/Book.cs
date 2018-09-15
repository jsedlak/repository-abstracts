namespace RepositoryAbstracts
{
    /// <summary>
    /// The entity we're going to store
    /// </summary>
    public class Book : Entity
    {
        public string Title { get; set; }

        public string Author { get; set; }
    }
}
