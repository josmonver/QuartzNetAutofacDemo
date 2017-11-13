using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuartzNetAutofacDemo
{
    public class FooJob : IJob
    {
        private readonly IFooService _service;

        public FooJob(IFooService service)
        {
            _service = service;
        }

        public void Execute(IJobExecutionContext context)
        {
            _service.DoSomething();
        }
    }
}