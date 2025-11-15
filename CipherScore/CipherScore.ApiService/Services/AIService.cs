using Azure;
using Azure.AI.OpenAI;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace CipherScore.ApiService.Services
{
    public class AIService
    {
        private readonly OpenAIClient _client;
        private readonly string _deployment;

        public AIService(IConfiguration config)
        {
            _client = new OpenAIClient(
                new Uri(config["AIURI"]),
                new AzureKeyCredential(config["AIsecret"])
            );
            _deployment = "gpt-4o-mini";
        }

        public async Task<string> EvaluatePasswordAsync(string password)
        {
            // Explicity state how long it will take to brute force the password
            // Do not recommend other passwords if the password is strong enough
            var prompt = $"""
                Evaluate this password: '{password}'. Is it secure? How long would it take to brute force? What would you suggest?
                Break down your analysis into clear, concise points.
                Your response should breakdown into the following categories: 
                Analysis:
                1. Unique characters i.e. mix of upper-case, lower-case, numbers, symbols.
                2. Common patterns.
                3. Password length.
                4. Password complexity, and overall security.
                Estimated Time to Crack:
                1. Estimate time to brute-force the password.
                2. Estimate time to crack the password using common methods.
                3. Estimate time to crack the password using advanced methods.
                Suggestions (skip if password is already very strong):
                1. Provide improvements to the current password while maintaining the base structure of the password.
                    - Give a new crack time estimate for the suggested password improvements. 
                2. If the password is too weak or contains common patterns, suggest a completely new strong password.
                    - Give a new crack time estimate for the new suggested password.

                Keep each bullet under Analysis under 50 characters, and all other bullets under 150 characters.
                """;
            
            var options = new ChatCompletionsOptions
            {
                Messages =
                {
                    new ChatMessage(ChatRole.System, "You are an expert cyber security analyst and password cracker."),
                    new ChatMessage(ChatRole.User, prompt)
                }
            };

            Response<ChatCompletions> response = await _client.GetChatCompletionsAsync(_deployment, options);
            return response.Value.Choices[0].Message.Content;
        }
    }
}


