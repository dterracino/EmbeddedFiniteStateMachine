using EFSM.Domain;
using System.IO;

namespace EFSM.Generator
{
    public class EmbeddedCodeGenerator
    {
        private readonly GenerationModelFactory _generationModelFactory = new GenerationModelFactory();
        private readonly HeaderFileGenerator _headerFileGenerator = new HeaderFileGenerator();
        private readonly CodeFileGenerator _codeFileGenerator = new CodeFileGenerator();
        private readonly BinaryGenerator _binaryGenerator = new BinaryGenerator();

        public void Generate(StateMachineProject project, string projectPath)
        {
            // Get the model that we will use to generate the binary / code
            var generationModel = _generationModelFactory.GetGenerationModel(project);

            //Generate the binary part
            var binaryResult = _binaryGenerator.Generate(generationModel);

            string header = _headerFileGenerator.GenerateHeader(generationModel, binaryResult);
            string code = _codeFileGenerator.GenerateCode(generationModel, binaryResult);

            if (!string.IsNullOrWhiteSpace(project.GenerationOptions.HeaderFilePath))
            {
                var fullPath = project.GenerationOptions.HeaderFilePath;
                var path = Path.Combine(projectPath, Path.GetDirectoryName(fullPath));
                Directory.CreateDirectory(path);
                path = Path.Combine(path, Path.GetFileName(fullPath));
                File.WriteAllText(path, header);
            }

            if (!string.IsNullOrWhiteSpace(project.GenerationOptions.CodeFilePath))
            {
                var fullPath = project.GenerationOptions.CodeFilePath;
                var path = Path.Combine(projectPath, Path.GetDirectoryName(fullPath));
                Directory.CreateDirectory(path);
                path = Path.Combine(path, Path.GetFileName(fullPath));
                File.WriteAllText(path, code);
            }
        }
    }
}