using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class Server
    {
        private readonly HttpListener _httpListener;
        private string[] _updateData;
        private List<string>[] _bulletData;
        private List<string>[] _prizesData;
        private int _clientId;

        public Server(List<string> prefixes)
        {
            _httpListener = new HttpListener();

            foreach (var prefix in prefixes)
            {
                _httpListener.Prefixes.Add(prefix);
            }

            _updateData = new string[] { "{}", "{}" };
            _bulletData = new List<string>[] { new List<string>(), new List<string>() };
            _prizesData = new List<string>[] { new List<string>(), new List<string>() };
        }

        public void Loop()
        {
            _httpListener.Start();

            while (true)
            {
                var context = _httpListener.GetContext();

                if (context.Request.RawUrl.Contains("Bullet"))
                {
                    if (context.Request.HttpMethod == "GET")
                    {
                        Task.Run(() => {
                            var stream = context.Response.OutputStream;
                            var bulletList = _bulletData[(int.Parse(context.Request.QueryString.Get("id")) - 1) * -1];
                            if (bulletList.Count > 0)
                            {
                                var buffer = Encoding.UTF8.GetBytes(bulletList[0]);
                                bulletList.RemoveAt(0);
                                context.Response.ContentLength64 = buffer.Length;
                                stream.Write(buffer, 0, buffer.Length);
                            }
                            stream.Close();
                        });
                    }
                    else
                    {
                        Task.Run(() => {
                            var inputStream = context.Request.InputStream;
                            var buffer = new byte[context.Request.ContentLength64];
                            inputStream.Read(buffer, 0, (int)context.Request.ContentLength64);
                            var bullets = Encoding.UTF8.GetString(buffer);
                            _bulletData[int.Parse(context.Request.QueryString.Get("id"))].Add(bullets);
                            inputStream.Close();
                            var stream = context.Response.OutputStream;
                            stream.Close();
                        });
                    }
                }
                else if (context.Request.RawUrl.Contains("Prizes"))
                {
                    if (context.Request.HttpMethod == "GET")
                    {
                        Task.Run(() => {
                            var stream = context.Response.OutputStream;
                            var prizeList = _prizesData[(int.Parse(context.Request.QueryString.Get("id")) - 1) * -1];
                            if (prizeList.Count > 0)
                            {
                                var buffer = Encoding.UTF8.GetBytes(prizeList[0]);
                                prizeList.RemoveAt(0);
                                context.Response.ContentLength64 = buffer.Length;
                                stream.Write(buffer, 0, buffer.Length);
                            }
                            stream.Close();
                        });
                    }
                    else
                    {
                        Task.Run(() => {
                            var inputStream = context.Request.InputStream;
                            var buffer = new byte[context.Request.ContentLength64];
                            inputStream.Read(buffer, 0, (int)context.Request.ContentLength64);
                            var prize = Encoding.UTF8.GetString(buffer);
                            _prizesData[int.Parse(context.Request.QueryString.Get("id"))].Add(prize);
                            inputStream.Close();
                            var stream = context.Response.OutputStream;
                            stream.Close();
                        });
                    }

                }
                else if (context.Request.RawUrl.Contains("Start"))
                {
                    Task.Run(() => {
                        var stream = context.Response.OutputStream;
                        var buffer = Encoding.UTF8.GetBytes((_clientId - 2).ToString());
                        context.Response.ContentLength64 = buffer.Length;
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Close();
                    });
                }
                else if (context.Request.HttpMethod == "GET" && context.Request.QueryString.Count == 0)
                {
                    Task.Run(() => {
                        Console.WriteLine("Connected client: " + _clientId);
                        var stream = context.Response.OutputStream;
                        var buffer = Encoding.UTF8.GetBytes(_clientId.ToString());
                        _clientId++;
                        context.Response.ContentLength64 = buffer.Length;
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Close();
                    });
                }
                else
                {
                    if (context.Request.HttpMethod == "GET")
                    {
                        Task.Run(() => {
                            var stream = context.Response.OutputStream;
                            var buffer = Encoding.UTF8.GetBytes(_updateData[(int.Parse(context.Request.QueryString.Get("id")) - 1) * -1]);
                            context.Response.ContentLength64 = buffer.Length;
                            stream.Write(buffer, 0, buffer.Length);
                            stream.Close();
                        });
                    }
                    else
                    {
                        Task.Run(() => {
                            var inputStream = context.Request.InputStream;
                            var buffer = new byte[context.Request.ContentLength64];
                            inputStream.Read(buffer, 0, (int)context.Request.ContentLength64);
                            _updateData[int.Parse(context.Request.QueryString.Get("id"))] = Encoding.UTF8.GetString(buffer);
                            inputStream.Close();
                            var stream = context.Response.OutputStream;
                            stream.Close();
                        });
                    }
                }
            }
        }
    }
}
