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

            string header = _headerFileGenerator.GenerateHeader(generationModel);
            string code = _codeFileGenerator.GenerateCode(generationModel, binaryResult);

            if (!string.IsNullOrWhiteSpace(project.GenerationOptions.HeaderFilePath))
            {
                var path = Path.Combine(projectPath, project.GenerationOptions.HeaderFilePath);
                File.WriteAllText(path, header);
            }

            if (!string.IsNullOrWhiteSpace(project.GenerationOptions.CodeFilePath))
            {
                var path = Path.Combine(projectPath, project.GenerationOptions.CodeFilePath);
                File.WriteAllText(path, code);
            }
        }


    }
}