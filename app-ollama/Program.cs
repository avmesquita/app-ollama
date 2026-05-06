using Microsoft.Extensions.AI;
using OllamaSharp;

IChatClient chatClient = new OllamaApiClient(
    new Uri("http://hp-envy-17:11434"),
    "qwen2.5-coder:1.5b");

List<ChatMessage> chatHistory = new();

while (true)
{
    Console.Write("User: ");
    string userInput = Console.ReadLine() ?? string.Empty;

    chatHistory.Add(new ChatMessage(ChatRole.User, userInput));

    Console.WriteLine($"Assistant: ");
    var assistantResponse = "";

    await foreach(var update in chatClient.GetStreamingResponseAsync(chatHistory))
    {
        Console.Write(update.Text);
        assistantResponse += update.Text;
    }

    chatHistory.Add(new ChatMessage(ChatRole.Assistant, assistantResponse));
    Console.WriteLine("\n\n");
}
