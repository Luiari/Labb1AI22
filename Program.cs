using System;
using Azure;
using Azure.AI.Language.QuestionAnswering;
using Azure.AI.TextAnalytics;

namespace Labb1AI22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // This example requires environment variables named "LANGUAGE_KEY" and "LANGUAGE_ENDPOINT"
            Uri endpoint = new Uri("https://labb1ai.cognitiveservices.azure.com/");
            AzureKeyCredential credential = new AzureKeyCredential("82caee0d5a654604ab0c12bd9c15b2c9");
            string projectName = "LearnFAQ";
            string deploymentName = "production";

            string cogSvcKey = "34fe9483ad074b0088a714cc621d9545";
            string cogSvcEndpoint = "https://cogservicelabb1ai.cognitiveservices.azure.com/";

            AzureKeyCredential cogCredentials = new AzureKeyCredential(cogSvcKey);
            Uri cogEndpoint = new Uri(cogSvcEndpoint);
            TextAnalyticsClient cogClient = new TextAnalyticsClient(cogEndpoint, cogCredentials);

            Console.WriteLine("Hello, I'm the AIBot. Ask me anything or type 'exit' to quit.");

            while (true)
            {
                Console.Write("You: ");
                string question = Console.ReadLine();
                // Hämta språk
                    DetectedLanguage detectedLanguage = cogClient.DetectLanguage(question);
                    Console.WriteLine($"\nDetected Language: {detectedLanguage.Name}");

                if (question.ToLower() == "exit")
                {
                  
                    break;
                }

                QuestionAnsweringClient client = new QuestionAnsweringClient(endpoint, credential);
                QuestionAnsweringProject project = new QuestionAnsweringProject(projectName, deploymentName);

                Response<AnswersResult> response = client.GetAnswers(question, project);

                foreach (KnowledgeBaseAnswer answer in response.Value.Answers)
                {
                    Console.WriteLine($"Q: {question}");
                    Console.WriteLine($"A: {answer.Answer}");
                }
            }
        }
    }
}
