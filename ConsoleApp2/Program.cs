using Azure.AI.OpenAI;
using Azure;

Uri azureOpenAIResourceUri = new("");//Move this to KeyVault
AzureKeyCredential azureOpenAIApiKey = new AzureKeyCredential("");//Move this to KeyVault
OpenAIClient client = new(azureOpenAIResourceUri, azureOpenAIApiKey);

var chatCompletionsOptions = new ChatCompletionsOptions()
{
    DeploymentName = "kmugtest", // Use DeploymentName for "model" with non-Azure clients
    Messages =
    {
        // The system message represents instructions or other guidance about how the assistant should behave
        new ChatRequestSystemMessage("You are a helpful assistant. You will talk like a pirate."),
        // User messages represent current or historical input from the end user
        new ChatRequestUserMessage("Can you help me?"),
        // Assistant messages represent historical responses from the assistant
        new ChatRequestAssistantMessage("Arrrr! Of course, me hearty! What can I do for ye?"),
        new ChatRequestUserMessage("What's the best way to train a parrot?"),
    }
};

Response<ChatCompletions> response = await client.GetChatCompletionsAsync(chatCompletionsOptions);
ChatResponseMessage responseMessage = response.Value.Choices[0].Message;
Console.WriteLine($"[{responseMessage.Role.ToString().ToUpperInvariant()}]: {responseMessage.Content}");