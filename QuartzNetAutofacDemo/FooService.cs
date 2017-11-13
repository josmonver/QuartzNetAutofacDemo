using System;
using System.IO;

namespace QuartzNetAutofacDemo
{
    public class FooService : IFooService
    {
        public void DoSomething()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/logs");
            using (StreamWriter sw = new StreamWriter(path + "\\foo.log", true))
            {
                sw.WriteLine(DateTime.Now.ToLongTimeString() +  " Something has been done!");
            }
        }
    }

    public interface IFooService
    {
        void DoSomething();
    }
}