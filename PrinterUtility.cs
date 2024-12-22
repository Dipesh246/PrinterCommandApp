using System.Net.Sockets;
using System.IO.Ports;
using WIA;

namespace PrinterCommandApp
{
    public class NetworkPrinter
    {
        public static void PrintPdfOverIp(string printerIp, int port, string filePath)
        {
            try
            {
                // Open a TCP connection to the printer
                using (TcpClient client = new TcpClient(printerIp, port))
                using (NetworkStream stream = client.GetStream())
                {
                    Console.WriteLine($"Connected to printer at {printerIp}:{port}");

                    // Read the PDF file as binary data
                    byte[] fileBytes = File.ReadAllBytes(filePath);

                    // Send the file data to the printer
                    stream.Write(fileBytes, 0, fileBytes.Length);
                    Console.WriteLine("PDF file sent to the printer successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public static void PrintPdfOverComPort(string comPort, int baudRate, string filePath)
        {
            try
            {
                // Configure the serial port
                using (SerialPort port = new SerialPort(comPort, baudRate, Parity.None, 8, StopBits.One))
                {
                    port.Open();
                    Console.WriteLine($"Connected to printer on {comPort} at {baudRate} baud.");

                    // Read the PDF file as binary data
                    byte[] fileBytes = File.ReadAllBytes(filePath);

                    // Write the file data to the serial port
                    port.Write(fileBytes, 0, fileBytes.Length);

                    Console.WriteLine("PDF file sent to the printer successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public static void ScanDocument(string outputPath)
        {
            try
            {
                // Specify the save path for the scanned document
                //string savePath = @"C:\Users\paban\OneDrive\Desktop\scannedfile.jpg";

                // Create a WIA common dialog instance
                CommonDialog dialog = new CommonDialog();

                // Allow the user to select the scanner device
                Device? scanner = dialog.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, false, false);

                if (scanner == null)
                {
                    Console.WriteLine("No scanner selected. Exiting program.");
                    return;
                }

                Console.WriteLine("Scanner selected. Starting scan... " + scanner);

                // Select the first item (assuming it's the scanner item)
                Item item = scanner.Items[1];

                // Define the format for the scanned image (JPEG)
                const string WiaFormatJpeg = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";

                // Transfer the scanned image to a file in JPEG format
                ImageFile image = (ImageFile)item.Transfer(WiaFormatJpeg);

                // Save the file to the specified path
                image.SaveFile(outputPath);

                Console.WriteLine($"Scanned document saved successfully to: {outputPath}");
            }
            catch (System.Runtime.InteropServices.COMException comEx)
            {
                Console.WriteLine($"WIA Error: {comEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }
    }
}
