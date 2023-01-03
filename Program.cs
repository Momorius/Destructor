using System;
using System.IO;

namespace FileDamageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // El nombre del archivo a dañar
            Console.WriteLine("Version 1.0");
            string fileName = "example.txt";

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