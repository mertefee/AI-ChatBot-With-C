using System;
using RestSharp;
using Newtonsoft.Json;

public class Client
{
    private readonly string _apianahtari;
    private readonly RestClient _client;

    // API key'i parametreleştiriyoruz
    public Client(string apiKey)
    {
        _apianahtari = apiKey;
        // API'nin Endpointi
        _client = new RestClient("https://api.openai.com/v1/chat/completions");
    }

    // Mesaj gönderdiğimiz ve geri cevap aldığımız methodlar
    public string SendMessage(string message)
    {
        // Yeni bir post request oluşturuyoruz
        var request = new RestRequest("", Method.Post);
        // Content-Type'ı header olarak ayarlıyoruz
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", $"Bearer {_apianahtari}");

        // Request oluşturduğumuz alan
        var requestBody = new
        {
            model = "gpt-3.5-turbo", // Kullandığımız ChatGPT Modeli
            messages = new[]
            {
                new { role = "user", content = message }
            },
            max_tokens = 100,
            temperature = 0.7
        };

        // JSON body request ekliyoruz
        request.AddJsonBody(JsonConvert.SerializeObject(requestBody));

        // cevap aldığımız kısım
        var response = _client.Execute(request);

        // log cevapsız kaldığında burası çalışır
        if (!response.IsSuccessful)
        {
            Console.WriteLine($"Error: {response.ErrorMessage}");
            return string.Empty;
        }

        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content ?? string.Empty);

        // seçimin exit olup olmadığını kontrol ediyoruz
        if (jsonResponse?.choices != null && jsonResponse.choices.Count > 0)
        {
            // Yanıtı stringe çeviriyoruz
            string chatbotResponse = jsonResponse.choices[0].message.content.ToString();
            return chatbotResponse?.Trim() ?? string.Empty;
        }
        else
        {
            // Geri Cevap Alamadığımız Yer
            Console.WriteLine("No valid response from API.");
            return string.Empty;
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        // Api Keyi Buraya Yazıyoruz
        string apiKey = "YOUR_APİ_KEY_HERE";

        // API anahtarıyla chatgptclienti oluşturuyoruz
        var chatGPTClient = new Client(apiKey);

        // Hoşgeldin Mesajı 
        Console.WriteLine("AI asistana giriş yaptınız çıkmak için exit yazın.");

        // Kullanıcı exit yazmadığı sürece loop alanımız burası boylece konuşmaya devam ediyoruz
        while (true)
        {
            // Kullanıcıdan input istiyoruz
            Console.ForegroundColor = ConsoleColor.Green; // Set text color to green
            Console.Write("Me: ");
            Console.ResetColor(); // Reset text color to default
            string input = Console.ReadLine() ?? string.Empty;

            // loop'dan çıkış için exit komutunu kullanıyoruz
            if (input.ToLower() == "exit")
                break;

            // Kullanıcı İnputlarını api'ye yollayıp geri mesaj alıyoruz
            string response = chatGPTClient.SendMessage(input);

            // Chatbotun ayarları
            Console.ForegroundColor = ConsoleColor.Blue; // Asistan Mavi Olarak Görünüyor
            Console.Write("Asistan: ");
            Console.ResetColor(); // text color default kalıyor
            Console.WriteLine(response);
        }
    }
}
