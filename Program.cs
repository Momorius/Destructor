using System;
using System.IO;

namespace FileDamageExample
{
    class Program
    {
        static void Main(string[] args)
        { 
            string fileName = "example.png";
            destruirPNG(fileName);
            //destruirTXT(fileName);

        }
         static void destruirPNG(string fileName)
        {
            // El nombre del archivo a dañar
            Console.WriteLine("destruyendo PNG");
            

            // Creamos una copia de seguridad del archivo original
            string backupFileName = $"{fileName}.backup";
            File.Copy(fileName, backupFileName, true);

            // Abrimos el archivo .png en modo lectura y escritura
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                // Buscamos la sección IDAT en el archivo .png
                bool idatFound = false;
                byte[] idatSignature = new byte[] { 73, 68, 65, 84 }; // "IDAT"
                byte[] buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < bytesRead - 3; i++)
                    {
                        if (buffer[i] == idatSignature[0] &&
                            buffer[i + 1] == idatSignature[1] &&
                            buffer[i + 2] == idatSignature[2] &&
                            buffer[i + 3] == idatSignature[3])
                        {
                            // Encontramos la sección IDAT, comenzamos a cambiar los bytes
                            idatFound = true;
                            break;
                        }
                    }

                    if (idatFound)
                    {
                        // Generamos un nuevo valor aleatorio para cada byte
                        Random random = new Random();
                        for (int i = 0; i < bytesRead; i++)
                        {
                            buffer[i] = (byte)random.Next(256);
                        }

                        // Escribimos los bytes cambiados de vuelta al archivo .png
                        stream.Position -= bytesRead;
                        stream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        
        }
        static void destruirTXT(string fileName)
        {

            // El nombre del archivo a dañar
            Console.WriteLine("destruyendo texto");
            // Creamos una copia de seguridad del archivo original
            string damegedFileName = $"{fileName}.damaged";
            File.Copy(fileName, damegedFileName, true);

            // Abrimos el archivo en modo lectura y escritura
            using (FileStream stream = new FileStream(damegedFileName, FileMode.Open, FileAccess.ReadWrite))
            {
                // Cambiamos el contenido del archivo de manera aleatoria
                Random rnd = new Random();
                byte[] buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < bytesRead; i++)
                    {
                        // Generamos un nuevo valor aleatorio para el byte
                        buffer[i] = (byte)rnd.Next(256);
                    }

                    // Escribimos los bytes aleatorios en el archivo
                    stream.Position -= bytesRead;
                    stream.Write(buffer, 0, bytesRead);
                }
            }

    }
    }
}