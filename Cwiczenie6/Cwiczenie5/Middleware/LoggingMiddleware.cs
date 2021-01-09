using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenie5.Middleware
{
    
    public class LoggingMiddleware
    {

        private readonly RequestDelegate _next;
        static int logNumber = 0;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            if (context.Request != null)
            {
                string path = context.Request.Path;
                string method = context.Request.Method;
                string queryString = context.Request.QueryString.ToString();
                string bodyStr = "";

                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }

                //ZAPISZ DO PLIKU
                logNumber++;
                string logId=" Logowanie w sesji: " + logNumber;
                string[] lines = {logId, path, method, queryString, bodyStr, "\n" };


                string filePath = @"Log.txt";
                if (!File.Exists(filePath))
                {

                    using (StreamWriter streamWriter = File.CreateText(filePath))
                    {
                        foreach (string line in lines)
                        {
                            streamWriter.WriteLine(line);
                        }

                    }
                }
                else
                {

                    using (StreamWriter outputFile = new StreamWriter(filePath, true))
                    {


                        foreach (string line in lines)
                        {
                            outputFile.WriteLine(line);
                        }



                    }
                }


            }


                if (_next != null) await _next(context);
            }


        }
    }
