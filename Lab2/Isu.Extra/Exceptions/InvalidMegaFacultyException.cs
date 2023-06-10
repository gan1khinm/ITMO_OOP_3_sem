namespace Isu.Exceptions
{
    public class InvalidMegaFacultyException : Exception
    {
        public InvalidMegaFacultyException(string megaFaculty)
        {
            MegaFaculty = megaFaculty;
        }

        public string MegaFaculty { get; set; }
    }
}