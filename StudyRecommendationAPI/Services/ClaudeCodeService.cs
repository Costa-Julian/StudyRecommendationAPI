using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Options;
using StudyRecommendationAPI.Configuration;

namespace StudyRecommendationAPI.Services;

/// <summary>
/// Runs prompts through the Claude Code CLI process and captures the response.
/// </summary>
public class ClaudeCodeService(IOptions<ExternalApisConfig> config)
{
    private readonly ClaudeCodeConfig _config = config.Value.ClaudeCode;

    /// <summary>
    /// Sends a prompt to the Claude Code CLI and returns the raw text response.
    /// </summary>
    public async Task<(bool Success, string Result, string Error)> RunPromptAsync(string prompt)
    {
        ProcessStartInfo startInfo = BuildStartInfo(prompt);

        using Process process = new() { StartInfo = startInfo };

        try
        {
            process.Start();
            process.StandardInput.Close(); // señala modo no-interactivo

            using CancellationTokenSource cts = new(TimeSpan.FromSeconds(_config.TimeoutSeconds));

            Task<string> stdoutTask = process.StandardOutput.ReadToEndAsync(cts.Token);
            Task<string> stderrTask = process.StandardError.ReadToEndAsync(cts.Token);

            await process.WaitForExitAsync(cts.Token);

            string output = await stdoutTask;
            string error = await stderrTask;

            if (process.ExitCode != 0)
                return (false, string.Empty, string.IsNullOrWhiteSpace(error) ? $"Exit code {process.ExitCode}" : error);

            return (true, output.Trim(), string.Empty);
        }
        catch (OperationCanceledException)
        {
            try { process.Kill(entireProcessTree: true); } catch { /* already exited */ }
            return (false, string.Empty, $"El proceso superó el tiempo límite de {_config.TimeoutSeconds} segundos.");
        }
        catch (Exception ex)
        {
            return (false, string.Empty, ex.Message);
        }
    }

    private ProcessStartInfo BuildStartInfo(string prompt)
    {
        // On Windows, .cmd wrappers (npm-installed tools) require cmd.exe
        bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        ProcessStartInfo startInfo = new()
        {
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            WorkingDirectory = @"C:\"
        };

        if (isWindows)
        {
            startInfo.FileName = "cmd.exe";
            startInfo.ArgumentList.Add("/c");
            startInfo.ArgumentList.Add(_config.ExecutablePath);
        }
        else
        {
            startInfo.FileName = _config.ExecutablePath;
        }

        startInfo.ArgumentList.Add("-p");
        startInfo.ArgumentList.Add(prompt);
        startInfo.ArgumentList.Add("--output-format");
        startInfo.ArgumentList.Add("text");
        startInfo.ArgumentList.Add("--allowedTools");
        startInfo.ArgumentList.Add("WebSearch");
        startInfo.ArgumentList.Add("--dangerously-skip-permissions");

        return startInfo;
    }
}
