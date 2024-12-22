using PrinterCommandApp;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "D:/Dipesh/Documents/College/Document.pdf";
        string filesavePath = @"C:\\Users\\dipes\\OneDrive\\Desktop\\testDocument.png";
        string ipAddress = "192.168.1.68";
        int ipPort = 9100;
        NetworkPrinter.ScanDocument(filesavePath);

        //if (!File.Exists(filePath))
        //{
        //    Console.WriteLine("File not found. Please check the path and try again.");
        //    return;
        //}

        //PrinterUtility.PrintTestFile(filePath);
        //NetworkPrinter.PrintPdfOverIp("192.168.1.68", 9100, filePath);
        //Console.WriteLine("Press any key to exit...");
        //Console.ReadKey();
    }
}
