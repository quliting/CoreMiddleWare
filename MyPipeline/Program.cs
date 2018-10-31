using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyPipeline
{
    internal class Program
    {
        public static List<Func<RequestDelegate, RequestDelegate>> _list = new List<Func<RequestDelegate, RequestDelegate>>();

        private static void Main(string[] args)
        {
            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine("1"); 
                    return next.Invoke(context);
                };
            });

            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine("2");
                    return next.Invoke(context);
                };
            });

            RequestDelegate end = async context =>
            {
                Console.WriteLine("end");
                await Task.CompletedTask;
            };

            _list.Reverse();
            foreach (var middleWare in _list)
            {
                end = middleWare.Invoke(end); 
            }

            end.Invoke(new Context());
            Console.ReadLine();
        }

        public static void Use(Func<RequestDelegate, RequestDelegate> _middleWare)
        {
            _list.Add(_middleWare);
        }
    }
}