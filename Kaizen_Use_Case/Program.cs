using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Kaizen_Use_Case {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            while (true) {
                Console.Clear();
                Console.WriteLine("Ana Menü:");
                Console.WriteLine("1. Kampanya Kodu Doğrulama ve Oluşturma");
                Console.WriteLine("2. JSON Dosyası Okuma ve İşleme");
                Console.WriteLine("3. Çıkış");
                Console.Write("Lütfen bir seçenek girin (1-3): ");

                var choice = Console.ReadLine();

                switch (choice) {
                    case "1":
                        RunCampaignCodeApplication();
                        break;
                    case "2":
                        RunJsonFileProcessingApplication();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçenek. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        static void RunCampaignCodeApplication() {
            CodeGenerator generator = new CodeGenerator();

            Console.WriteLine("EXAMPLE CODES: ");

            int numberOfCodes = 10; // Number of codes to generate
            for (int i = 0; i < numberOfCodes; i++) {
                string code = generator.GenerateCode();
                Console.WriteLine(code);
            }

            // Example code verification
            Console.Write("Enter Your Code: ");
            var testCode = Console.ReadLine();

            if (testCode.Length != 8) {
                Console.WriteLine("The campaign code entered must be 8 characters.");
            }
            else {
                //string testCode = "A2CDEFGH";
                bool isValid = generator.CheckCode(testCode);
                string validOrNot = isValid == true ? "is" : "is NOT";
                Console.WriteLine($"Your code that {testCode} {validOrNot} valid");

            }

            Console.WriteLine("Devam etmek için bir tuşa basın...");
            Console.ReadKey();
        }

        static void RunJsonFileProcessingApplication() {
            // Initialize OpenFileDialog
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                Title = "Select a JSON file"
            };

            // Show the dialog and get result
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                // Get the selected file path
                string jsonFilePath = openFileDialog.FileName;

                // Read the JSON file
                string jsonContent = File.ReadAllText(jsonFilePath);

                // Deserialize the JSON content into a list of Line objects
                List<Line> lines = JsonConvert.DeserializeObject<List<Line>>(jsonContent);

                lines.RemoveAt(0);

                ReceiptScanner scanner = new ReceiptScanner();
                // Process and print the lines in the desired format
                scanner.ProcessAndPrintLines(lines);
            }
            else {
                Console.WriteLine("No file selected.");
            }

            Console.WriteLine("Devam etmek için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}