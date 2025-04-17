using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace modul8_103022300093
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int Fee = 0;
            string jsonFilePath = "bank_transfer_config.json";
            BankTransferConfig config = BankTransferConfig.ReadConfigFile(jsonFilePath);
            if (config.Lang == "en")
            {
                Console.Write("Please insert the amount of money to transfer: ");
            }
            else if (config.Lang == "id")
            {
                Console.Write("Masukkan jumlah uang yang akan di - transfer: ");
            }
            string amountStr = Console.ReadLine();
            if (int.TryParse(amountStr, out int amount) && int.TryParse(config.Transfer.Threshold, out int threshold) && amount <= threshold)
            {
                Fee = int.Parse(config.Transfer.LowFee);
            }
            else
            {
                Fee = int.Parse(config.Transfer.HighFee);
            }
            if (config.Lang == "id")
            {
                Console.WriteLine($"Biaya Transfer = {Fee}\nTotal Biaya = {Fee + amount}\nPilih metode transfer: ");
            }
            else if (config.Lang == "en")
            {
                Console.WriteLine($"Transfer Fee = {Fee}\nTotal Amount = {Fee + amount}\nSelect payment method: ");
            }
            int no = 1;
            foreach (var method in config.Methods)
            {
                Console.WriteLine(no + ". " + method);
                no++;
            }
            if (config.Lang == "id")
            {
                Console.Write("Pilih metode transfer: ");
            }
            else if (config.Lang == "en")
            {
                Console.Write("Select payment method: ");
            }
            string methodStr = Console.ReadLine();
            int.TryParse(methodStr, out int methodIndex);
            if (methodIndex > 0 && methodIndex <= config.Methods.Count)
            {
                string selectedMethod = config.Methods[methodIndex - 1];
                if (config.Lang == "id")
                {
                    Console.Write($"Ketik '{config.Confirmation["id"]}' untuk mengkonfirmasi transaksi: ");
                    string confirmation = Console.ReadLine();
                    if (confirmation == config.Confirmation["id"])
                    {
                        Console.WriteLine($"Transaksi berhasil menggunakan metode {selectedMethod}.");
                    }
                    else
                    {
                        Console.WriteLine("Konfirmasi gagal.");
                    }
                }
                else if (config.Lang == "en")
                {
                    Console.Write($"Please type '{config.Confirmation["en"]}' to confirm the transaction: ");
                    string confirmation = Console.ReadLine();
                    if (confirmation == config.Confirmation["en"])
                    {
                        Console.WriteLine($"Transaction successful using {selectedMethod} method.");
                    }
                    else
                    {
                        Console.WriteLine("Confirmation failed.");
                    }
                }
            }
            else
            {
                if (config.Lang == "id")
                {
                    Console.WriteLine("Pilihan tidak valid.");
                }
                else if (config.Lang == "en")
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
        }
    }
}