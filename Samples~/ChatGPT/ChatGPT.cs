using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    // Klassen ChatGPT är en MonoBehaviour, vilket betyder att den kan fästas på ett Unity-objekt.
    public class ChatGPT : MonoBehaviour
    {
        // Fält för att koppla UI-element i Unity-redigeraren.
        [SerializeField] private InputField inputField;  // Inmatningsfältet för användaren att skriva meddelanden.
        [SerializeField] private Button button;  // Knappen för att skicka meddelanden.
        [SerializeField] private ScrollRect scroll;  // Skrollområdet där meddelanden visas.
        
        // Fält för meddelandevisningslayout.
        [SerializeField] private RectTransform sent;  // Layout för skickade meddelanden.
        [SerializeField] private RectTransform received;  // Layout för mottagna meddelanden.

        private float height;  // Variabel som lagrar höjden på chattfönstret.
        private OpenAIApi openai = new OpenAIApi();  // Instans av en OpenAIApi-klass (förmodligen för att kommunicera med GPT-3).

        private List<ChatMessage> messages = new List<ChatMessage>();  // Lista över meddelanden i chatten.
        // En prompt som anger en bakgrundshistoria för GPT-3:s svar.
        private string prompt = "In 2105, where magic meets circuits, ... avoid revealing your true nature.";

        // Metod som körs när objektet startas i Unity.
        private void Start()
        {
            // Lägg till en lyssnare till knappens klickhändelse.
            button.onClick.AddListener(SendReply);
        }

        // Funktion för att lägga till ett nytt meddelande i chattfönstret.
        private void AppendMessage(ChatMessage message)
        {
            // Kod för att hantera utseendet och lägga till meddelandet i chatten.
        }

        // Asynkron funktion för att skicka ett svar via GPT-3 API.
        private async void SendReply()
        {
            // Skapa ett nytt meddelandeobjekt från användarens inmatning.
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };
            
            // Lägg till meddelandet i chattfönstret.
            AppendMessage(newMessage); 

            // Om det är det första meddelandet, lägg till prompten före användarens inmatning.
            if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputField.text; 
            
            // Lägg till meddelandet i meddelandelistan.
            messages.Add(newMessage);
            
            // Inaktivera knappen och inmatningsfältet medan svar väntar.
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;
            
            // Få svar från GPT-3 API.
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0613",
                Messages = messages
            });

            // Om det finns ett svar, lägg till det i chattfönstret.
            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                // Kod för att hantera svaret och lägga till det i chatten.
            }
            else
            {
                // Om inget svar genererades, logga en varning.
                Debug.LogWarning("No text was generated from this prompt.");
            }

            // Återaktivera knappen och inmatningsfältet.
            button.enabled = true;
            inputField.enabled = true;
        }
    }
}
