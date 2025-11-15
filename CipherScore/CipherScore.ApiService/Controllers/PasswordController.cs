using Microsoft.AspNetCore.Mvc;
using CipherScore.ApiService.Services;
using System.Diagnostics;

namespace CipherScore.ApiService.Controllers
{
    public class PasswordRequest
    {
        public string Password { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly AIService _aiService;
        public PasswordController(AIService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("ai-suggestions")]
        public async Task<IActionResult> Evaluate([FromBody] PasswordRequest request)
        {
            var stopwatch = Stopwatch.StartNew();

            //Call the AI Service to get its analysis as a string.
            var aiAnalysis = await _aiService.EvaluatePasswordAsync(request.Password);

            stopwatch.Stop();

            var result = new PasswordEvaluationResult
            {
                AiSuggestions = aiAnalysis,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
            };

            //7. Return the structured response to the frontend.
            return Ok(result);
        }


        private string[] GetFunctionSuggestions(string password)
        {
            var suggestions = new List<string>();
            if (password.Length < 12) suggestions.Add("Increase password length to at least 12 characters.");
            if (!password.Any(char.IsUpper)) suggestions.Add("Add uppercase letters.");
            if (!password.Any(char.IsDigit)) suggestions.Add("Include numbers.");
            if (!password.Any(ch => "!@#$%^&*()".Contains(ch))) suggestions.Add("Add special symbols.");

            return suggestions.Take(3).ToArray(); //limit to 3 suggestions
        }

        //Estimate brute-force time using a simple entropy-based formula.
        private string EstimateBruteForceTime(string password)
        {
            int entropy = password.Length * 4; // Simple entropy estimate: 4bits per character
            double guesses = Math.Pow(2, entropy);
            double seconds = guesses / 1_000_000_000; // Assume 1 billion guesses/sec
            if (seconds < 3600) return $"{seconds / 60:F2} minutes";
            if (seconds < 86400) return $"{seconds / 3600:F2} hours";
            if (seconds < 31536000) return $"{seconds / 86400:F2} days";
            return $"{seconds / 31536000:F2} years";
        }
        // Warn about common pattern in the password.

        private string DetectPatternMisuse(string password)
        {
            if (password.ToLower().Contains("password") || password.Contains("1234"))
                return "Avoid using common patterns like 'password' or sequential numbers.";
            return "No common patterns detected.";
        }


       
    }
    public class PasswordEvaluationResult
    {
        public string AiSuggestions { get; set; }         // Suggestions from the AI service
        public long ElapsedMilliseconds { get; set; }     // Time taken for analysis in milliseconds
    }


}
