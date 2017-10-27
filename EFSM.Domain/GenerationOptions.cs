namespace EFSM.Domain
{
    public class GenerationOptions
    {
        public string HeaderFilePath { get; set; }

        public string HeaderFileHeader { get; set; }

        public string HeaderFileFooter { get; set; }

        public string CodeFilePath { get; set; }

        public bool IsLittleEndian { get; set; }

        public string DocumentationFolder { get; set; }
    }
}