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

            if ((project.GenerationOptions.DebugMode == DebugMode.None) || (project.GenerationOptions.DebugMode == DebugMode.Embedded))
            {
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
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(projectPath);

                /*Clean up the directory by deleting all files not part of the platform (i.e. delete the generated files).*/
                foreach (var file in directoryInfo.GetFiles())
                {
                    if ((file.Name != "efsm_core.h")
                        && (file.Name != "efsm_core.c")
                        && (file.Name != "efsm_binary_protocol.h"))
                    {
                        file.Delete();
                    }                    
                }
                
                var fileName = Path.GetFileName(project.GenerationOptions.HeaderFilePath);
                var path = Path.Combine(projectPath, fileName);

                File.WriteAllText(path, header);

                fileName = Path.GetFileName(project.GenerationOptions.CodeFilePath);
                path = Path.Combine(projectPath, fileName);

                File.WriteAllText(path, code);                
            }
        }
    }
}