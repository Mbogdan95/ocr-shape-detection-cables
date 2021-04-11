namespace Hirschmann
{
    public class User
    {
        public string IdBadge { get; set; }

        public Rank Rank { get; set; }
    }

    public enum Rank
    {
        Administrator,
        Admin,
        Adjuster,
        Operator
    }
}
