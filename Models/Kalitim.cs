using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Data;

namespace WebApp.Models
{
    public class Kalitim
    {
        public MYPORTALEntities Dbc { get; set; }
        public Kalitim()
        {
            Dbc = new MYPORTALEntities();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        ~Kalitim()
        {
            Dispose();
        }
    }
}